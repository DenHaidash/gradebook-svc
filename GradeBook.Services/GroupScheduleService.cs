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
    public sealed class GroupScheduleService : IGroupScheduleService
    {
        private readonly IUnitOfWork<ISemesterSubjectsRepository> _semesterScheduleUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IGroupSemestersService _groupSemestersService;
        private readonly IGradebooksService _gradebooksService;
        private readonly IStudentsService _studentsService;
        private readonly IGroupsService _groupsService;

        public GroupScheduleService(IUnitOfWork<ISemesterSubjectsRepository> semesterScheduleUnitOfWork, IMapper mapper,
            IGroupSemestersService groupSemestersService, IGradebooksService gradebooksService,
            IStudentsService studentsService, IGroupsService groupsService)
        {
            _semesterScheduleUnitOfWork = semesterScheduleUnitOfWork;
            _mapper = mapper;
            _groupSemestersService = groupSemestersService;
            _gradebooksService = gradebooksService;
            _studentsService = studentsService;
            _groupsService = groupsService;
        }

        public async Task<IEnumerable<SemesterSubjectDto>> GetGroupSemesterSubjects(int groupId, int yearNumber, int semesterNumber)
        {
            var semesterSubjects = await _semesterScheduleUnitOfWork.Repository
                .GetAllAsync(s => s.Semester.GroupRefId == groupId
                                  && s.Semester.StartsAt.Year == (semesterNumber == 2 ? yearNumber + 1 : yearNumber)
                                  && s.Semester.SemesterNumber == semesterNumber).ConfigureAwait(false);

            return _mapper.Map<IEnumerable<SemesterSubjectDto>>(semesterSubjects);
        }

        public async Task<IEnumerable<SemesterSubjectDto>> GetStudentGroupCurrentSemesterSubjects(int studentId)
        {
            var student = await _studentsService.GetStudentAsync(studentId).ConfigureAwait(false);

            if (student == null)
            {
                throw new ResourceNotFoundException($"Студент {studentId} не знайдений");
            }

            var semesterData = SemestersHelper.IdentifySemester(DateTime.Now);
            
            return await GetGroupSemesterSubjects(student.Group.Id, semesterData.year, semesterData.semester).ConfigureAwait(false);
        }

        public async Task<bool> HasGroupSubjectInScheduleAsync(int groupId, int subjectId)
        {
            return await _semesterScheduleUnitOfWork.Repository.HasGroupSubjectInScheduleAsync(groupId, subjectId)
                .ConfigureAwait(false);
        }

        public async Task AddSubjectToSemester(SemesterSubjectDto semesterSubject, int groupId, int yearNumber, int semesterNumber)
        {
            var group = await _groupsService.GetGroupAsync(groupId).ConfigureAwait(false);

            if (group == null)
            {
                throw new ResourceNotFoundException($"Група {groupId} не знайдена");
            }
            
            if (await HasGroupSubjectInScheduleAsync(groupId, semesterSubject.Subject.Id).ConfigureAwait(false))
            {
                throw new ResourceOperationException($"Група {group.Code} вже має предмет {semesterSubject.Subject.Name} у своєму розкладі");
            }
            
            var semester = await _groupSemestersService.GetGroupSemesterAsync(groupId, yearNumber, semesterNumber)
                .ConfigureAwait(false);

            if (semester == null)
            {
                throw new ResourceNotFoundException($"Семестр {yearNumber}/{semesterNumber} для групи {group.Code} не знайдений");
            }

            var newSemesterSubject = _mapper.Map<SemesterSubject>(semesterSubject);
            newSemesterSubject.SemesterRefId = semester.Id;

            using (var transaction = await _semesterScheduleUnitOfWork.BeginTransactionAsync().ConfigureAwait(false))
            {
                _semesterScheduleUnitOfWork.Repository.Add(newSemesterSubject);

                await _semesterScheduleUnitOfWork.SaveChangesAsync().ConfigureAwait(false);

                await _gradebooksService.CreateGradebookAsync(new GradebookDto
                {
                    SemesterId = semester.Id,
                    SubjectId = semesterSubject.Subject.Id
                }).ConfigureAwait(false);
                
                transaction.Commit();
            }
        }

        public async Task DeleteSubjectFromSemester(int subjectId, int groupId, int yearNumber, int semesterNumber)
        {
            var group = await _groupsService.GetGroupAsync(groupId).ConfigureAwait(false);
            
            if (group == null)
            {
                throw new ResourceNotFoundException($"Група {groupId} не знайдена");
            }
            
            var semester = await _groupSemestersService.GetGroupSemesterAsync(groupId, yearNumber, semesterNumber)
                .ConfigureAwait(false);

            if (semester == null)
            {
                throw new ResourceNotFoundException($"Семестр {yearNumber}/{semesterNumber} для групи {group.Code} не знайдений");
            }

            var semesterSubject = (await _semesterScheduleUnitOfWork.Repository
                .GetAllAsync(s => s.SemesterRefId == semester.Id && s.SubjectRefId == subjectId)
                .ConfigureAwait(false)).FirstOrDefault();

            if (semesterSubject == null)
            {
                throw new ResourceNotFoundException($"Предмет {subjectId} у семестрі {semester.Id} не знайдено");
            }

            using (var transaction = await _semesterScheduleUnitOfWork.BeginTransactionAsync().ConfigureAwait(false))
            {
                var gradebook = await _gradebooksService.GetGradebookAsync(semester.Id, subjectId).ConfigureAwait(false);

                await _gradebooksService.RemoveGradebookAsync(gradebook.Id);
                
                _semesterScheduleUnitOfWork.Repository.Delete(semesterSubject);

                await _semesterScheduleUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
                
                transaction.Commit();
            }
        }
    }
}