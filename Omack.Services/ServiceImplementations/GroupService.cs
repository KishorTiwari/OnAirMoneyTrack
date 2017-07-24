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
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Omack.Services.ServiceImplementations
{
    public class GroupService : IGroupService
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ILogger<GroupService> _logger;
        public GroupService(UnitOfWork unitOfWork, IMapper mapper, ILogger<GroupService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public Result<GroupServiceModel> Add(GroupServiceModel group, CurrentUser currentUser)
        {
            var result = new Result<GroupServiceModel>();           
            try
            {
                //No mapping. Because system properties shouldn't be null. 
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

                //return mapped new group
                var mappedNewGroup = _mapper.Map<GroupServiceModel>(newGroup); 

                result.IsSuccess = true;
                result.Data = mappedNewGroup;
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = "Sorry. Something went wrong when adding a group.";
                return result;
            }          
        }
        public Result<GroupServiceModel> Delete(int Id, CurrentUser currentUser)
        {
            var result = new Result<GroupServiceModel>();
            try
            {
                var dbGroup = _unitOfWork.GroupRepository.GetSingle(x =>x.Id == Id && x.IsActive && x.Id == Id && x.Group_Users.All(y => y.UserId == currentUser.Id && y.IsActive == true));
                if(dbGroup != null)
                {
                    //IsActive to false for group
                    dbGroup.IsActive = false;
                    dbGroup.UpdatedBy = currentUser.Id;
                    dbGroup.UpdatedOn = DateTime.UtcNow;

                    //IsActive to false for GroupUser 
                    var dbGroupUser = _unitOfWork.GroupUserRepository.GetSingle(x => x.Group.Id == dbGroup.Id && x.Group.IsActive);
                    dbGroupUser.IsActive = false;
                    dbGroupUser.UpdatedBy = currentUser.Id;
                    dbGroupUser.UpdatedOn = DateTime.UtcNow;

                    _unitOfWork.GroupRepository.Update(dbGroup);
                    _unitOfWork.Save();

                    //return deleted Group
                    var updatedGroup = _mapper.Map<GroupServiceModel>(dbGroup);                  
                    result.IsSuccess = true;
                    result.Data = updatedGroup;

                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Sorry. This group doesn't belongs to you or it doesn't exists.";
                    return result;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = "Something went wrong while deleting group.";
                return result;
            }
        }
        public Result<IQueryable<GroupServiceModel>> GetAll(CurrentUser currentUser)
        {
            var result = new Result<IQueryable<GroupServiceModel>>();
            try
            {
                var groups = _unitOfWork.GroupRepository.GetAll(x=> x.IsActive == true && x.Group_Users.All(y=>y.UserId == currentUser.Id && x.IsActive == true));
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
                _logger.LogError(ex.InnerException.Message);
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
                var dbGroup = _unitOfWork.GroupRepository.GetSingle(x =>x.Id == id && x.IsActive == true && x.Group_Users.All(y => y.IsActive == true && y.UserId == currentUser.Id));
                if(dbGroup == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Sorry. This group doesn't exists or it doesn't belongs to you.";
                    return result;
                }
                else
                {             
                    //Use mapper because data is being returned from database. Hence, all properties has data.
                    var group = _mapper.Map<GroupServiceModel>(dbGroup);
                    result.IsSuccess = true;
                    result.Data = group;
                    return result;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = "Something went wrong while fetching data.";
                return result;
            }
        }
        public Result<GroupServiceModel> Update(GroupServiceModel groupModel, CurrentUser currentUser)
        {
            var result = new Result<GroupServiceModel>();
            try
            {
                var group = _mapper.Map<Group>(groupModel);

                //append system properties as mapper map to null
                group.UpdatedBy = currentUser.Id;
                group.UpdatedOn = DateTime.UtcNow;

                _unitOfWork.GroupRepository.Update(group);
                _unitOfWork.Save();

                //map it to GroupServiceModel 
                var updatedGroup = _mapper.Map<GroupServiceModel>(group);
                result.IsSuccess = true;
                result.Data = updatedGroup;
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = "Something went wrong while updating group";
                return result;
            }
        }
    }
}
