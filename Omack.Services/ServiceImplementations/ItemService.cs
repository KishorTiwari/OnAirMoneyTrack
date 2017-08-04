using Omack.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Omack.Services.Models;
using System.Linq.Expressions;
using Omack.Data.Models;
using Omack.Data.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Omack.Data;
using Omack.Core.Models;
using System.Linq;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Omack.Core.Constants;

namespace Omack.Services.ServiceImplementations
{
    public class ItemService : IItemService
    {
        //private fields
        private UnitOfWork _unitOfWork;
        private ILogger<ItemService> _logger;
        private IMapper _mapper;

        //Constructor
        public ItemService(UnitOfWork unitOfWork, ILogger<ItemService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        //functions
        public Result<IQueryable<ItemServiceModel>> GetAll(CurrentUser currentUser, CurrentGroup currentGroup)
        {
            var result = new Result<IQueryable<ItemServiceModel>>();

            try
            {
                var items = _unitOfWork.ItemRepository.GetAll(x => x.IsActive && x.GroupId == currentGroup.Id);
                if (items.Any())
                {
                    var itemModels = _mapper.Map<IQueryable<ItemServiceModel>>(items);
                    result.IsSuccess = true;
                    result.Data = itemModels;
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = ErrorMessage.GetUnAuth;
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Get;
                return result;
            }
        }

        public Result<ItemServiceModel> GetById(int id, CurrentUser currentUser, CurrentGroup currentGroup)
        {
            var result = new Result<ItemServiceModel>();
            try
            {
                var item = _unitOfWork.ItemRepository.GetSingle(x => x.Id == id && x.IsActive && x.UserId == currentUser.Id && x.GroupId == currentGroup.Id);

                if (item != null)
                {
                    var itemModel = _mapper.Map<ItemServiceModel>(item);

                    result.IsSuccess = true;
                    result.Data = itemModel;
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = ErrorMessage.GetUnAuth;
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Get;
                return result;
            }
        }

        public Result<ItemServiceModel> Add(ItemServiceModel item, CurrentUser currentUser, CurrentGroup currentGroup)
        {
            var result = new Result<ItemServiceModel>();
            try
            {
                var newItem = new Item()
                {
                    Name = item.Name,
                    Price = item.Price,
                    DateOfPurchase = item.DateOfPurchase,
                    ItemType = (int)item.ItemType,
                    IsActive = true,
                    UserId = currentUser.Id,
                    GroupId = currentGroup.Id,
                    MediaId = item.MediaId,
                    CreatedOn = Application.CurrentDate,
                    CreatedBy = currentUser.Id
                };
                _unitOfWork.ItemRepository.Add(newItem);
                _unitOfWork.Save();

                var mappedItem = _mapper.Map<ItemServiceModel>(newItem);

                result.IsSuccess = true;
                result.Data = mappedItem;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Add;
                return result;
            }
        }

        public Result<ItemServiceModel> Update(ItemServiceModel itemModel, CurrentUser currentUser, CurrentGroup currentGroup)
        {
            var result = new Result<ItemServiceModel>();

            try
            {
                var itemEntity = _unitOfWork.ItemRepository.GetSingle(x => x.Id == itemModel.Id && x.IsActive && x.UserId == currentUser.Id && x.GroupId == currentGroup.Id);

                if (itemEntity != null)
                {
                    itemEntity.Name = itemModel.Name;
                    itemEntity.Price = itemModel.Price;
                    itemEntity.DateOfPurchase = itemModel.DateOfPurchase;
                    itemEntity.ItemType = (int)itemModel.ItemType;
                    itemEntity.GroupId = itemModel.GroupId;
                    itemEntity.MediaId = itemModel.MediaId;
                    itemEntity.UpdatedOn = Application.CurrentDate;
                    itemEntity.UpdatedBy = currentUser.Id;
                    _unitOfWork.Save();

                    //map to service model after update
                    var updatedItemModel = _mapper.Map<ItemServiceModel>(itemEntity);

                    //return result
                    result.IsSuccess = true;
                    result.Data = updatedItemModel;
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = ErrorMessage.UpdateUnAuth;
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Update;
                return result;
            }
        }

        public Result<ItemServiceModel> Delete(int id, CurrentUser currentUser, CurrentGroup currentGroup)
        {
            var result = new Result<ItemServiceModel>();
            try
            {
                var itemEntity = _unitOfWork.ItemRepository.GetSingle(x => x.Id == id && x.IsActive && x.GroupId == currentGroup.Id);
                if(itemEntity != null)
                {
                    itemEntity.IsActive = false;
                    itemEntity.UpdatedBy = currentUser.Id;
                    itemEntity.UpdatedOn = Application.CurrentDate;
                    _unitOfWork.Save();

                    var itemServiceModel = _mapper.Map<ItemServiceModel>(itemEntity);

                    result.IsSuccess = true;
                    result.Data = itemServiceModel;
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = ErrorMessage.DeleteUnAuth;
                    return result;
                }
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Delete;
                return result;
            }
        }
    }
}
