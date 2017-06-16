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

namespace Omac.Web.Controllers
{
    public class HomeController : Controller
    {
        private IItemService _itemService;
        private UserManager<User> _userManager;

        public HomeController(IItemService itemService, UserManager<User> userManager)
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
                UserId = 1,
                GroupId = 10,
                MediaId = 1
            };
            _itemService.Add(item);
            return Ok(item);
        }

        [Authorize]
        public IActionResult Index(int? Id)
        {
            var currentUserName = _userManager.GetUserName(User);
            return Ok($"Hi {currentUserName}. You're authorized to access this page. ");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "GroupAdmin")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
