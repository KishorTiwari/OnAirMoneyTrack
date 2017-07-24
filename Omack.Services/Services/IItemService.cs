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
        Result<ItemServiceModel> GetById(CurrentUser currentUser, CurrentGroup currentGroup, int id);
        Result<ItemServiceModel> Add(CurrentUser currentUser, CurrentGroup currentGroup, ItemServiceModel item);
        Result<ItemServiceModel> Update(CurrentUser currentUser, CurrentGroup currentGroup, ItemServiceModel item);
        Result<ItemServiceModel> Delete(CurrentUser currentUser, CurrentGroup currentGroup, int Id);
    }
}
