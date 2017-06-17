using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Omack.Web.ViewModels;
using Omack.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Omack.Web.Controllers
{
    public class BaseController : Controller
    {
        private UserManager<User> _userManager;

        public BaseController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}