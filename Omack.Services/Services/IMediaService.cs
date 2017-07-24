using Omack.Core.Models;
using Omack.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omack.Services.Services
{
    public interface IMediaService
    {
        Result<IQueryable<MediaServiceModel>> GetAll(CurrentUser currentUser, CurrentGroup currentGroup);
        Result<MediaServiceModel> GetById(CurrentUser currentUser, CurrentGroup currentGroup, int id);
        Result<MediaServiceModel> Add(CurrentUser currentUser, CurrentGroup currentGroup, MediaServiceModel item);
        Result<MediaServiceModel> Update(CurrentUser currentUser, CurrentGroup currentGroup, MediaServiceModel item);
        Result<MediaServiceModel> Delete(CurrentUser currentUser, CurrentGroup currentGroup, int Id);
    }
}
