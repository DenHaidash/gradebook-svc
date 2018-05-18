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

        private const int MinFinalGradeValue = 60;
        private const int MaxFinalGradeValue = 100;

        public StudentGradesService(IUnitOfWork<IGradesRepository> gradesUnitOfWork,
            IUnitOfWork<IFinalGradesRepository> finalGradesUnitOfWork, IMapper mapper,
            IGradebooksService gradebooksService, IStudentsService studentsService)
        {
            _gradesUnitOfWork = gradesUnitOfWork;
            _finalGradesUnitOfWork = finalGradesUnitOfWork;
            _mapper = mapper;
            _gradebooksService = gradebooksService;
            _studentsService = studentsService;
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
                throw new ResourceNotFoundException($"Student {studentId} not found");
            }
            
            var gradebook = await _gradebooksService.GetGradebookByGroupAsync(student.Group.Id, subjectId)
                .ConfigureAwait(false);

            if (gradebook == null)
            {
                throw new ResourceNotFoundException($"Gradebook of group {student.Group.Id} for subject {subjectId} not found");
            }
            
            return new StudentSubjectGradesDto
            {
                CurrentGrades = _mapper.Map<IEnumerable<GradeDto>>(currentGrades),
                FinalGrade = _mapper.Map<FinalGradeDto>(finalGrade),
                AssestmentType = gradebook.AssestmentType
            };
        }

        public async Task AddStudentCourseGradeAsync(GradeDto grade, int studentId, int teacherId, int subjectId)
        {
            if (grade.Value == 0)
            {
                throw new ResourceOperationException("Grade value can't be 0");
            }

            using (var transaction = await _gradesUnitOfWork.BeginTransactionAsync(IsolationLevel.RepeatableRead).ConfigureAwait(false))
            {
                var finalGrade = await GetStudentSubjectFinalGradeAsync(studentId, subjectId).ConfigureAwait(false);

                if (finalGrade != null)
                {
                    throw new ResourceOperationException($"Student {studentId} already has final grade for subject {subjectId}");
                }

                var student = await _studentsService.GetStudentAsync(studentId).ConfigureAwait(false);

                if (student == null)
                {
                    throw new ResourceNotFoundException($"Student {studentId} not found");
                }

                var gradebook = await _gradebooksService.GetGradebookByGroupAsync(student.Group.Id, subjectId)
                    .ConfigureAwait(false);

                if (gradebook == null)
                {
                    throw new ResourceNotFoundException($"Gradebook for subject {subjectId} of group {student.Group.Id} not found");
                }

                if (gradebook.Teachers.All(t => t.Id != teacherId))
                {
                    throw new ResourceAccessPermissionException($"Teacher {teacherId} doesn't have an access to gradebook {gradebook.Id}");
                }

                var currentGradeTotal =
                    await GetStudentCourseCurrentGradeTotalAsync(studentId, subjectId).ConfigureAwait(false);

                if (currentGradeTotal + grade.Value > MaxFinalGradeValue)
                {
                    throw new ResourceOperationException($"Total grades for subject can't be greater then {MaxFinalGradeValue}");
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
            }
        }

        public async Task RemoveStudentCourseGradeAsync(int gradeId, int teacherId)
        {
            var grade = await _gradesUnitOfWork.Repository.GetByIdAsync(gradeId).ConfigureAwait(false);

            if (grade == null)
            {
                throw new ResourceNotFoundException($"Grade {gradeId} not found");
            }

            var finalGrade = await GetStudentSubjectFinalGradeAsync(grade.StudentRefId, grade.Gradebook.SemesterRefId)
                .ConfigureAwait(false);

            if (finalGrade != null)
            {
                throw new ResourceOperationException($"Student {grade.StudentRefId} already has final grade for subject {grade.Gradebook.SemesterRefId}");
            }

            var gradebook = await _gradebooksService.GetGradebookAsync(grade.GradebookRefId)
                .ConfigureAwait(false);

            if (gradebook == null)
            {
                throw new ResourceNotFoundException($"Gradebook {grade.GradebookRefId} not found");
            }

            if (gradebook.Teachers.All(t => t.Id != teacherId))
            {
                throw new ResourceAccessPermissionException($"Teacher {teacherId} doesn't have an access to gradebook {gradebook.Id}");
            }

            _gradesUnitOfWork.Repository.Delete(grade);

            await _gradesUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task ConfirmStudentCourseFinalGradeAsync(int studentId, int teacherId, int subjectId)
        {
            var student = await _studentsService.GetStudentAsync(studentId).ConfigureAwait(false);

            if (student == null)
            {
                throw new ResourceNotFoundException($"Student {studentId} not found");
            }

            var gradebook = await _gradebooksService.GetGradebookByGroupAsync(student.Group.Id, subjectId)
                .ConfigureAwait(false);

            if (gradebook == null)
            {
                throw new ResourceNotFoundException($"Gradebook for subject {subjectId} of group {student.Group.Id} not found");
            }

            if (gradebook.Teachers.All(t => t.Id != teacherId))
            {
                throw new ResourceAccessPermissionException($"Teacher {teacherId} doesn't have an access to gradebook {gradebook.Id}");
            }

            using (var transaction = await _finalGradesUnitOfWork.BeginTransactionAsync(IsolationLevel.RepeatableRead)
                .ConfigureAwait(false))
            {
                var currentGradeTotal =
                    await GetStudentCourseCurrentGradeTotalAsync(studentId, subjectId).ConfigureAwait(false);

                if (currentGradeTotal < MinFinalGradeValue)
                {
                    throw new ResourceOperationException($"Final grade can't be lower than {MinFinalGradeValue}");
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