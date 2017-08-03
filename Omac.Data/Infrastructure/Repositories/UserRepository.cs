using Omack.Data.DAL;
using Omack.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Data.Infrastructure.Repositories
{
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        private OmackContext _context;

        public UserRepository(OmackContext context): base(context)
        {
            _context = context;
        }
    }
    public interface IUserRepository: IGenericRepository<User>
    {

    }
}
