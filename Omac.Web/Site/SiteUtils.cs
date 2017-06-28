using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Omack.Core.Models;
using Omack.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omack.Web.Site
{
    public class SiteUtils
    {
        private IHttpContextAccessor _context;
        private UserManager<User> _userManager;
        private User _currentUser;
        public SiteUtils(IHttpContextAccessor context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            _currentUser = _userManager.GetUserAsync(_context.HttpContext.User).Result;
        }
        public CurrentUser CurrentUser()
        {
            var user = new CurrentUser()
            {
                Id = _currentUser.Id,
                Name = _currentUser.UserName,
                Email = _currentUser.Email
            };
            return user;
        }
    }
}
