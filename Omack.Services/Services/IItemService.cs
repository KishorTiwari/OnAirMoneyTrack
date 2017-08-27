using Microsoft.AspNetCore.JsonPatch;
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
        Result<IList<ItemServiceModel>> GetAll(int userId, int groupId);
        Result<ItemServiceModel> GetById(int id, int userId, int groupId);
        Result<ItemServiceModel> Add(ItemServiceModel item, int userId, int groupId);
        Result<ItemServiceModel> Update(ItemServiceModel item, int userId, int groupId);
        Result<ItemServiceModel> PatchUpdate(int groupId, int userId, int itemId, JsonPatchDocument<ItemServiceModel> itemPatch);
        Result<ItemServiceModel> Delete(int id, int userId, int groupId);
    }
}
