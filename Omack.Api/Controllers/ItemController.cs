using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Omack.Data.Infrastructure;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Omack.Services.ServiceImplementations;
using Omack.Api.ViewModels;
using Omack.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Omack.Services.Models;
using Omack.Api.Filters;
using Omack.Data.Models;
using Omack.Services.Filters.ServiceImplementations;

namespace Omack.Api.Controllers
{
    [Route("api/item")]
    [Authorize]
    public class ItemController : Controller
    {
        private IMapper _mapper;
        private ILogger<ItemController> _logger;
        private IItemService _itemService;
        private SiteUtils _siteUtils;
        private IValidateEntityAccessService _validateEntityAccessService;

        public ItemController(IItemService itemService, SiteUtils siteUtils, ILogger<ItemController> logger, IMapper mapper, IValidateEntityAccessService validateEntityAccessService)
        {
            _itemService = itemService;
            _siteUtils = siteUtils;
            _validateEntityAccessService = validateEntityAccessService;
            _logger = logger;
            _mapper = mapper;
        }

        //Get item by Group Id and Item Id
        [HttpGet("{groupId:int}/{itemId:int}", Name = "GetItemById")]
        public ActionResult GetItemById(int groupId, int itemId)
        {
            var result = _itemService.GetById(itemId, _siteUtils.CurrentUser.Id, groupId);
            if (result.IsSuccess)
            {
                var items = Mapper.Map<ItemViewModel>(result.Data);
                return Ok(items);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
       
        [HttpPost("{groupId:int}", Name = "PostItemByGroupId")]
        [ValidateEntityAccess("Group")]
        [ValidateModel]
        public IActionResult PostItem([FromBody]ItemViewModel itemModel, int groupId)
        {
            if (ModelState.IsValid)
            {
                var resultt = _validateEntityAccessService.Validate(45, "");
                var itemServiceModel = _mapper.Map<ItemServiceModel>(itemModel);
                var result = _itemService.Add(itemServiceModel, _siteUtils.CurrentUser.Id, groupId);
                if (result.IsSuccess)
                {
                    return Ok(result.Data);
                }
                else
                {
                    return BadRequest(result.ErrorMessage);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        } 
        [HttpPost("test/{groupId:int}")]
        public IActionResult PostTest(int groupId)
        {
            return Ok("post success");
        }
    }
}