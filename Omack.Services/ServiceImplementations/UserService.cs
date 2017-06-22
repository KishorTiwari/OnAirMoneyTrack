using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Omack.Data.DAL;
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

        public UserService(IHttpContextAccessor context, UserManager<User> userManager, OmackContext dbContext)
        {
            _context = context;
            _userManager = userManager;
            _dbContext = dbContext;
            _currentUser = _userManager.GetUserAsync(_context.HttpContext.User).Result;
        }
        public User CurrentUser()
        {
            return _currentUser;
        } 
        public bool IsAdmin()
        {
            var userId = Convert.ToInt32(_userManager.GetUserId(_context.HttpContext.User));
            var isAdmin = _dbContext.Group_User.Where(x => x.UserId == _currentUser.Id && x.GroupId == 2 && x.IsAdmin && x.IsActive).Any();
            if (isAdmin)
            {
                return true;
            }
            return false;
        }
    }
}
