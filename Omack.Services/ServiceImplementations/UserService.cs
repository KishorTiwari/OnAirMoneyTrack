using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Omack.Core.Models;
using Omack.Data.DAL;
using Omack.Data.Infrastructure;
using Omack.Data.Models;
using Omack.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Omack.Services.ServiceImplementations
{
    public class UserService
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ILogger<UserService> _logger;

        public UserService(UnitOfWork unitOfwork, IMapper mapper, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfwork;            
            _mapper = mapper;
            _logger = logger;
        }
        public bool IsAdmin()
        {
            //var userId = Convert.ToInt32(_userManager.GetUserId(_context.HttpContext.User));
            //var usrGroup = _dbContext.Group_User.Where(x => x.UserId == _currentUser.Id && x.GroupId == 14 && x.IsAdmin && x.IsActive);
            //if (usrGroup.Any())
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return true;
        }
        public IQueryable<User> GetUsersByGroupId(int groupId)
        {
            var users = _unitOfWork.UserRepository.GetAll(x => x.Group_Users.All(y => y.GroupId == groupId));
            return users;
        }
        public Result<UserServiceModel> GetUserById(int id, CurrentUser currentUser)
        {
            var result = new Result<UserServiceModel>();
            try
            {
                var user = _unitOfWork.UserRepository.GetSingle(x => x.Id == id && x.Id == currentUser.Id);
                if (user != null)
                {
                    var userServiceModel = _mapper.Map<UserServiceModel>(user);
                    result.IsSuccess = true;
                    result.Data = userServiceModel;

                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Sorry. You don't have permission to access this user.";

                    return result;
                }
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = "Something went wrong while fetching data from server";

                return result;
            }
            
        }

        public Result<UserServiceModel> GetUserByEmail(string email)
        {
            var result = new Result<UserServiceModel>();
            try
            {
                var user = _unitOfWork.UserRepository.GetSingle(x => x.Email == email);
                if (user != null)
                {
                    var userServiceModel = _mapper.Map<UserServiceModel>(user);
                    result.IsSuccess = true;
                    result.Data = userServiceModel;

                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Sorry. You don't have permission to access this user.";

                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = "Something went wrong while fetching data from server";

                return result;
            }

        }
    }
}
