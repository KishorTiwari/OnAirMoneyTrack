﻿using System;
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
using Omack.Web.Site;

namespace Omac.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IItemService _itemService;
        private UserManager<User> _userManager;
        private GroupService _groupService;
        private SiteUtils _siteUtils;

        public HomeController(
            IItemService itemService,
            GroupService groupService,
            SiteUtils siteUtils,
            UserManager<User> userManager) : base(userManager)
        {
            _userManager = userManager;
            _siteUtils = siteUtils;
            _itemService = itemService;
            _groupService = groupService;
        }
        public ActionResult Demo(ItemServiceModel model)
        {
            var item = new ItemServiceModel
            {
                Name = "Milk",
                Price = 15,
                DateOfPurchase = DateTime.UtcNow,
                ItemType = (Omack.Core.Enums.ItemType)1,
                UserId = 2,
                GroupId = 10,
                MediaId = 1
            };
            _itemService.Add(item,_siteUtils.CurrentUser.Id, _siteUtils.CurrentGroup.Id);
            return Ok(item);
        }

        public IActionResult Index(int? Id)
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize(Policy = "Admin")]
        public IActionResult AdminPage()
        {
            return Ok("Only Admin can access this Page");
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

        public IActionResult GroupTestDelete(int Id)
        {
            var result = _groupService.Delete(Id, _siteUtils.CurrentUser.Id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return Ok(result.ErrorMessage);
        }
    }
}
