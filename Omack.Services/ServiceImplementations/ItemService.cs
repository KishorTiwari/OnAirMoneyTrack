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

namespace Omack.Services.ServiceImplementations
{
    public class ItemService : IItemService
    {
        private UnitOfWork _unitOfWork;

        public ItemService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(ItemServiceModel itemModel)
        {
            var item = new Item
            {
                Name = itemModel.Name,
                Price = itemModel.Price,
                DateOfPurchase = itemModel.DateOfPurchase,
                ItemType = itemModel.ItemType,
                UserId = itemModel.UserId,
                GroupId = itemModel.GroupId,
                MediaId = itemModel.MediaId,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow,
                IsActive = true              
            };

            _unitOfWork.itemRepository.Add(item);
            _unitOfWork.Save();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemServiceModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemServiceModel> GetAll(Expression<Func<ItemServiceModel, bool>> where)
        {
            throw new NotImplementedException();
        }

        public ItemServiceModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ItemServiceModel group)
        {
            throw new NotImplementedException();
        }
    }
}
