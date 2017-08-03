using Microsoft.AspNetCore.Http;
using Omack.Core.Models;
using Omack.Data.Infrastructure;
using Omack.Data.Models;
using Omack.Services.ServiceImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Omack.Api
{
    public class SiteUtils
    {
        private IHttpContextAccessor _httpContext;
        private UserService _userService;

        public SiteUtils(IHttpContextAccessor httpContext, UserService userService)
        {
            _httpContext = httpContext;
            _userService = userService;
        }
        public CurrentUser CurrentUser
        {
            get
            {
                try
                {
                    var claims = (ClaimsIdentity)_httpContext.HttpContext.User.Identity;
                    var userEmail = claims.FindFirst(ClaimTypes.Email);
                    var result = _userService.GetUserByEmail(userEmail.Value);
                    var userDb = result.Data;
                    var currentUser = new CurrentUser()
                    {
                        Id = userDb.Id,
                        Email = userDb.Email,
                        Name = userDb.UserName
                    };
                    return currentUser;
                }
                catch (Exception ex)
                {
                    return new CurrentUser { Id = 1000 };
                }
            }

        }
    }
}
