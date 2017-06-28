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
using Omack.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Omack.Services.ServiceImplementations;

namespace Omack.Web.Controllers
{
    public class AccountController : Controller
    {
        private UnitOfWork _unitOfWork;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private RoleManager<Role> _roleManager;
        private UserService _userService;

        public AccountController(
            UnitOfWork unitOfWork,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            UserService userService
            )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            //var currentUserr = _userService.CurrentUser();
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
                    var user = await _userManager.FindByEmailAsync(userLoginModel.Email);
                    if (user == null)
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Email.");
                    }
                    else
                    {                       
                        var signIn = await _signInManager.PasswordSignInAsync(user.UserName, userLoginModel.Password, userLoginModel.RememberMe, false);
                        if (!signIn.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, "Invalid Password");
                            return View();
                        }
                        else
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
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Something went wrong while signing in user. Please contact adminstrator. Thank You");
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
                };
                //user.Claims.Add(new IdentityUserClaim<int>
                //{
                //    ClaimType = "Admin",
                //    ClaimValue = "Admin"
                //});
                await _userManager.CreateAsync(user, userRegisterModel.Password); //register user
                return Ok($"Hi {user.UserName}. You've successfully created your account.");
            }
            return View(); //return View();
        }

        public IActionResult SelectGroup()
        {
           // var groups = 
            return View();
        }
        public async Task<IActionResult> CreateRole(string email, string role)
        {
            var newRole = new Role();
            await _roleManager.CreateAsync(newRole);  //create role
            var user = await _userManager.FindByEmailAsync(email);
            //await _userManager.AddToRoleAsync(user, role);   //add to role

            //List<Claim> claims = new List<Claim>();
            //claims.Add(new Claim(ClaimTypes.Name, "GrantFullAccess", ClaimValueTypes.String, null));
            //var userIdentity = new ClaimsIdentity("SuperUser");
            //userIdentity.AddClaims(claims);
            //var userPrincipal = new ClaimsPrincipal(userIdentity);
            //await HttpContext.Authentication.SignInAsync("Cookie", userPrincipal, new Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties
            //{
            //    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
            //    IsPersistent = true,
            //    AllowRefresh = false
            //});
            newRole.Name = role;
            //await _userManager.AddClaimsAsync(user, claims);

            //await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Root"));
            return Ok("Changed to Admin");
        }
        public async Task<IActionResult> AssignRole(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            await _userManager.AddToRoleAsync(user, role);   //add to role
            //await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Root"));
            return Ok($"Changed to {role}");
        }
        public async Task<IActionResult> Claim(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            user.Claims.Add(new IdentityUserClaim<int>
            {
                ClaimType = "ViewContact",
                ClaimValue = "View Contact"
            });
            return Ok(user.Claims);
        }
        public IActionResult AccessDenied()
        {
            return Ok("Sorry. Access denied");
        }
    }
}