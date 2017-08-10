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
using Omack.Core.Constants;

namespace Omack.Services.ServiceImplementations
{
    public class GroupService : IGroupService
    {
        //private fields
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ILogger<GroupService> _logger;

        //Constructor
        public GroupService(UnitOfWork unitOfWork, IMapper mapper, ILogger<GroupService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        //Functions
        public Result<GroupServiceModel> Add(GroupServiceModel group, int userId)
        {
            var result = new Result<GroupServiceModel>();

            //initialize transaction 
            var transaction = _unitOfWork.BeginTransaction();
            try
            {
                //No mapping. Because system properties shouldn't be null. 
                var newGroup = new Group()
                {
                    Name = group.Name,
                    IsActive = true,
                    MediaId = group.MediaId,
                    CreatedOn = Application.CurrentDate,
                    CreatedBy = userId,
                    UpdatedOn = Application.CurrentDate,
                    UpdatedBy = userId
                };
                _unitOfWork.GroupRepository.Add(newGroup);
                _unitOfWork.Save();

                var newGroupUser = new Group_User()
                {
                    GroupId = newGroup.Id,
                    UserId = userId,
                    IsActive = true
                };
                _unitOfWork.GroupUserRepository.Add(newGroupUser);
                _unitOfWork.Save();

                //commit transaction
                transaction.commit();

                //return mapped new group
                var mappedNewGroup = _mapper.Map<GroupServiceModel>(newGroup); 

                result.IsSuccess = true;
                result.Data = mappedNewGroup;
                return result;
            }
            catch(Exception ex)
            {
                //rollback transaction
                transaction.Rollback();
                _logger.LogError(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Add;
                return result;
            }          
        }

        public Result<GroupServiceModel> Delete(int Id, int userId)
        {
            var result = new Result<GroupServiceModel>();

            //initialize transaction
            var transaction = _unitOfWork.BeginTransaction();

            try
            {               
                var dbGroup = _unitOfWork.GroupRepository.GetSingle(x =>x.Id == Id && x.IsActive && x.Id == Id && x.Group_Users.All(y => y.UserId == userId && y.IsActive == true));
                if(dbGroup != null)
                {                   
                    //IsActive to false for group
                    dbGroup.IsActive = false;
                    dbGroup.UpdatedBy = userId;
                    dbGroup.UpdatedOn = Application.CurrentDate;
                    _unitOfWork.Save();

                    //IsActive to false for GroupUser 
                    var dbGroupUser = _unitOfWork.GroupUserRepository.GetSingle(x => x.Group.Id == dbGroup.Id && x.UserId == userId);
                    dbGroupUser.IsActive = false;
                    dbGroupUser.UpdatedBy = userId;
                    dbGroupUser.UpdatedOn = Application.CurrentDate;

                   // _unitOfWork.GroupRepository.Update(dbGroup);
                    _unitOfWork.Save();

                    //commit transaction
                    transaction.commit();

                    //return deleted Group
                    var updatedGroup = _mapper.Map<GroupServiceModel>(dbGroup);                     
                    result.IsSuccess = true;
                    result.Data = updatedGroup;
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = ErrorMessage.DeleteUnAuth;
                    return result;
                }
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Delete;
                return result;
            }
        }

        public Result<IQueryable<GroupServiceModel>> GetAll(int userId)
        {
            var result = new Result<IQueryable<GroupServiceModel>>();
            try
            {
                var groups = _unitOfWork.GroupRepository.GetAll(x=> x.IsActive == true && x.Group_Users.All(y=>y.UserId == userId && x.IsActive == true));
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
                    result.ErrorMessage = ErrorMessage.GetUnAuth;
                    return result;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Get;
                return result;
            }
        }

        public Result<GroupServiceModel> GetById(int id, int userId)
        {
            var result = new Result<GroupServiceModel>();
            try
            {
                var dbGroup = _unitOfWork.GroupRepository.GetSingle(x =>x.Id == id && x.IsActive == true && x.Group_Users.All(y => y.IsActive == true && y.UserId == userId));
                if(dbGroup == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = ErrorMessage.GetUnAuth;
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
                result.ErrorMessage = ErrorMessage.Get;
                return result;
            }
        }

        public Result<GroupServiceModel> Update(GroupServiceModel groupModel, int userId)
        {
            var result = new Result<GroupServiceModel>();
            try
            {
                var groupEntity = _unitOfWork.GroupRepository.GetSingle(x => x.Id == groupModel.Id && x.IsActive);

                if(groupEntity != null)
                {
                    var group = _mapper.Map<Group>(groupModel);

                    //append system properties as mapper map to null
                    group.UpdatedBy = userId;
                    group.UpdatedOn = Application.CurrentDate;

                    _unitOfWork.GroupRepository.Update(group);
                    _unitOfWork.Save();

                    //map it to GroupServiceModel 
                    var updatedGroup = _mapper.Map<GroupServiceModel>(group);
                    result.IsSuccess = true;
                    result.Data = updatedGroup;
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = ErrorMessage.UpdateUnAuth;
                    return result;
                }               
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Update;
                return result;
            }
        }
    }
}
