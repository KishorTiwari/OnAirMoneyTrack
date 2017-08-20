using Microsoft.EntityFrameworkCore;
using Omack.Data.DAL;
using Omack.Services.Filters.ServiceImplementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Services.Filters.Services
{
    public class ValidateEntityAccessService : IValidateEntityAccessService
    {
        private OmackContext _dbContext;

        public ValidateEntityAccessService(OmackContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool Validate(int id, string entity)
        {
            switch (entity)
            {
                case "Group": return true;
                default: return false;
            }
        }
    }
}
