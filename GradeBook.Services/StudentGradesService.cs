using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.DAL.UoW;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;

namespace GradeBook.Services
{
    public class StudentGradesService : IStudentGradesService
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

        public async Task<IEnumerable<GradeDto>> GetStudentFinalGradesAsync(int studentId)
        {
            var finalGrades = await _finalGradesUnitOfWork.Repository.GetAllAsync(s => s.StudentRefId == studentId)
                .ConfigureAwait(false);

            return _mapper.Map<IEnumerable<GradeDto>>(finalGrades);
        }

        public async Task<GradeDto> GetStudentSubjectFinalGradeAsync(int studentId, int subjectId)
        {
            var finalGrade = (await _finalGradesUnitOfWork.Repository
                .GetAllAsync(s => s.StudentRefId == studentId && s.Gradebook.SubjectRefId == subjectId)
                .ConfigureAwait(false)).FirstOrDefault();

            if (finalGrade == null)
            {
                return null;
            }

            return _mapper.Map<GradeDto>(finalGrade);
        }

        public async Task<StudentSubjectGradesDto> GetStudentSubjectCurrentGradesAsync(int studentId, int subjectId)
        {
            var currentGrades = await _gradesUnitOfWork.Repository.GetAllAsync(s =>
                s.StudentRefId == studentId && s.Gradebook.SubjectRefId == subjectId).ConfigureAwait(false);

            var finalGrade = await GetStudentSubjectFinalGradeAsync(studentId, subjectId).ConfigureAwait(false);

            return new StudentSubjectGradesDto
            {
                CurrentGrades = _mapper.Map<IEnumerable<GradeDto>>(currentGrades),
                FinalGrade = _mapper.Map<GradeDto>(finalGrade)
            };
        }

        public async Task AddStudentCourseGradeAsync(GradeDto grade, int studentId, int teacherId, int subjectId)
        {
            if (grade.Value == 0)
            {
                return;
            }

            using (var transaction = await _gradesUnitOfWork.BeginTransactionAsync(IsolationLevel.RepeatableRead).ConfigureAwait(false))
            {
                var finalGrade = await GetStudentSubjectFinalGradeAsync(studentId, subjectId).ConfigureAwait(false);

                if (finalGrade != null)
                {
                    return;
                }

                var student = await _studentsService.GetStudentAsync(studentId).ConfigureAwait(false);

                if (student == null)
                {
                    return;
                }

                var gradebook = await _gradebooksService.GetGradebookByGroupAsync(student.Group.Id, subjectId)
                    .ConfigureAwait(false);

                if (gradebook == null)
                {
                    return;
                }

                if (gradebook.Teachers.All(t => t.Id != teacherId))
                {
                    return;
                }

                var currentGradeTotal =
                    await GetStudentCourseCurrentGradeTotalAsync(studentId, subjectId).ConfigureAwait(false);

                if (currentGradeTotal + grade.Value > MaxFinalGradeValue)
                {
                    return;
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
                return;
            }

            var finalGrade = await GetStudentSubjectFinalGradeAsync(grade.StudentRefId, grade.TeacherRefId)
                .ConfigureAwait(false);

            if (finalGrade != null)
            {
                return;
            }

            var gradebook = await _gradebooksService.GetGradebookAsync(grade.GradebookRefId)
                .ConfigureAwait(false);

            if (gradebook == null)
            {
                return;
            }

            if (gradebook.Teachers.All(t => t.Id != teacherId))
            {
                return; // access denied
            }

            _gradesUnitOfWork.Repository.Delete(grade);

            await _gradesUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task ConfirmStudentCourseFinalGradeAsync(int studentId, int teacherId, int subjectId)
        {
            var student = await _studentsService.GetStudentAsync(studentId).ConfigureAwait(false);

            if (student == null)
            {
                return;
            }

            var gradebook = await _gradebooksService.GetGradebookByGroupAsync(student.Group.Id, subjectId)
                .ConfigureAwait(false);

            if (gradebook == null)
            {
                return;
            }

            if (gradebook.Teachers.All(t => t.Id != teacherId))
            {
                return;
            }

            using (var transaction = await _finalGradesUnitOfWork.BeginTransactionAsync(IsolationLevel.RepeatableRead)
                .ConfigureAwait(false))
            {
                var currentGradeTotal =
                    await GetStudentCourseCurrentGradeTotalAsync(studentId, subjectId).ConfigureAwait(false);

                if (currentGradeTotal < MinFinalGradeValue)
                {
                    return; // throw
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