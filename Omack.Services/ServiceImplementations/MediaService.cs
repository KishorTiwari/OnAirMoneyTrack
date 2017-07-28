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

namespace Omack.Services.ServiceImplementations
{
    public class MediaService : IMediaService
    {
        private UnitOfWork _unitOfWork;
        private ILogger<MediaService> _logger;
        private IMapper _mapper;

        public MediaService(UnitOfWork unitOfWork, ILogger<MediaService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public Result<MediaServiceModel> Add(CurrentUser currentUser, CurrentGroup currentGroup, MediaServiceModel mediaModel)
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
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = currentUser.Id
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
            catch(Exception ex)
            {
                _logger.LogError(ex.InnerException.Message);
                result.IsSuccess = false;
                result.ErrorMessage = "Sorry. Something went wrong when uploading media.";
                return result;
            }
        }

        public Result<MediaServiceModel> Delete(CurrentUser currentUser, CurrentGroup currentGroup, int Id)
        {
            // var result = new Result<MediaServiceModel>();
            return null;

        }

        public Result<IQueryable<MediaServiceModel>> GetAll(CurrentUser currentUser, CurrentGroup currentGroup)
        {
            throw new NotImplementedException();
        }

        public Result<MediaServiceModel> GetById(CurrentUser currentUser, CurrentGroup currentGroup, int id)
        {
            throw new NotImplementedException();
        }

        public Result<MediaServiceModel> Update(CurrentUser currentUser, CurrentGroup currentGroup, MediaServiceModel mediaModel)
        {
            throw new NotImplementedException();
        }
    }
}
