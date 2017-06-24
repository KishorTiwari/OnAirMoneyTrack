using Omack.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Omack.Data.Models;
using System.Linq.Expressions;
using Omack.Data.Infrastructure;
using Omack.Services.Models;
using System.Linq;
using Omack.Core.Models;

namespace Omack.Services.ServiceImplementations
{
    public class GroupService : IGroupService
    {
        private UnitOfWork _unitOfWork;

        public GroupService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Result<GroupServiceModel> Add(GroupServiceModel group, CurrentUser currentUser)
        {
            var result = new Result<GroupServiceModel>();
            try
            {
                var newGroup = new Group()
                {
                    Name = group.Name,
                    IsActive = true,
                    MediaId = group.MediaId,
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = currentUser.Id,
                    UpdatedOn = DateTime.UtcNow,
                    UpdatedBy = currentUser.Id
                };              
                _unitOfWork.GroupRepository.Add(newGroup);
                _unitOfWork.Save();

                var newGroupUser = new Group_User()
                {
                    GroupId = newGroup.Id,
                    UserId = currentUser.Id,
                    IsActive = true
                };
                _unitOfWork.GroupUserRepository.Add(newGroupUser);
                _unitOfWork.Save();
                group.Id = newGroup.Id;
                result.IsSuccess = true;
                result.Data = group;

                return result;
            }
            catch(Exception ex)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Sorry. Something went wrong when adding a group.";
                return result;
            }          
        }

        public Result<GroupServiceModel> Delete(int Id, CurrentUser currentUser)
        {
            throw new NotImplementedException();
        }

        public Result<IQueryable<GroupServiceModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Result<IQueryable<GroupServiceModel>> GetAll(Expression<Func<GroupServiceModel, bool>> where)
        {
            throw new NotImplementedException();
        }

        public Result<GroupServiceModel> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Result<GroupServiceModel> Update(GroupServiceModel group, CurrentUser currentUser)
        {
            throw new NotImplementedException();
        }
    }
}
