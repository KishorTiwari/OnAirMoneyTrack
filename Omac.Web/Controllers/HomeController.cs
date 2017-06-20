using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Omack.Services.ServiceImplementations;
using Omack.Data.Models;
using Omack.Data.DAL;
using Omack.Data.Infrastructure;
using Omack.Services.Services;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using Omack.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Omack.Web.Controllers;
using Microsoft.AspNetCore.Http;

namespace Omac.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IItemService _itemService;
        private UserManager<User> _userManager;

        public HomeController(IItemService itemService, UserManager<User> userManager) : base(userManager)
        {
            _userManager = userManager;
            _itemService = itemService;
        }
        public ActionResult Demo(ItemServiceModel model)
        {
            var item = new ItemServiceModel
            {
                Name = "Milk",
                Price = 15,
                DateOfPurchase = DateTime.UtcNow,
                ItemType = 1,
                UserId = 2,
                GroupId = 10,
                MediaId = 1
            };
            _itemService.Add(item);
            return Ok(item);
        }

        [Authorize]
        public IActionResult Index(int? Id)
        {
            var cookies = Request.Cookies;

            var claims = HttpContext.User.Claims;

            return Ok(claims);
        }

        [Authorize(Roles = "administrator")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        //[Authorize(Roles = "administrator,root")]
        [Authorize(Policy = "ViewContact")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [Authorize(Policy = "IsAdmin")]
        public IActionResult AdminPage()
        {
            return Ok("Admin Page");
        }

        [Authorize(Policy = "IsAdmin")]
        public IActionResult BadPage()
        {
            return Ok("Some bad stuff. Only 18 plus are able to see this page");
        }

        public IActionResult WriteCookies(string name, string value, bool isPersistent)
        {
            if (isPersistent)
            {
                var opt = new CookieOptions();
                opt.Expires = DateTime.UtcNow.AddMinutes(60);
                Response.Cookies.Append(name, value, opt);
            }
            var cookieAddress = Request.Cookies["address"];
            return Ok($"Cookie stored. Value is: {cookieAddress}");
        }
    }
}
