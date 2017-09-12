using Microsoft.EntityFrameworkCore;
using Omack.Data.DAL;
using Omack.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omack.Data.Infrastructure.Repositories
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        private OmackContext _context;

        public GroupRepository(OmackContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<Group> GetAllGroupsByUserId(int userId)
        {
            var groups = _context.Group.Where(x => x.Group_Users.All(y => y.UserId == userId && y.IsActive == true));
            return groups;
        }

        public IQueryable<User> GetAllUsersByGroupId(int groupId)
        {
            var users = _context.Users.Where(x => x.Group_Users.All(y=>y.GroupId == groupId));
            return users;
        }
    }

    public interface IGroupRepository : IGenericRepository<Group>
    {
        IQueryable<User> GetAllUsersByGroupId(int groupId);
        IQueryable<Group> GetAllGroupsByUserId(int userId);
    }
}
