using Omack.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Omack.Core.Models;
using Omack.Services.Models;
using System.Linq;
using AutoMapper;
using Omack.Data.Infrastructure;
using Microsoft.Extensions.Logging;
using Omack.Core.Constants;
using Omack.Data.Models;

namespace Omack.Services.ServiceImplementations
{
    public class NotificationService : INotificationService
    {
        private ILogger _logger;
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;

        public NotificationService(IMapper mapper, UnitOfWork unitOfWork, ILogger logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public Result<NotificationServiceModel> Add(NotificationServiceModel notificationModel, int userId, int groupId)
        {
            var result = new Result<NotificationServiceModel>();
            try
            {
                var newNotification = new NotificationServiceModel()
                {
                    Description = notificationModel.Description,
                    Type = notificationModel.Type,
                    UserId = userId,
                    GroupId = groupId,
                    CreatedOn = Application.CurrentDate,
                    CreatedBy = userId
                };

                var notificationEntity = _mapper.Map<Notification>(newNotification);
                _unitOfWork.NotificationRepository.Add(notificationEntity);

                var notificationServiceModel = _mapper.Map<NotificationServiceModel>(notificationEntity);
                result.IsSuccess = true;
                result.Data = notificationServiceModel;
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Add;
                return result;
            }
        }

        public Result<NotificationServiceModel> Delete(int id, int userId, int groupId)
        {
            var result = new Result<NotificationServiceModel>();
            try
            {
                var notificationEntity = _unitOfWork.NotificationRepository.GetSingle(x => x.Id == id && x.IsActive && x.UserId == userId && x.GroupId == groupId);
                if(notificationEntity != null)
                {
                    notificationEntity.IsActive = false;
                    notificationEntity.UpdatedBy = userId;
                    notificationEntity.UpdatedOn = Application.CurrentDate;
                    _unitOfWork.Save();

                    var notificationServiceModel = _mapper.Map<NotificationServiceModel>(notificationEntity);

                    result.IsSuccess = true;
                    result.Data = notificationServiceModel;
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = ErrorMessage.DeleteUnAuth;
                    return result;
                }
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Delete;
                return result;
            }
        }

        public Result<IQueryable<NotificationServiceModel>> GetAll(int userId, int groupId)
        {
            var result = new Result<IQueryable<NotificationServiceModel>>();
            try
            {
                var notificationEntities = _unitOfWork.NotificationRepository.GetAll(x => x.UserId == userId && x.IsActive && x.GroupId == groupId);
                if (notificationEntities.Any())
                {
                    var notificationServiceModels = _mapper.Map<IQueryable<NotificationServiceModel>>(notificationEntities);

                    result.IsSuccess = true;
                    result.Data = notificationServiceModels;
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = ErrorMessage.GetUnAuth;
                    return result;
                }
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.GetUnAuth;
                return result;
            }
        }

        public Result<NotificationServiceModel> GetById(int id, int userId, int groupId)
        {
            var result = new Result<NotificationServiceModel>();
            try
            {
                var notificationEntity = _unitOfWork.NotificationRepository.GetSingle(x => x.Id == id && x.IsActive && x.UserId == userId && x.GroupId == groupId);
                if(notificationEntity != null)
                {
                    var notificationServiceModel = _mapper.Map<NotificationServiceModel>(notificationEntity);

                    result.IsSuccess = true;
                    result.Data = notificationServiceModel;
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = ErrorMessage.GetUnAuth;
                    return result;
                }
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.GetUnAuth;
                return result;
            }
        }

        public Result<NotificationServiceModel> Update(NotificationServiceModel notificationModel, int userId, int groupId)
        {
            var result = new Result<NotificationServiceModel>();
            try
            {
                var notificationEntity = _unitOfWork.NotificationRepository.GetSingle(x => x.Id == notificationModel.Id && x.IsActive && x.UserId == userId && x.GroupId == groupId);
                if(notificationEntity != null)
                {
                    notificationEntity.Description = notificationModel.Description;
                    notificationEntity.Type = (int)notificationModel.Type;
                    notificationEntity.UpdatedOn = Application.CurrentDate;
                    notificationEntity.UpdatedBy = userId;
                    _unitOfWork.Save();

                    var notificationServiceModel = _mapper.Map<NotificationServiceModel>(notificationEntity);

                    result.IsSuccess = true;
                    result.Data = notificationServiceModel;
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = ErrorMessage.GetUnAuth;
                    return result;
                }
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Update;
                return result;
            }
        }
    }
}
