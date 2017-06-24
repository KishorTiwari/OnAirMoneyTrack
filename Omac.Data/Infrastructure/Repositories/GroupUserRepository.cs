using Omack.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Omack.Data.DAL;

namespace Omack.Data.Infrastructure.Repositories
{
    public class GroupUserRepository : GenericRepository<Group_User>, IGroupUserRepository
    {
        public GroupUserRepository(OmackContext context) : base(context)
        {
        }
    }
    public interface IGroupUserRepository: IGenericRepository<Group_User>
    {

    }
}
