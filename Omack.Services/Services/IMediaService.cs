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
        Result<IList<MediaServiceModel>> GetAll(int userId, int groupId);
        Result<MediaServiceModel> GetById(int id, int userId, int groupId);
        Result<MediaServiceModel> Add(MediaServiceModel item, int userId, int groupId);
        Result<MediaServiceModel> Update(MediaServiceModel item, int userId, int groupId);
        Result<MediaServiceModel> Delete(int Id, int userId, int groupId);
    }
}
