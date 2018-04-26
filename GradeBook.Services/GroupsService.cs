using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly IGroupsRepository _groupsRepository;

        public GroupsService(IGroupsRepository groupsRepository)
        {
            _groupsRepository = groupsRepository;
        }
        
        public async Task<GroupDto> GetGroupAsync(int id)
        {
            var group = await _groupsRepository.GetByIdAsync(id).ConfigureAwait(false);

            if (group == null)
            {
                return null;
            }
            
            return new GroupDto()
            {
                Id = group.Id,
                Code = group.Code,
                Specialty = new SpecialtyDto()
                {
                    Id = group.Specialty.Id,
                    Name = group.Specialty.Name
                }
            };
        }

        public async Task<IEnumerable<GroupDto>> GetGroupsAsync()
        {
            var groups = await _groupsRepository.GetAll().ToListAsync();

            return groups.Select(g => new GroupDto()
            {
                Id = g.Id,
                Code = g.Code,
                Specialty = new SpecialtyDto()
                {
                    Id = g.Specialty.Id,
                    Code = g.Specialty.Code,
                    Name = g.Specialty.Name
                }
            });
        }

        public async Task<int> CreateGroupAsync(GroupDto group)
        {
            var newGroup = new Group()
            {
                Code = group.Code,
                SpecialityRefId = group.Specialty.Id
            };
            
            _groupsRepository.Add(newGroup);
            
            // save

            return newGroup.Id;
        }

        public async Task UpdateGroupAsync(GroupDto group)
        {
            var groupToUpdate = await _groupsRepository.GetByIdAsync(group.Id).ConfigureAwait(false);

            if (groupToUpdate == null)
            {
                return;
            }

            groupToUpdate.Code = group.Code;
            groupToUpdate.SpecialityRefId = group.Specialty.Id;
            
            // save
        }

        public async Task RemoveGroupAsync(int groupId)
        {
            var groupToUpdate = await _groupsRepository.GetByIdAsync(groupId).ConfigureAwait(false);

            if (groupToUpdate == null)
            {
                return;
            }

            groupToUpdate.IsDeleted = true;
            
            // save
        }
    }
}