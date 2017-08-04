using Omack.Core.Models;
using Omack.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Omack.Services.Services
{
    public interface IItemService
    {
        Result<IQueryable<ItemServiceModel>> GetAll(CurrentUser currentUser, CurrentGroup currentGroup);
        Result<ItemServiceModel> GetById(int id, CurrentUser currentUser, CurrentGroup currentGroup);
        Result<ItemServiceModel> Add(ItemServiceModel item, CurrentUser currentUser, CurrentGroup currentGroup);
        Result<ItemServiceModel> Update(ItemServiceModel item, CurrentUser currentUser, CurrentGroup currentGroup);
        Result<ItemServiceModel> Delete(int id, CurrentUser currentUser, CurrentGroup currentGroup);
    }
}
