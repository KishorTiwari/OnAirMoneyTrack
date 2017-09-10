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

namespace Omack.Api.Controllers
{
    [Route("api/group")]
    [Authorize]
    public class GroupController : Controller
    {
        private IGroupService _groupService;
        private int _currentUserId;
        private IHttpContextAccessor _context;
        private IMapper _mapper;

        public GroupController(IGroupService groupService, SiteUtils siteUtils, IHttpContextAccessor context, IMapper mapper)
        {
            _groupService = groupService;
            _currentUserId = siteUtils.CurrentUser.Id;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{id:int:Min(1)}", Name = "GetGroupById")]
        public IActionResult GetGroupById(int id)
        {
            var result = _groupService.GetById(id, _currentUserId);
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
            var result = _groupService.GetAll(_currentUserId);
            if (result.IsSuccess)
            {
                var groupsVM = _mapper.Map<IList<GroupVM>>(result.Data);
                return Ok(groupsVM);
            }
            return new StatusCodeResult(result.StatusCodes);
        }

        [HttpPost(Name = "CreateGroup")]
        [ValidateModel]
        public IActionResult CreateGroup([FromBody]GroupVM groupVM)
        {
                var groupServiceModel = _mapper.Map<GroupServiceModel>(groupVM);
                var result = _groupService.Add(groupServiceModel, _currentUserId);
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