using Omack.Core.Models;
using Omack.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Services.Services
{
    public interface IAdminService
    {
        Result<GroupServiceModel> MakeGroupAdmin(int userId, int groupId, int currentUserId);
        Result<GroupServiceModel> RemoveFromGroupAdmin(int userId, int groupId, int currentUserId);
        Result<UserServiceModel> DeleteUserFromGroup(int userId, int groupId, int currentUserId);
        Result<UserServiceModel> AddUserToGroup(int userId, int groupId, int currentUserId);
    }
}
