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
    public sealed class GroupsService : IGroupsService
    {
        private readonly IUnitOfWork<IGroupsRepository> _groupsUnitOfWork;
        private readonly IGroupSemestersService _groupSemestersService;
        private readonly IMapper _mapper;

        public GroupsService(IUnitOfWork<IGroupsRepository> groupsUnitOfWork, IGroupSemestersService groupSemestersService, IMapper mapper)
        {
            _groupsUnitOfWork = groupsUnitOfWork;
            _groupSemestersService = groupSemestersService;
            _mapper = mapper;
        }
        
        public async Task<GroupDto> GetGroupAsync(int id)
        {
            var group = await _groupsUnitOfWork.Repository.GetByIdAsync(id).ConfigureAwait(false);

            if (group == null)
            {
                return null;
            }
            
            return _mapper.Map<GroupDto>(group);
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsAsync()
        {
            var groups = await _groupsUnitOfWork.Repository
                .GetAllAsync()
                .ConfigureAwait(false);

            return _mapper.Map<IEnumerable<GroupDto>>(groups);
        }

        public async Task<GroupDto> CreateGroupAsync(GroupDto group, DateTime educationStartedAt)
        {
            using (var transaction = await _groupsUnitOfWork.BeginTransactionAsync().ConfigureAwait(false))
            {
                var newGroup = _mapper.Map<Group>(group);
 
                _groupsUnitOfWork.Repository.Add(newGroup);

                await _groupsUnitOfWork.SaveChangesAsync().ConfigureAwait(false);

                var groupSemesters = SemestersHelper.GenerateSemesters(educationStartedAt.Year).ToList();
                groupSemesters.ForEach(s => { s.GroupId = newGroup.Id; });
            
                await _groupSemestersService.CreateGroupSemestersAsync(groupSemesters).ConfigureAwait(false);

                transaction.Commit();
                
                return await GetGroupAsync(newGroup.Id).ConfigureAwait(false);
            }
        }

        public async Task UpdateGroupAsync(GroupDto group)
        {
            var groupToUpdate = await _groupsUnitOfWork.Repository.GetByIdAsync(group.Id).ConfigureAwait(false);

            if (groupToUpdate == null)
            {
                throw new ResourceNotFoundException($"Group {group.Id} not found");
            }

            groupToUpdate.Code = group.Code;
            groupToUpdate.SpecialityRefId = group.Speciality.Id;

            await _groupsUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task RemoveGroupAsync(int groupId)
        {
            var groupToDelete = await _groupsUnitOfWork.Repository.GetByIdAsync(groupId).ConfigureAwait(false);

            if (groupToDelete == null)
            {
                throw new ResourceNotFoundException($"Group {groupId} not found");
            }

            _groupsUnitOfWork.Repository.Delete(groupToDelete);
            
            await _groupsUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}