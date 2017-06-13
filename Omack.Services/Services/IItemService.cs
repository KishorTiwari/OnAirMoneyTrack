using Omack.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Omack.Services.Services
{
    public interface IItemService
    {
        IEnumerable<ItemServiceModel> GetAll();
        IEnumerable<ItemServiceModel> GetAll(Expression<Func<ItemServiceModel, bool>> where);
        ItemServiceModel GetById(int id);
        void Add(ItemServiceModel group);
        void Update(ItemServiceModel group);
        void Delete(int Id);
    }
}
