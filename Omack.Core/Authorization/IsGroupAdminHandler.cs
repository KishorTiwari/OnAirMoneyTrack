using Microsoft.AspNetCore.Authorization;
using Omack.Data.DAL;
using Omack.Services.ServiceImplementations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Omack.Core.Authorization
{
    public class IsGroupAdminHandler: AuthorizationHandler<IsGroupAdmin>
    {
        private UserService _userService;

        public IsGroupAdminHandler(UserService userService)
        {
            _userService = userService;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsGroupAdmin requirement)
        {
            var isAdmin = _userService.IsAdmin();
            if (!isAdmin)
            {
                return Task.CompletedTask;
            }
            else
            {
                 context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
