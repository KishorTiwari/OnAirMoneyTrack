using Omack.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Omack.Data.Models;
using System.Linq.Expressions;
using Omack.Data.Infrastructure;
using Omack.Services.Models;
using System.Linq;
using Omack.Core.Models;

namespace Omack.Services.ServiceImplementations
{
    public class GroupService : IGroupService
    {
        private UnitOfWork _unitOfWork;
        private UserService _userService;

        public GroupService(UnitOfWork unitOfWork, UserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public Result<GroupServiceModel> Add(GroupServiceModel group, CurrentUser currentUser)
        {
            var result = new Result<GroupServiceModel>();
            try
            {
                var newGroup = new Group()
                {
                    Name = group.Name,
                    IsActive = true,
                    MediaId = group.MediaId,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = currentUser.Id,
                    UpdatedOn = DateTime.UtcNow,
                    UpdatedBy = currentUser.Id
                };              
                _unitOfWork.GroupRepository.Add(newGroup);
                _unitOfWork.Save();

                var newGroupUser = new Group_User()
                {
                    GroupId = newGroup.Id,
                    UserId = currentUser.Id,
                    IsActive = true
                };
                _unitOfWork.GroupUserRepository.Add(newGroupUser);
                _unitOfWork.Save();
                group.Id = newGroup.Id;
                result.IsSuccess = true;
                result.Data = group;

                return result;
            }
            catch(Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Sorry. Something went wrong when adding a group.";
                return result;
            }          
        }
        public Result<GroupServiceModel> Delete(int Id, CurrentUser currentUser)
        {
            throw new NotImplementedException();
        }
        public Result<IEnumerable<GroupServiceModel>> GetAllByUserId(CurrentUser currentUser)
        {
            var result = new Result<IEnumerable<GroupServiceModel>>();
            try
            {
                var groups = _unitOfWork.GroupRepository.GetAll(x=>x.Group_Users.All(y=>y.UserId == currentUser.Id));
                if (groups.Any())
                {
                    var groupService = groups.Select(group => new GroupServiceModel
                    {
                        Id = group.Id,
                        Name = group.Name,
                        IsActive = group.IsActive,
                        MediaId = group.MediaId,
                        CreatedOn = group.CreatedOn,
                        CreatedBy = group.CreatedBy,
                        UpdatedOn = group.UpdatedOn,
                        UpdatedBy = group.UpdatedBy
                    });
                    result.IsSuccess = true;
                    result.Data = groupService;
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Sorry. You don't have any groups";
                    return result;
                }
            }
            catch(Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Something went wrong while fetching data from database";
                return result;
            }
        }
        public Result<GroupServiceModel> GetById(int id, CurrentUser currentUser)
        {
            var result = new Result<GroupServiceModel>();
            try
            {
                var group = _unitOfWork.GroupRepository.GetById(id);
            }
            catch(Exception ex)
            {

            }
        }
        public Result<GroupServiceModel> Update(GroupServiceModel group, CurrentUser currentUser)
        {
            throw new NotImplementedException();
        }
        public Result<IEnumerable<GroupServiceModel>> GetAllGroupsByUserId(CurrentUser currentUser)
        {
            var result = new Result<IEnumerable<GroupServiceModel>>();
            try
            {
                var groups = _unitOfWork.GroupRepository.GetAllGroupsByUserId(currentUser.Id);
                if (groups.Any())
                {
                    var groupService = groups.Select(x => new GroupServiceModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        IsActive = x.IsActive,
                        MediaId = x.MediaId
                    });
                    result.IsSuccess = true;
                    result.Data = groupService;
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Sorry. You don't have any groups.";
                    return result;
                }
            }
            catch(Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Sorry. Something went wrong when fetching data from server.";
                return result;
            }           
        }
    }
}
