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
using Omack.Services.Models.Group;
using Microsoft.AspNetCore.Http;

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
        public Result<GroupServiceGM> Add(GroupServicePM group, CurrentUser currentUser)
        {
            var result = new Result<GroupServiceGM>();

            //initialize transaction 
            var transaction = _unitOfWork.BeginTransaction();
            try
            {
                //No mapping. Because system properties shouldn't be null. 
                var newGroup = new Group()
                {
                    Name = group.Name,
                    IsActive = true,
                    MediaId = 2,
                    CreatedOn = Application.CurrentDate,
                    CreatedBy = currentUser.Id
                };
                _unitOfWork.GroupRepository.Add(newGroup);
                _unitOfWork.Save();

                var newGroupUser = new Group_User()
                {
                    GroupId = newGroup.Id,
                    UserId = currentUser.Id,
                    IsActive = true,
                    IsAdmin = true,
                };
                _unitOfWork.GroupUserRepository.Add(newGroupUser);
                _unitOfWork.Save();

                //commit transaction
                transaction.commit();

                //return mapped new group
                var mediaUrl = _unitOfWork.MediaRepository.GetById((int)newGroup.MediaId).Url;
                var groupGM = new GroupServiceGM()
                {
                    Id = newGroup.Id,
                    Name = newGroup.Name,
                    IsAdmin = newGroupUser.IsAdmin,
                    MediaUrl = mediaUrl,
                    CreatedOn = newGroup.CreatedOn,
                    CreatedBy = currentUser.Name
                };              
                result.IsSuccess = true;
                result.Data = groupGM;
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

        public Result<GroupServiceGM> Delete(int Id, CurrentUser currentUser)
        {
            var result = new Result<GroupServiceGM>();

            //initialize transaction
            var transaction = _unitOfWork.BeginTransaction();

            try
            {               
                var dbGroup = _unitOfWork.GroupRepository.GetSingle(x =>x.Id == Id && x.IsActive && x.Id == Id && x.Group_Users.All(y => y.UserId == currentUser.Id && y.IsActive == true));
                if(dbGroup != null)
                {                   
                    //IsActive to false for group
                    dbGroup.IsActive = false;
                    dbGroup.UpdatedBy = currentUser.Id;
                    dbGroup.UpdatedOn = Application.CurrentDate;
                    _unitOfWork.Save();

                    //IsActive to false for GroupUser 
                    var dbGroupUser = _unitOfWork.GroupUserRepository.GetSingle(x => x.Group.Id == dbGroup.Id && x.UserId == currentUser.Id);
                    dbGroupUser.IsActive = false;
                    dbGroupUser.UpdatedBy = currentUser.Id;
                    dbGroupUser.UpdatedOn = Application.CurrentDate;

                   // _unitOfWork.GroupRepository.Update(dbGroup);
                    _unitOfWork.Save();

                    //commit transaction
                    transaction.commit();

                    //return deleted Group
                    var groupServiceGM = new GroupServiceGM()
                    {
                        Id = dbGroup.Id,
                        Name = dbGroup.Name,
                        MediaUrl = dbGroup.Media.Url,
                        CreatedOn = dbGroup.CreatedOn,
                        CreatedBy = currentUser.Name,
                        UpdatedOn = dbGroup.UpdatedOn,
                        UpdatedBy = currentUser.Name
                    };                  
                    result.IsSuccess = true;
                    result.Data = groupServiceGM;
                    result.StatusCodes = StatusCodes.Status200OK;
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

        public Result<IList<GroupServiceGM>> GetAll(CurrentUser currentUser)
        {
            var result = new Result<IList<GroupServiceGM>>();
            try
            {
                var groups = _unitOfWork.GroupRepository.GetAll(x=> x.IsActive == true && x.Group_Users.All(y=>y.UserId == currentUser.Id && x.IsActive == true));
                if (groups.Any())
                {
                    var groupService = groups.Select(group => new GroupServiceGM
                    {
                        Id = group.Id,
                        Name = group.Name,
                        MediaUrl = group.Media.Url,
                        CreatedOn = group.CreatedOn,
                        CreatedBy = group.Group_Users.FirstOrDefault(gu=>gu.UserId == currentUser.Id).User.UserName,
                        UpdatedOn = group.UpdatedOn,
                        UpdatedBy = group.Group_Users.FirstOrDefault(gu => gu.UserId == currentUser.Id).User.UserName,
                        IsAdmin = group.Group_Users.FirstOrDefault(gp => gp.UserId == currentUser.Id).IsAdmin                     
                    }).ToList();

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

        public Result<GroupServiceGM> GetById(int id, CurrentUser currentUser)
        {
            var result = new Result<GroupServiceGM>();
            try
            {
                var dbGroup = _unitOfWork.GroupRepository.GetSingle(x =>x.Id == id && x.IsActive == true && x.Group_Users.All(y => y.IsActive == true && y.UserId == currentUser.Id));
                if(dbGroup == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = ErrorMessage.GetUnAuth;
                    result.StatusCodes = StatusCodes.Status401Unauthorized;
                    return result;
                }
                else
                {
                    //Use mapper because data is being returned from database. Hence, all properties has data.
                    var groupServeGM = new GroupServiceGM()
                    {
                        Id = dbGroup.Id,
                        Name = dbGroup.Name,
                        MediaUrl = dbGroup.Media.Url,
                        CreatedOn = dbGroup.CreatedOn,
                        CreatedBy = dbGroup.Group_Users.FirstOrDefault(gu => gu.UserId == currentUser.Id).User.UserName,
                        UpdatedOn = dbGroup.UpdatedOn,
                        UpdatedBy = dbGroup.Group_Users.FirstOrDefault(gu => gu.UserId == currentUser.Id).User.UserName,
                        IsAdmin = dbGroup.Group_Users.FirstOrDefault(gp => gp.UserId == currentUser.Id).IsAdmin,
                    };

                    result.IsSuccess = true;
                    result.Data = groupServeGM;
                    result.StatusCodes = StatusCodes.Status200OK; ;
                    return result;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Get;
                result.StatusCodes = StatusCodes.Status500InternalServerError;
                return result;
            }
        }

        public Result<GroupServiceGM> Update(GroupServicePM group, CurrentUser currentUser)
        {
            throw new NotImplementedException();
        }

        //public Result<GroupServiceGM> Update(GroupServicePM groupModel, CurrentUser currentUser)
        //{
        //    var result = new Result<GroupServiceModel>();
        //    try
        //    {
        //        var groupEntity = _unitOfWork.GroupRepository.GetSingle(x => x.Id == groupModel.Id && x.IsActive);

        //        if(groupEntity != null)
        //        {
        //            var group = _mapper.Map<Group>(groupModel);

        //            //append system properties as mapper map to null
        //            group.UpdatedBy = currentUser.Id;
        //            group.UpdatedOn = Application.CurrentDate;

        //            _unitOfWork.GroupRepository.Update(group);
        //            _unitOfWork.Save();

        //            //map it to GroupServiceModel 
        //            var updatedGroup = _mapper.Map<GroupServiceModel>(group);
        //            result.IsSuccess = true;
        //            result.Data = updatedGroup;
        //            return result;
        //        }
        //        else
        //        {
        //            result.IsSuccess = false;
        //            result.ErrorMessage = ErrorMessage.UpdateUnAuth;
        //            return result;
        //        }               
        //    }
        //    catch(Exception ex)
        //    {
        //        _logger.LogError(ex.InnerException.Message);
        //        result.IsSuccess = false;
        //        result.ErrorMessage = ErrorMessage.Update;
        //        return result;
        //    }
        //}
    }
}
