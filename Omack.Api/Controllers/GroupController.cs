using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Omack.Data.Models;
using Omack.Services.ServiceImplementations;
using Omack.Services.Services;
using Omack.Core;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using Omack.Api.ViewModels;
using Omack.Services.Models;
using Omack.Api.Filters;
using Omack.Api.ViewModels.Group;
using Omack.Services.Models.Group;
using Omack.Core.Models;

namespace Omack.Api.Controllers
{
    [Route("api/group")]
    [Authorize]
    public class GroupController : Controller
    {
        private IGroupService _groupService;
        private CurrentUser _currentUser;
        private IHttpContextAccessor _context;
        private IMapper _mapper;

        public GroupController(IGroupService groupService, SiteUtils siteUtils, IHttpContextAccessor context, IMapper mapper)
        {
            _groupService = groupService;
            _currentUser = siteUtils.CurrentUser;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id:int:Min(1)}", Name = "GetGroupById")]
        public IActionResult GetGroupById(int id)
        {
            var result = _groupService.GetById(id, _currentUser);
            if (result.IsSuccess)
            {
                var group = result.Data;
                return Ok(group);
            }
            return new StatusCodeResult(result.StatusCodes);
        }

        [HttpGet()]
        public IActionResult GetGroupsByUserID()
        {
            var result = _groupService.GetAll(_currentUser);
            if (result.IsSuccess)
            {
                var groupsVM = _mapper.Map<IList<GroupViewGM>>(result.Data);
                return Ok(groupsVM);
            }
            return new StatusCodeResult(result.StatusCodes);
        }

        [HttpPost(Name = "CreateGroup")]
        [ValidateModel]
        public IActionResult CreateGroup([FromBody]GroupViewPM groupVM)
        {
            var groupServiceModel = _mapper.Map<GroupServicePM>(groupVM);
            var result = _groupService.Add(groupServiceModel, _currentUser);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return new StatusCodeResult(result.StatusCodes);
            }
        }
    }
}