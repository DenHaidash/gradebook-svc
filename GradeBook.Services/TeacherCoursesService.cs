using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.DAL.UoW.Base;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;

namespace GradeBook.Services
{
    public class TeacherCoursesService : ITeacherCoursesService
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
        
        public async Task<IEnumerable<GroupDto>> GetTeacherSemesterGroupsAsync(int teacherId, int year, int semester)
        {
            var groups = await _teacherGradebookUnitOfWork.Repository
                .GetTeacherSemesterGroups(teacherId, year, semester)
                .ConfigureAwait(false);
                
            return _mapper.Map<IEnumerable<GroupDto>>(groups);
        }

        public async Task<IEnumerable<SubjectDto>> GetTeacherSemesterGroupCoursesAsync(int teacherId, int year, int semester, int groupId)
        {
            var subjects = (await _teacherGradebookUnitOfWork.Repository
                .GetAllAsync(s => s.TeacherRefId == teacherId
                                  && s.Gradebook.Semester.GroupRefId == groupId
                                  && s.Gradebook.Semester.StartsAt.Year == year
                                  && s.Gradebook.Semester.SemesterNumber == semester)
                .ConfigureAwait(false)).Select(s => s.Gradebook.Subject);

            return _mapper.Map<IEnumerable<SubjectDto>>(subjects);
        }

        public async Task AssignTeacherToCourseAsync(int teacherId, int year, int semester, int groupId, int subjectId)
        {
            var gradebook = await _gradebooksService.GetGradebookAsync(year, semester, subjectId).ConfigureAwait(false);

            if (gradebook == null)
            {
                return;
            }
            
            var newTeacherAssignment = new GradebookTeacher
            {
                GradebookRefId = gradebook.Id,
                TeacherRefId = teacherId
            };
            
            _teacherGradebookUnitOfWork.Repository.Add(newTeacherAssignment);

            await _teacherGradebookUnitOfWork.SaveAsync().ConfigureAwait(false);
        }

        public async Task UnassignTeacherFromCourseAsync(int teacherId, int year, int semester, int groupId, int subjectId)
        {
            var teacherAssignment = (await _teacherGradebookUnitOfWork.Repository.GetAllAsync(s =>
                s.TeacherRefId == teacherId
                && s.Gradebook.Semester.GroupRefId == groupId
                && s.Gradebook.SubjectRefId == subjectId
                && s.Gradebook.Semester.StartsAt.Year == year
                && s.Gradebook.Semester.SemesterNumber == semester).ConfigureAwait(false)).FirstOrDefault();

            if (teacherAssignment == null)
            {
                return;
            }
            
            _teacherGradebookUnitOfWork.Repository.Delete(teacherAssignment);

            await _teacherGradebookUnitOfWork.SaveAsync().ConfigureAwait(false);
        }
    }
}