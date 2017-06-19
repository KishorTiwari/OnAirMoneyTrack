using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Omack.Web.ViewModels;
using Omack.Data.Models;
using Microsoft.AspNetCore.Identity;
using Omack.Services.ServiceImplementations;

namespace Omack.Web.Controllers
{
    public class BaseController : Controller
    {
        private UserManager<User> _userManager;
        private IdentityService _itemService;

        public BaseController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public UserIdentityModel GetCurrentUser()
        {
            var currentUser = GetCurrentUserAsync();
            return currentUser.Result;
        }
        public async Task<UserIdentityModel> GetCurrentUserAsync()
        {
            var identityUser = await _userManager.GetUserAsync(User);
            var currentUser = new UserIdentityModel
            {
                Id = identityUser.Id,
                Name = identityUser.UserName,
                Email = identityUser.Email
            };
            return currentUser;
        }
    }
}