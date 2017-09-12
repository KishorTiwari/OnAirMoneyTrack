using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Omack.Api.ViewModels;
using Omack.Api.ViewModels.Group;
using Omack.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Omack.Api.Controllers
{
    [Route("api/admin/group/{groupId:int:min(1)}")]
    public class AdminController: Controller
    {
        private IAdminService _adminService;
        private int _currentUserId;
        private IMapper _mapper;

        public AdminController(IAdminService adminService, SiteUtils siteUtils, IMapper mapper)
        {
            _adminService = adminService;
            _currentUserId = siteUtils.CurrentUser.Id;
            _mapper = mapper;
        }

        [Route("user/{userId:int:min(1)}")]
        [HttpPost(Name = "MakeGroupAdmin")]
        public IActionResult MakeGroupAdmin(int groupId, int userId)
        {
            var result = _adminService.MakeGroupAdmin(userId, groupId, _currentUserId);
            if (result.IsSuccess)
            {
                var groupVM = _mapper.Map<GroupViewGM>(result.Data);
                return Ok(groupVM);
            }
            else
            {
                return new StatusCodeResult(result.StatusCodes);
            }
        }
    }
}
