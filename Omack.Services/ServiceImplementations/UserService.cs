using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Omack.Core.Models;
using Omack.Data.DAL;
using Omack.Data.Infrastructure;
using Omack.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Omack.Services.ServiceImplementations
{
    public class UserService
    {
        private IHttpContextAccessor _context;
        private UserManager<User> _userManager;
        private OmackContext _dbContext;
        private User _currentUser;
        private UnitOfWork _unitOfWork;

        public UserService(IHttpContextAccessor context, UserManager<User> userManager, OmackContext dbContext, UnitOfWork unitOfwork)
        {
            _context = context;
            _userManager = userManager;
            _dbContext = dbContext;
            _unitOfWork = unitOfwork;
            _currentUser = _userManager.GetUserAsync(_context.HttpContext.User).Result;
        }
        //public CurrentUser CurrentUser()
        //{
        //    var user = new CurrentUser()
        //    {
        //        Id = _currentUser.Id,
        //        Name = _currentUser.UserName,
        //        Email = _currentUser.Email
        //    };
        //    return user;
        //} 
        public bool IsAdmin()
        {
            var userId = Convert.ToInt32(_userManager.GetUserId(_context.HttpContext.User));
            var usrGroup = _dbContext.Group_User.Where(x => x.UserId == _currentUser.Id && x.GroupId == 1 && x.IsAdmin && x.IsActive);
            if(usrGroup == null)
            {
                return false;
            }
            if (usrGroup.Any())
            {
                return true;
            }
            return false;
        }
        public IQueryable<User> GetAllUsersByGroupId(int groupId)
        {
            var users = _dbContext.Users.Where(x => x.Group_Users.All(y => y.GroupId == groupId));
            return users;
        }
    }
}
