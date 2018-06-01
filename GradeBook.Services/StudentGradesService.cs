using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.Common.Exceptions;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.DAL.UoW.Abstractions;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;

namespace GradeBook.Services
{
    public sealed class StudentGradesService : IStudentGradesService
    {
        private readonly IUnitOfWork<IGradesRepository> _gradesUnitOfWork;
        private readonly IUnitOfWork<IFinalGradesRepository> _finalGradesUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IGradebooksService _gradebooksService;
        private readonly IStudentsService _studentsService;
        private readonly ISubjectsService _subjectsService;

        private const int MinFinalGradeValue = 60;
        private const int MaxFinalGradeValue = 100;

        public StudentGradesService(IUnitOfWork<IGradesRepository> gradesUnitOfWork,
            IUnitOfWork<IFinalGradesRepository> finalGradesUnitOfWork, IMapper mapper,
            IGradebooksService gradebooksService, IStudentsService studentsService, ISubjectsService subjectsService)
        {
            _gradesUnitOfWork = gradesUnitOfWork;
            _finalGradesUnitOfWork = finalGradesUnitOfWork;
            _mapper = mapper;
            _gradebooksService = gradebooksService;
            _studentsService = studentsService;
            _subjectsService = subjectsService;
        }

        public async Task<IEnumerable<FinalGradeDto>> GetStudentFinalGradesAsync(int studentId)
        {
            var finalGrades = await _finalGradesUnitOfWork.Repository.GetAllAsync(s => s.StudentRefId == studentId)
                .ConfigureAwait(false);

            return _mapper.Map<IEnumerable<FinalGradeDto>>(finalGrades);
        }

        public async Task<FinalGradeDto> GetStudentSubjectFinalGradeAsync(int studentId, int subjectId)
        {
            var finalGrade = (await _finalGradesUnitOfWork.Repository
                .GetAllAsync(s => s.StudentRefId == studentId && s.Gradebook.SubjectRefId == subjectId)
                .ConfigureAwait(false)).FirstOrDefault();

            if (finalGrade == null)
            {
                return null;
            }

            return _mapper.Map<FinalGradeDto>(finalGrade);
        }

        public async Task<StudentSubjectGradesDto> GetStudentSubjectCurrentGradesAsync(int studentId, int subjectId)
        {
            var currentGrades = await _gradesUnitOfWork.Repository.GetAllAsync(s =>
                s.StudentRefId == studentId && s.Gradebook.SubjectRefId == subjectId).ConfigureAwait(false);

            var finalGrade = await GetStudentSubjectFinalGradeAsync(studentId, subjectId).ConfigureAwait(false);

            var student = await _studentsService.GetStudentAsync(studentId).ConfigureAwait(false);

            if (student == null)
            {
                throw new ResourceNotFoundException($"Стедент {studentId} не знайдений");
            }
            
            var gradebook = await _gradebooksService.GetGradebookByGroupAsync(student.Group.Id, subjectId)
                .ConfigureAwait(false);

            var subject = await _subjectsService.GetSubjectAsync(subjectId).ConfigureAwait(false);
            
            if (gradebook == null)
            {
                throw new ResourceNotFoundException($"Журнал групи {student.Group.Code} по предмету {subject?.Name ?? subjectId.ToString()} не знайдений");
            }
            
            return new StudentSubjectGradesDto
            {
                CurrentGrades = _mapper.Map<IEnumerable<GradeDto>>(currentGrades),
                FinalGrade = _mapper.Map<FinalGradeDto>(finalGrade),
                AssessmentType = gradebook.AssessmentType
            };
        }

        public async Task<GradeDto> GetGradeAsync(int gradeId)
        {
            var grade = await _gradesUnitOfWork.Repository.GetByIdAsync(gradeId).ConfigureAwait(false);

            if (grade == null)
            {
                return null;
            }

            return _mapper.Map<GradeDto>(grade);
        }

        public async Task<FinalGradeDto> GetFinalGradeAsync(int finalGradeId)
        {
            var grade = await _finalGradesUnitOfWork.Repository.GetByIdAsync(finalGradeId).ConfigureAwait(false);

            if (grade == null)
            {
                return null;
            }

            return _mapper.Map<FinalGradeDto>(grade);
        }

