using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface IGroupsService
    {
        Task<GroupDto> GetGroupAsync(int id);
        Task<IEnumerable<GroupDto>> GetGroupsAsync();
        Task<int> CreateGroupAsync(GroupDto group, DateTime educationStartedAt);
        Task UpdateGroupAsync(GroupDto group);
        Task RemoveGroupAsync(int groupId);
    }
}