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
        Result<IList<GroupServiceModel>> GetAll(int userId);
        Result<GroupServiceModel> GetById(int id, int userId);
        Result<GroupServiceModel> Add(GroupServiceModel group, int userId);
        Result<GroupServiceModel> Update(GroupServiceModel group, int userId);
        Result<GroupServiceModel> Delete(int Id, int userId);
    }
}
