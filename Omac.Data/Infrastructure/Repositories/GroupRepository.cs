using Microsoft.EntityFrameworkCore;
using Omack.Data.DAL;
using Omack.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omack.Data.Infrastructure.Repositories
{
    public class GroupRepository : GenericRepository<Group>//, IGroupRepository
    {
        private OmackContext _context;

        public GroupRepository(OmackContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<User> GetAllUsers(int groupId)
        {
            //var users = _context.Group_User.Where(x => x.GroupId == groupId && x.IsActive);
            //var users = _context.Users.Include(x=>x.gro).Where(x => x.GroupId == groupId && x.IsActive);
            var users = _context.Group_User.Include(x => x.Group).Where(x => x.User.Id == 0);
            //r users = _context.Users.Include(x=>x.Group_Users).Where(x=>x.Group_Users.
            // userss = _context.Users.Include(x=>x.Group_Users).Where(x=>x.Group_Users.)
           // var userss = _context.Users.Include(x => x.Group_Users).Where(x => x.Group_Users);
            return null;
        }
    }

    public interface IGroupRepository : IGenericRepository<Group>
    {
        IQueryable<User> GetAllUsers();
    }
}
