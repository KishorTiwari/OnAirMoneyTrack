using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Omack.Data.Infrastructure;
using Omack.Data.Models;
using Microsoft.AspNetCore.Identity;
using Omack.Web.ViewModels;

namespace Omack.Web.Controllers
{
    public class AccountController : Controller
    {
        private UnitOfWork _unitOfWork;
        private UserManager<User> _userManager;

        public AccountController(UnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok("You're logged in");
            }
            else
            {
                return View();
            }
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel userViewModel,string returnUrl)
        {
            //do user registration
            if (await _userManager.FindByEmailAsync(userViewModel.Email) == null)
            {
                var user = new User()
                {
                    UserName = userViewModel.UserName,
                    Email = userViewModel.Email  ,
                    MediaId = 1
                };
                await _userManager.CreateAsync(user, userViewModel.Password);
                return Ok(user);
            }
            return View(); //return View();
        }
    }
}