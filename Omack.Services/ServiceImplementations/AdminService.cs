using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Omack.Core.Constants;
using Omack.Core.Models;
using Omack.Data.DAL;
using Omack.Data.Infrastructure;
using Omack.Data.Models;
using Omack.Services.Models;
using Omack.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Services.ServiceImplementations
{
    public class AdminService: IAdminService
    {
        private UnitOfWork _unitOfwork;
        private ILogger<AdminService> _logger;
        private IMapper _mapper;

        public AdminService(UnitOfWork unitOfWork, ILogger<AdminService> logger, IMapper mapper)
        {
            _unitOfwork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public Result<GroupServiceModel> MakeGroupAdmin(int userId, int groupId,  int currentUserId)
        {
            var result = new Result<GroupServiceModel>();
            try
            {
                var groupUser = _unitOfwork.GroupUserRepository.GetSingle(x =>x.IsActive && x.GroupId == groupId && x.UserId == userId);
                if (groupUser != null)
                {
                    groupUser.IsAdmin = true;
                    groupUser.UpdatedBy = currentUserId;
                    groupUser.UpdatedOn = Application.CurrentDate;
                    _unitOfwork.Save();

                    var group = _unitOfwork.GroupRepository.GetById(groupId);
                    var groupSM = _mapper.Map<GroupServiceModel>(group);
                    result.IsSuccess = true;
                    result.Data = groupSM;
                    result.StatusCodes = StatusCodes.Status200OK;

                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = ErrorMessage.UpdateUnAuth;
                    result.StatusCodes = StatusCodes.Status401Unauthorized;
                    return result;
                }
            }
            catch(Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Update;
                result.StatusCodes = StatusCodes.Status500InternalServerError;
                return result;
            }
            
        }

        public Result<GroupServiceModel> RemoveFromGroupAdmin(int userId, int groupId, int currentUserId)
        {
            var result = new Result<GroupServiceModel>();
            try
            {
                var groupUser = _unitOfwork.GroupUserRepository.GetSingle(x => x.IsActive && x.GroupId == groupId && x.UserId == userId);
                if (groupUser != null)
                {
                    groupUser.IsAdmin = false;
                    groupUser.UpdatedBy = currentUserId;
                    groupUser.UpdatedOn = Application.CurrentDate;
                    _unitOfwork.Save();

                    var group = _unitOfwork.GroupRepository.GetById(groupId);
                    var groupSM = _mapper.Map<GroupServiceModel>(group);
                    result.IsSuccess = true;
                    result.Data = groupSM;
                    result.StatusCodes = StatusCodes.Status200OK;

                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = ErrorMessage.UpdateUnAuth;
                    result.StatusCodes = StatusCodes.Status401Unauthorized;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Update;
                result.StatusCodes = StatusCodes.Status500InternalServerError;
                return result;
            }

        }

        public Result<UserServiceModel> AddUserToGroup(int userId, int groupId, int currentUserId)
        {
            var result = new Result<UserServiceModel>();
            try
            {
                var groupUser = new Group_User()
                {
                    UserId = userId,
                    GroupId = groupId,
                    IsAdmin = false,
                    IsActive = true,
                    CreatedBy = currentUserId,
                    CreatedOn = Application.CurrentDate
                };
                _unitOfwork.GroupUserRepository.Add(groupUser);
                _unitOfwork.Save();

                var user = _unitOfwork.UserRepository.GetById(userId);
                var userSM = _mapper.Map<UserServiceModel>(user);

                result.IsSuccess = true;
                result.Data = userSM;
                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Update;
                result.StatusCodes = StatusCodes.Status500InternalServerError;
                return result;
            }
        }

        public Result<UserServiceModel> DeleteUserFromGroup(int userId, int groupId, int currentUserId)
        {
            throw new NotImplementedException();
        }
    }
}
