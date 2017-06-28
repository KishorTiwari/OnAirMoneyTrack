using Omack.Data.DAL;
using Omack.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omack.Services.ServiceImplementations
{
    public class IdentityService
    {
        private UnitOfWork _unitOfWork;
        private OmackContext _context;

        public IdentityService(UnitOfWork unitOfWork, OmackContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public bool IsGroupAdmin(int userId, int groupId)
        {
            if(userId != 0 || groupId != 0)
            {
                return false;
            }
            var isAdmin = _context.Group_User.Where(x => x.UserId == userId && x.GroupId == groupId && x.IsAdmin).Any();
            if (isAdmin)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }

}
