using Omack.Core.Models;
using Omack.Data.Models;
using Omack.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Omack.Services.Services
{
    public interface IGroupService
    {
        Result<IQueryable<GroupServiceModel>> GetAll(CurrentUser currentUser);
        Result<GroupServiceModel> GetById(int id, CurrentUser currrentUser);
        Result<GroupServiceModel> Add(GroupServiceModel group, CurrentUser currentUser);
        Result<GroupServiceModel> Update(GroupServiceModel group, CurrentUser currentUser);
        Result<GroupServiceModel> Delete(int Id, CurrentUser currentUser);
    }
}
