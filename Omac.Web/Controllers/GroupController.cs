using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Omack.Data.Models;
using Omack.Data.Infrastructure;
using Omack.Web.Site;

namespace Omack.Web.Controllers
{
    public class GroupController : BaseController
    {
        private UnitOfWork _unitOfWork;
        private SiteUtils _siteUtils;

        public GroupController(UserManager<User> userManager, SiteUtils siteUtils,UnitOfWork unitOfWork) : base(userManager)
        {
            _unitOfWork = unitOfWork;
            _siteUtils = siteUtils;
        }

        public IActionResult Index()
        {
            var groups = _unitOfWork.GroupRepository.GetAllGroupsByUserId(_siteUtils.CurrentUser().Id);
            return Ok(groups);
        }

        public IActionResult Select()
        { 
            return Ok("this is select group page");
        }
    }
}