using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Omack.Data.Models;
using Omack.Data.Infrastructure;
using Omack.Web.Site;
using Omack.Web.ViewModels;
using Omack.Services.ServiceImplementations;
using AutoMapper;
using Omack.Services.Models;
using Microsoft.Extensions.Logging;

namespace Omack.Web.Controllers
{
    public class GroupController : BaseController
    {
        private SiteUtils _siteUtils;
        private GroupService _groupService;
        private IMapper _mapper;
        public GroupController(
            UserManager<User> userManager, 
            SiteUtils siteUtils,
            GroupService groupService,
            IMapper mapper) : base(userManager)
        {
            _siteUtils = siteUtils;
            _groupService = groupService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var groups = _groupService.GetAllByUserId(_siteUtils.CurrentUser());
            
            return Ok(groups);
        }

        public IActionResult Select()
        { 
            return Ok("this is select group page");
        }
        public IActionResult Create()
        {
            var group = new GroupViewModel
            {
                Name = "Bhuwan Group"              
            };

            var groupServiceModel = _mapper.Map<GroupServiceModel>(group);
            var result = _groupService.Add(groupServiceModel, _siteUtils.CurrentUser());
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return Ok($"Error: {result.ErrorMessage}");
            }
            
        }
    }
}