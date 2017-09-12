using Omack.Core.Models;
using Omack.Data.Models;
using Omack.Services.Models;
using Omack.Services.Models.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Omack.Services.Services
{
    public interface IGroupService
    {
        Result<IList<GroupServiceGM>> GetAll(CurrentUser currentUser);
        Result<GroupServiceGM> GetById(int id, CurrentUser currentUser);
        Result<GroupServiceGM> Add(GroupServicePM group, CurrentUser currentUser);
        Result<GroupServiceGM> Update(GroupServicePM group, CurrentUser currentUser);
        Result<GroupServiceGM> Delete(int Id, CurrentUser currentUser);
    }
}
