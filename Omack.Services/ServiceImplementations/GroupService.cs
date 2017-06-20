using Omack.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Omack.Data.Models;
using System.Linq.Expressions;
using Omack.Data.Infrastructure;
using Omack.Services.Models;
using System.Linq;

namespace Omack.Services.ServiceImplementations
{
    public class GroupService : IGroupService
    {
        private UnitOfWork _unitOfWork;

        public GroupService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(GroupServiceModel group)
        {

            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<GroupServiceModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<GroupServiceModel> GetAll(Expression<Func<GroupServiceModel, bool>> where)
        {
            // return _unitOfWork.GroupRepository.GetAll(where);
            throw new NotImplementedException();
        }

        public GroupServiceModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(GroupServiceModel group)
        {
            throw new NotImplementedException();
        }
    }
}
