using Omack.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Omack.Core.Models;
using Omack.Services.Models;
using System.Linq;
using Omack.Data.Models;
using Microsoft.Extensions.Logging;
using Omack.Data.Infrastructure;
using AutoMapper;
using Omack.Core.Constants;

namespace Omack.Services.ServiceImplementations
{
    public class MediaService : IMediaService
    {
        //Private fields
        private UnitOfWork _unitOfWork;
        private ILogger<MediaService> _logger;
        private IMapper _mapper;

        //Constructor
        public MediaService(UnitOfWork unitOfWork, ILogger<MediaService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        //Functions
        public Result<IQueryable<MediaServiceModel>> GetAll(int userId, int groupId)
        {
            var result = new Result<IQueryable<MediaServiceModel>>();
            try
            {
                var mediaEntities = _unitOfWork.MediaRepository.GetAll(x => x.User.Id == userId);
                if (mediaEntities.Any())
                {
                    var mediaServiceModels = _mapper.Map<IQueryable<MediaServiceModel>>(mediaEntities);

                    result.IsSuccess = true;
                    result.Data = mediaServiceModels;
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
                result.ErrorMessage = ErrorMessage.Get;
                return result;
            }
        }

        public Result<MediaServiceModel> GetById(int id, int userId, int groupId)
        {
            var result = new Result<MediaServiceModel>();
            try
            {
                var mediaEntity = _unitOfWork.MediaRepository.GetSingle(x => x.Id == id && x.IsActive && x.User.Id == userId);
                if(mediaEntity != null)
                {
                    var mediaServiceModel = _mapper.Map<MediaServiceModel>(mediaEntity);

                    result.IsSuccess = true;
                    result.Data = mediaServiceModel;
                    return result;
                }
                else
                {
                    result.IsSuccess = true;
                    result.ErrorMessage = ErrorMessage.GetUnAuth;
                    return result;
                }
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.InnerException.Message);
                result.IsSuccess = true;
                result.ErrorMessage = ErrorMessage.Get;
                return result;
            }
        }

        public Result<MediaServiceModel> Add(MediaServiceModel mediaModel, int userId, int groupId)
        {
            var result = new Result<MediaServiceModel>();

            try
            {
                //map to media entity
                var mediaEntity = new Media
                {
                    Guid = mediaModel.Guid,
                    Url = mediaModel.Url,
                    TypeId = (int)mediaModel.TypeId,
                    CreatedOn = Application.CurrentDate,
                    CreatedBy = userId
                };

                //save to database
                _unitOfWork.MediaRepository.Add(mediaEntity);
                _unitOfWork.Save();

                //map to service model
                var mappedMediaModel = _mapper.Map<MediaServiceModel>(mediaEntity);

                //return result
                result.IsSuccess = true;
                result.Data = mappedMediaModel;
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ErrorMessage.Add;
                return result;
            }
        }

        public Result<MediaServiceModel> Update(MediaServiceModel mediaModel, int userId, int groupId)
        {
            var result = new Result<MediaServiceModel>();
            try
            {
                var mediaEntity = _unitOfWork.MediaRepository.GetSingle(x => x.Id == mediaModel.Id && x.IsActive && x.User.Id == userId);
                if(mediaModel != null)
                {
                    mediaEntity.UpdatedBy = userId;
                    mediaEntity.UpdatedOn = Application.CurrentDate;
                    _unitOfWork.MediaRepository.Update(mediaEntity);
                    _unitOfWork.Save();

                    var mediaServiceModel = _mapper.Map<MediaServiceModel>(mediaEntity);
                    result.IsSuccess = true;
                    result.Data = mediaServiceModel;
                    return result;
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = ErrorMessage.UpdateUnAuth;
                    return result;
                }
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex.InnerException.Message);
                result.IsSuccess = true;
                result.ErrorMessage = ErrorMessage.Update; ;
                return result;
            }
        }

        public Result<MediaServiceModel> Delete(int id, int userId, int groupId)
        {
            var result = new Result<MediaServiceModel>();
            try
            {
                var mediaEntity = _unitOfWork.MediaRepository.GetSingle(x => x.Id == id && x.IsActive && x.User.Id == userId && x.Group.Id == groupId);
                if(mediaEntity != null)
                {
                    mediaEntity.IsActive = false;
                    mediaEntity.UpdatedBy = userId;
                    mediaEntity.UpdatedOn = Application.CurrentDate;
                    _unitOfWork.Save();

                    var mediaServiceModel = _mapper.Map<MediaServiceModel>(mediaEntity);
                    result.IsSuccess = true;
                    result.Data = mediaServiceModel;
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
    }
}
