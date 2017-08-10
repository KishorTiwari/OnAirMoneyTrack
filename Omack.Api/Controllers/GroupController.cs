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

namespace Omack.Api.Controllers
{
    [Route("api/group")]
    [Authorize]
    public class GroupController : Controller
    {
        private IGroupService _groupService;
        private SiteUtils _siteUtils;
        private IHttpContextAccessor _context;

        public GroupController(IGroupService groupService, SiteUtils siteUtils, IHttpContextAccessor context)
        {
            _groupService = groupService;
            _siteUtils = siteUtils;
            _context = context;
        }
        [Authorize]
        [HttpGet("{id}", Name = "GetGroupById")]
        public IActionResult GetGroupById(int id)
        {
            var result = _groupService.GetById(id, _siteUtils.CurrentUser.Id);
            if (result.IsSuccess)
            {
                var group = result.Data;
                return Ok(group);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpGet()]
        public IActionResult GetGroups()
        {
            var result = _groupService.GetAll(_siteUtils.CurrentUser.Id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return Ok(result.ErrorMessage);
            }
        }
    }
}