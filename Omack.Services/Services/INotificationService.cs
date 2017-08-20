using Omack.Core.Models;
using Omack.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omack.Services.Services
{
    public interface INotificationService
    {
        Result<IQueryable<NotificationServiceModel>> GetAll(int userId, int groupId);
        Result<NotificationServiceModel> GetById(int id, int userId, int groupId);
        Result<NotificationServiceModel> Add(NotificationServiceModel notificationModel, int userId, int groupId);
        Result<NotificationServiceModel> Update(NotificationServiceModel notificationModel, int userId, int groupId);
        Result<NotificationServiceModel> Delete(int Id, int userId, int groupId);
    }
}