        public async Task<GradeDto> AddStudentCourseGradeAsync(GradeDto grade, int studentId, int teacherId, int subjectId)
        {
            if (grade.Value == 0)
            {
                throw new ResourceOperationException("Бал не повинен бути 0");
            }

            using (var transaction = await _gradesUnitOfWork.BeginTransactionAsync(IsolationLevel.RepeatableRead).ConfigureAwait(false))
            {
                var student = await _studentsService.GetStudentAsync(studentId).ConfigureAwait(false);

                if (student == null)
                {
                    throw new ResourceNotFoundException($"Студент {studentId} не знайдений");
                }
                
                var finalGrade = await GetStudentSubjectFinalGradeAsync(studentId, subjectId).ConfigureAwait(false);

                var subject = await _subjectsService.GetSubjectAsync(subjectId).ConfigureAwait(false);
                
                if (finalGrade != null)
                {
                    throw new ResourceOperationException($"Студент {student.LastName} {student.FirstName} ({studentId}) вже має кінцеву оцінку по предмету {subject?.Name ?? subjectId.ToString()}");
                }

                var gradebook = await _gradebooksService.GetGradebookByGroupAsync(student.Group.Id, subjectId)
                    .ConfigureAwait(false);
                
                if (gradebook == null)
                {
                    throw new ResourceNotFoundException($"Журнал групи {student.Group.Code} по предмету {subject?.Name ?? subjectId.ToString()} не знайдено");
                }

                if (gradebook.Teachers.All(t => t.Id != teacherId))
                {
                    throw new ResourceAccessPermissionException($"Викладач {teacherId} не має доступу до журналу {gradebook.Id}");
                }

                var currentGradeTotal =
                    await GetStudentCourseCurrentGradeTotalAsync(studentId, subjectId).ConfigureAwait(false);

                if (currentGradeTotal + grade.Value > MaxFinalGradeValue)
                {
                    throw new ResourceOperationException($"Сумарна кількість балів не має перевищувати {MaxFinalGradeValue} балів");
                }

                var newGrade = new Grade
                {
                    Value = grade.Value,
                    CreatedAt = grade.CreatedAt,
                    Description = grade.Description,
                    GradebookRefId = gradebook.Id,
                    StudentRefId = studentId,
                    TeacherRefId = teacherId
                };

                _gradesUnitOfWork.Repository.Add(newGrade);

                await _gradesUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
                
                transaction.Commit();

                return await GetGradeAsync(newGrade.Id).ConfigureAwait(false);
            }
        }

        public async Task RemoveStudentCourseGradeAsync(int gradeId, int teacherId)
        {
            var grade = await _gradesUnitOfWork.Repository.GetByIdAsync(gradeId).ConfigureAwait(false);

            if (grade == null)
            {
                throw new ResourceNotFoundException($"Оцінка {gradeId} не знайдена");
            }

            var finalGrade = await GetStudentSubjectFinalGradeAsync(grade.StudentRefId, grade.Gradebook.SemesterRefId)
                .ConfigureAwait(false);

            if (finalGrade != null)
            {
                throw new ResourceOperationException($"Студент {grade.Student.Account.LastName} {grade.Student.Account.FirstName} ({grade.StudentRefId}) вже має кінцеву оцінку по предмету {grade.Gradebook.Subject.Name}");
            }

            var gradebook = await _gradebooksService.GetGradebookAsync(grade.GradebookRefId)
                .ConfigureAwait(false);

            if (gradebook == null)
            {
                throw new ResourceNotFoundException($"Журнал {grade.GradebookRefId} не знайдено");
            }

            if (gradebook.Teachers.All(t => t.Id != teacherId))
            {
                throw new ResourceAccessPermissionException($"Викладач {teacherId} не має доступу до журналу {gradebook.Id}");
            }

            _gradesUnitOfWork.Repository.Delete(grade);

            await _gradesUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<FinalGradeDto> ConfirmStudentCourseFinalGradeAsync(int studentId, int teacherId, int subjectId)
        {
            var student = await _studentsService.GetStudentAsync(studentId).ConfigureAwait(false);

            if (student == null)
            {
                throw new ResourceNotFoundException($"Студент {studentId} не знайдний");
            }

            var gradebook = await _gradebooksService.GetGradebookByGroupAsync(student.Group.Id, subjectId)
                .ConfigureAwait(false);

            if (gradebook == null)
            {
                throw new ResourceNotFoundException($"Журнал групи {student.Group.Code} по предмету {subjectId} не знайдено");
            }

            if (gradebook.Teachers.All(t => t.Id != teacherId))
            {
                throw new ResourceAccessPermissionException($"Викладач {teacherId} не має доступу до журналу {gradebook.Id}");
            }

            using (var transaction = await _finalGradesUnitOfWork.BeginTransactionAsync(IsolationLevel.RepeatableRead)
                .ConfigureAwait(false))
            {
                var currentGradeTotal =
                    await GetStudentCourseCurrentGradeTotalAsync(studentId, subjectId).ConfigureAwait(false);

                if (currentGradeTotal < MinFinalGradeValue)
                {
                    throw new ResourceOperationException($"Кінцева оцінка не може бути нижча за {MinFinalGradeValue} балів");
                }

                var finalGrade = new FinalGrade
                {
                    CreatedAt = DateTime.Now,
                    GradebookRefId = gradebook.Id,
                    StudentRefId = studentId,
                    TeacherRefId = teacherId,
                    Value = currentGradeTotal
                };

                _finalGradesUnitOfWork.Repository.Add(finalGrade);

                await _finalGradesUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
                
                transaction.Commit();

                return await GetFinalGradeAsync(finalGrade.Id).ConfigureAwait(false);
            }
        }

        public async Task<int> GetStudentCourseCurrentGradeTotalAsync(int studentId, int subjectId)
        {
            return await _gradesUnitOfWork.Repository
                .GetStudentSubjectCurrentGradeTotalAsync(studentId, subjectId)
                .ConfigureAwait(false);
        }
    }
}