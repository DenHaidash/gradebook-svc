using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.DAL.UoW.Base;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly IUnitOfWork<IGroupsRepository> _groupsUnitOfWork;
        private readonly IMapper _mapper;

        public GroupsService(IUnitOfWork<IGroupsRepository> groupsUnitOfWork, IMapper mapper)
        {
            _groupsUnitOfWork = groupsUnitOfWork;
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
            var groups = await _groupsUnitOfWork.Repository.GetAllAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<GroupDto>>(groups);
        }

        public async Task<int> CreateGroupAsync(GroupDto group)
        {
            var newGroup = _mapper.Map<Group>(group);
            
            _groupsUnitOfWork.Repository.Add(newGroup);

            await _groupsUnitOfWork.SaveAsync().ConfigureAwait(false);

            return newGroup.Id;
        }

        public async Task UpdateGroupAsync(GroupDto group)
        {
            var groupToUpdate = await _groupsUnitOfWork.Repository.GetByIdAsync(group.Id).ConfigureAwait(false);

            if (groupToUpdate == null)
            {
                return;
            }

            groupToUpdate.Code = group.Code;
            groupToUpdate.SpecialityRefId = group.Specialty.Id;

            await _groupsUnitOfWork.SaveAsync().ConfigureAwait(false);
        }

        public async Task RemoveGroupAsync(int groupId)
        {
            var groupToUpdate = await _groupsUnitOfWork.Repository.GetByIdAsync(groupId).ConfigureAwait(false);

            if (groupToUpdate == null)
            {
                return;
            }

            groupToUpdate.IsDeleted = true;
            
            await _groupsUnitOfWork.SaveAsync().ConfigureAwait(false);
        }
    }
}