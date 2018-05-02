using System.Collections.Generic;
using System.Data;
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
    public class GroupScheduleService : IGroupScheduleService
    {
        private readonly IUnitOfWork<ISemesterSubjectsRepository> _semesterScheduleUnitOfWork;
        private readonly IMapper _mapper;
        private readonly IGroupSemestersService _groupSemestersService;
        private readonly IGradebooksService _gradebooksService;

        public GroupScheduleService(IUnitOfWork<ISemesterSubjectsRepository> semesterScheduleUnitOfWork, IMapper mapper, IGroupSemestersService groupSemestersService, IGradebooksService gradebooksService)
        {
            _semesterScheduleUnitOfWork = semesterScheduleUnitOfWork;
            _mapper = mapper;
            _groupSemestersService = groupSemestersService;
            _gradebooksService = gradebooksService;
        }

        public async Task<IEnumerable<SemesterSubjectDto>> GetGroupSemestedSubjects(int groupId, int yearNumber, int semesterNumber)
        {
            var semesterSubjects = await _semesterScheduleUnitOfWork.Repository
                .GetAllAsync(s => s.Semester.GroupRefId == groupId
                                  && s.Semester.StartsAt.Year == yearNumber
                                  && s.Semester.SemesterNumber == semesterNumber).ConfigureAwait(false);

            return _mapper.Map<IEnumerable<SemesterSubjectDto>>(semesterSubjects);
        }

        public async Task AddSubjectToSemester(SemesterSubjectDto semesterSubject, int groupId, int yearNumber, int semesterNumber)
        {
            var semester = await _groupSemestersService.GetGroupSemesterAsync(groupId, yearNumber, semesterNumber)
                .ConfigureAwait(false);

            if (semester == null)
            {
                return;
            }

            var newSemesterSubject = _mapper.Map<SemesterSubject>(semesterSubject);
            newSemesterSubject.SemesterRefId = semester.Id;

            using (var transaction = await _semesterScheduleUnitOfWork.InTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false))
            {
                _semesterScheduleUnitOfWork.Repository.Add(newSemesterSubject);

                await _semesterScheduleUnitOfWork.SaveAsync().ConfigureAwait(false);

                await _gradebooksService.CreateGradebookAsync(new GradebookDto()
                {
                    SemesterId = semester.Id,
                    SubjectId = semesterSubject.Subject.Id
                }).ConfigureAwait(false);
                
                transaction.Commit();
            }
        }

        public async Task DeleteSubjectFromSemester(int subjectId, int groupId, int yearNumber, int semesterNumber)
        {
            var semester = await _groupSemestersService.GetGroupSemesterAsync(groupId, yearNumber, semesterNumber)
                .ConfigureAwait(false);

            if (semester == null)
            {
                return;
            }

            var semesterSubject = (await _semesterScheduleUnitOfWork.Repository
                .GetAllAsync(s => s.SemesterRefId == semester.Id && s.SubjectRefId == subjectId)
                .ConfigureAwait(false)).FirstOrDefault();

            if (semesterSubject == null)
            {
                return;
            }

            using (var transaction = await _semesterScheduleUnitOfWork.InTransactionAsync(IsolationLevel.ReadCommitted)
                .ConfigureAwait(false))
            {
                _semesterScheduleUnitOfWork.Repository.Delete(semesterSubject);

                await _semesterScheduleUnitOfWork.SaveAsync().ConfigureAwait(false);

                var gradebook = await _gradebooksService.GetGradebookAsync(semester.Id, subjectId).ConfigureAwait(false);

                await _gradebooksService.RemoveGradebookAsync(gradebook.Id);
                
                transaction.Commit();
            }
        }
    }
}