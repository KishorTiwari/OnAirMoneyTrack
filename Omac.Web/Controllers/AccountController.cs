using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Omack.Data.Infrastructure;
using Omack.Data.Models;
using Microsoft.AspNetCore.Identity;
using Omack.Web.ViewModels;
using System.Security.Claims;

namespace Omack.Web.Controllers
{
    public class AccountController : Controller
    {
        private UnitOfWork _unitOfWork;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private RoleManager<Role> _roleManager;

        public AccountController(
            UnitOfWork unitOfWork,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager
            )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var currentUser = _userManager.GetUserName(User);
                return Ok($"Hi {currentUser}. You are already logged in !");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(userLoginModel.UserName, userLoginModel.Password, true, false); //sign in new user
                    if (signInResult.Succeeded)
                    {
                        if (string.IsNullOrWhiteSpace(returnUrl))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return Redirect(returnUrl);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Email or Password.");
                    }
                }
                catch (Exception e)
                {
                    var error = e;
                }

            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel userRegisterModel, string returnUrl)
        {
            //do user registration
            if (await _userManager.FindByEmailAsync(userRegisterModel.Email) == null)
            {
                var user = new User()
                {
                    UserName = userRegisterModel.UserName,
                    Email = userRegisterModel.Email,
                    MediaId = 1
                };
                await _userManager.CreateAsync(user, userRegisterModel.Password); //register user
                return Ok(user);
            }
            return View(); //return View();
        }

        public async Task<IActionResult> MakeAdmin(string email)
        {
            var role = new Role();
            role.Name = "GroupAdmin";
            await _roleManager.CreateAsync(role);  //create role
            //var user = await _userManager.FindByEmailAsync(email);
            //await _userManager.AddToRoleAsync(user, "Root");   //add to role
            //await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Root"));
            return Ok("Changed to Admin");
        }
    }
}