using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Omack.Data.DAL;
using Omack.Data.Infrastructure;
using Omack.Services.Filters.ServiceImplementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Services.Filters.Services
{
    public class ValidateEntityAccessService : IValidateEntityAccessService
    {
        private OmackContext _dbContext;
        private UnitOfWork _unitOfWork;

        public ValidateEntityAccessService(OmackContext dbContext, UnitOfWork unitOfWork)
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }
        public bool Validate(int currentUserId, int entityId, string parameterName)
        {
            switch (parameterName)
            {
                case "groupId":

                    var groupUser = _unitOfWork.GroupUserRepository.GetSingle(x => x.IsActive && x.UserId == currentUserId && x.GroupId == entityId);
                    if(groupUser != null)
                    {
                        return true;
                    }
                    return false;

                default: return false;
            }
        }
    }
}
