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
        Result<IEnumerable<GroupServiceModel>> GetAll();
        Result<IEnumerable<GroupServiceModel>> GetAll(Expression<Func<GroupServiceModel, bool>> where);
        Result<GroupServiceModel> GetById(int id);
        Result<GroupServiceModel> Add(GroupServiceModel group, CurrentUser currentUser);
        Result<GroupServiceModel> Update(GroupServiceModel group, CurrentUser currentUser);
        Result<GroupServiceModel> Delete(int Id, CurrentUser currentUser);
    }
}
