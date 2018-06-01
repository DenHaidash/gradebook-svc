using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.Common.Exceptions;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.DAL.UoW.Abstractions;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;
using GradeBook.Services.Helpers;

namespace GradeBook.Services
{
    public sealed class TeacherCoursesService : ITeacherCoursesService
    {
        private readonly IUnitOfWork<ITeacherGradebookRepository> _teacherGradebookUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IGradebooksService _gradebooksService;

        public TeacherCoursesService(IUnitOfWork<ITeacherGradebookRepository> teacherGradebookUnitOfWork, IMapper mapper, IGradebooksService gradebooksService)
        {
            _teacherGradebookUnitOfWork = teacherGradebookUnitOfWork;
            _mapper = mapper;
            _gradebooksService = gradebooksService;
        }
        
        public async Task<IEnumerable<GroupSubjectsDto>> GetTeacherSemesterGroupsAsync(int teacherId, int year, int semester)
        {
            var groupsSubjects = (await _teacherGradebookUnitOfWork.Repository
                .GetTeacherSemesterGroups(teacherId, year, semester)
                .ConfigureAwait(false))
                .GroupBy(g => g.Semester.Group.Id, s => new { s.Semester.Group, s.Subject })
                .Select(g => new GroupSubjectsDto
                {
                    Group = _mapper.Map<GroupDto>(g.FirstOrDefault()?.Group),
                    Subjects = _mapper.Map<IEnumerable<SubjectDto>>(g.Select(s => s.Subject))
                });
                
            return groupsSubjects;
        }

        public async Task<IEnumerable<GroupSubjectsDto>> GetTeacherCurrentSemesterGroupsAsync(int teacherId)
        {
            var semesterData = SemestersHelper.IdentifySemester(DateTime.Now);

            return await GetTeacherSemesterGroupsAsync(teacherId, semesterData.year, semesterData.semester)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<SubjectDto>> GetTeacherSemesterGroupCoursesAsync(int teacherId, int year, int semester, int groupId)
        {
            var subjects = (await _teacherGradebookUnitOfWork.Repository
                .GetAllAsync(s => s.TeacherRefId == teacherId
                                  && s.Gradebook.Semester.GroupRefId == groupId
                                  && s.Gradebook.Semester.StartsAt.Year == (semester == 2 ? year + 1 : year)
                                  && s.Gradebook.Semester.SemesterNumber == semester)
                .ConfigureAwait(false)).Select(s => s.Gradebook.Subject);

            return _mapper.Map<IEnumerable<SubjectDto>>(subjects);
        }

        public async Task<IEnumerable<SubjectDto>> GetTeacherCurrentSemesterGroupCoursesAsync(int teacherId, int groupId)
        {
            var semesterData = SemestersHelper.IdentifySemester(DateTime.Now);

            return await GetTeacherSemesterGroupCoursesAsync(teacherId, semesterData.year, semesterData.semester, groupId)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<TeacherDto>> GetTeachersOfCourseAsync(int groupId, int subjectId)
        {
            var gradebook = await _gradebooksService.GetGradebookByGroupAsync(groupId, subjectId).ConfigureAwait(false);
            
            if (gradebook == null)
            {
                throw new ResourceNotFoundException($"Журнал групи {groupId} по предмету {subjectId} не знайдено");
            }
            
            return _mapper.Map<IEnumerable<TeacherDto>>(gradebook.Teachers);
        }

        public async Task AssignTeacherToCourseAsync(int teacherId, int groupId, int subjectId)
        {
            var gradebook = await _gradebooksService.GetGradebookByGroupAsync(groupId, subjectId).ConfigureAwait(false);

            if (gradebook == null)
            {
                throw new ResourceNotFoundException($"Журнал групи {groupId} по предмету {subjectId} не знайдено");
            }
            
            var newTeacherAssignment = new GradebookTeacher
            {
                GradebookRefId = gradebook.Id,
                TeacherRefId = teacherId
            };
            
            _teacherGradebookUnitOfWork.Repository.Add(newTeacherAssignment);

            await _teacherGradebookUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UnassignTeacherFromCourseAsync(int teacherId, int year, int semester, int groupId, int subjectId)
        {
            var teacherAssignment = (await _teacherGradebookUnitOfWork.Repository.GetAllAsync(s =>
                s.TeacherRefId == teacherId
                && s.Gradebook.Semester.GroupRefId == groupId
                && s.Gradebook.SubjectRefId == subjectId
                && s.Gradebook.Semester.StartsAt.Year == (semester == 2 ? year + 1 : year)
                && s.Gradebook.Semester.SemesterNumber == semester).ConfigureAwait(false)).FirstOrDefault();

            if (teacherAssignment == null)
            {
                return;
            }
            
            _teacherGradebookUnitOfWork.Repository.Delete(teacherAssignment);

            await _teacherGradebookUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}