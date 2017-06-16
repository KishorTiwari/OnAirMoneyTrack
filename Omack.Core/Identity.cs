using Microsoft.AspNetCore.Identity;
using Omack.Core.Models;
using Omack.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Core
{
    public class Identity
    {
        private UserManager<User> _userManager;

        public Identity(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        //public CurrentUser GetCurrentUser()
        //{
        //    var currentUser = new CurrentUser
        //    {
        //        Name = _userManager.GetUserName(User)
        //    }
        //}
    }
}
