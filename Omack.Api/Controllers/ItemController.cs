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
    [Route("api/group/{groupId:int:min(1)}/item")]
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

        [HttpGet(Name = "GetItemsByGroupId")]
        [ValidateModel]
        public ActionResult GetItemsByGroupId(int groupId)
        {
            var result = _itemService.GetAll(_currentUserId, groupId);
            if (result.IsSuccess)
            {
                var itemViewModels = _mapper.Map<IList<ItemViewModel>>(result.Data);
                return Ok(itemViewModels);
            }
            return NotFound(result.ErrorMessage);
        }

        [HttpGet("{itemId:int:min(1)}", Name = "GetItemById")]
        public ActionResult GetItemById(int groupId, int itemId)
        {
            var result = _itemService.GetById(itemId, _currentUserId, groupId);
            if (result.IsSuccess)
            {
                var itemViewModel = _mapper.Map<ItemViewModel>(result.Data);
                return Ok(itemViewModel);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpPost(Name = "CreateItemByGroupId")]
        [ValidateModel]
        [ServiceFilter(typeof(ValidateEntityAccess))] //put groupId as first parameter, as filter will take it as entity name to be validated. 
        public IActionResult CreateItem(int groupId, [FromBody]ItemViewModel itemModel)
        {
            var itemServiceModel = _mapper.Map<ItemServiceModel>(itemModel);
            var result = _itemService.Add(itemServiceModel, _currentUserId, groupId);
            if (result.IsSuccess)
            {
                var itemViewModel = _mapper.Map<ItemViewModel>(result.Data);
                return CreatedAtAction("GetItemById", new { itemId = itemViewModel.Id }, itemViewModel); //return 201 Created with http location header
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpDelete("{itemId:int:min(1)}",Name = "DeleteItemById")]
        [ValidateModel]
        public IActionResult DeleteItemById(int groupId, int itemId)
        {
            var result = _itemService.Delete(itemId, _currentUserId, groupId);
            if (result.IsSuccess)
            {
                var itemModel = _mapper.Map<ItemViewModel>(result.Data);
                return Ok(itemModel);
            }
            return BadRequest(result.ErrorMessage);
        }
        
    }
}