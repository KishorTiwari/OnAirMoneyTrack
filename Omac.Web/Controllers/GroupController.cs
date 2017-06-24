using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Omack.Data.Models;

namespace Omack.Web.Controllers
{
    public class GroupController : BaseController
    {
        public GroupController(UserManager<User> userManager) : base(userManager)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Select()
        { 
            return Ok("this is select group page");
        }
    }
}