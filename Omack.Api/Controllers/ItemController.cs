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
    [Route("api/group/{groupId:int}/item")]
    [Authorize]
    public class ItemController : Controller
    {
        private IMapper _mapper;
        private ILogger<ItemController> _logger;
        private IItemService _itemService;
        private int _currentUserId;
        private IValidateEntityAccessService _validateEntityAccessService;

        public ItemController(IItemService itemService, SiteUtils siteUtils, ILogger<ItemController> logger, IMapper mapper, IValidateEntityAccessService validateEntityAccessService)
        {
            _itemService = itemService;
            _currentUserId = siteUtils.CurrentUser.Id;
            _validateEntityAccessService = validateEntityAccessService;
            _logger = logger;
            _mapper = mapper;
        }


        /*
        ROUTING - same for other enitities
        api/group/23/item/23 --get item of id 23 from group id 23
        api/group/24/items --get all items from group id 24
        */

        [HttpGet()]
        [ValidateModel]
        public ActionResult GetItemsByGroupId(int groupId)
        {
            var result = _itemService.GetAll(_currentUserId, groupId);
            if (result.IsSuccess)
            {
                var itemModels = _mapper.Map<IQueryable<ItemViewModel>>(result.Data);
                return Ok(itemModels);
            }
            return NotFound(result.ErrorMessage);
        }

        //Get item by Group Id and Item Id
        [HttpGet("{itemId:int}", Name = "GetItemById")]
        public ActionResult GetItemByIdItemId(int groupId, int itemId)
        {
            var result = _itemService.GetById(itemId, _currentUserId, groupId);
            if (result.IsSuccess)
            {
                var items = _mapper.Map<ItemViewModel>(result.Data);
                return Ok(items);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost(Name = "PostItemByGroupId")]
        [ValidateModel]
        [ServiceFilter(typeof(ValidateEntityAccess))]
        public IActionResult PostItem([FromBody]ItemViewModel itemModel, int groupId)
        {
            var itemServiceModel = _mapper.Map<ItemServiceModel>(itemModel);
            var result = _itemService.Add(itemServiceModel, _currentUserId, groupId);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
    }
}