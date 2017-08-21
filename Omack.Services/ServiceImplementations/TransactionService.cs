using Omack.Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Omack.Core.Models;
using Omack.Services.Models;
using System.Linq;
using Omack.Data.Infrastructure;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Omack.Core.Constants;
using Omack.Data.Models;

namespace Omack.Services.ServiceImplementations
{
    public class TransactionService : ITransactionService
    {
        private UnitOfWork _unitOfWork;
        private ILogger<TransactionService> _logger;
        private IMapper _mapper;

        public TransactionService(UnitOfWork unitOfWork, ILogger<TransactionService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public Result<TransactionServiceModel> Add(TransactionServiceModel transaction, CurrentUser currentUser, CurrentGroup currentGroup)
        {
            var result = new Result<TransactionServiceModel>();
            try
            {
                var transactionEntity = new Transaction()
                {
                    Amount = transaction.Amount,
                    ReceiverId = transaction.ReceiverId,
                    SenderId = transaction.SenderId,
                    IsComplete = false,
                    IsActive = true,
                    UserId = transaction.UserId,
                    GroupId = transaction.GroupId,
                    CreatedOn = Application.CurrentDate,
                    CreatedBy = currentUser.Id
                };
                _unitOfWork.TransactionRepository.Add(transactionEntity);
                _unitOfWork.Save();

                var transactionServiceModel = _mapper.Map<TransactionServiceModel>(transactionEntity);
                result.IsSuccess = true;
                result.Data = transactionServiceModel;

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

        public Result<TransactionServiceModel> Delete(int id, CurrentUser currentUser, CurrentGroup currentGroup)
        {
            var result = new Result<TransactionServiceModel>();
            try
            {
                var transactionEntity = _unitOfWork.TransactionRepository.GetSingle(x => x.IsActive && x.Id == id && x.UserId == currentUser.Id && x.GroupId == currentGroup.Id);
                if(transactionEntity != null)
                {
                    transactionEntity.IsActive = false;
                    transactionEntity.UpdatedBy = currentUser.Id;
                    transactionEntity.UpdatedOn = Application.CurrentDate;
                    _unitOfWork.Save();

                    var transactionServiceModel = _mapper.Map<TransactionServiceModel>(transactionEntity); 
                    result.IsSuccess = true;
                    result.Data = transactionServiceModel;
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

        public Result<IList<TransactionServiceModel>> GetAll(CurrentUser currentUser, CurrentGroup currentGroup)
        {
            var result = new Result<IList<TransactionServiceModel>>();
            try
            {
                var transactionEntities = _unitOfWork.TransactionRepository.GetAll(x => x.IsActive && x.UserId == currentUser.Id && x.GroupId == currentGroup.Id);
                if (transactionEntities.Any())
                {
                    var transactionServiceModels = _mapper.Map<IList<TransactionServiceModel>>(transactionEntities);

                    result.IsSuccess = true;
                    result.Data = transactionServiceModels;
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

        public Result<TransactionServiceModel> GetById(int id, CurrentUser currrentUser, CurrentGroup currentGroup)
        {
            var result = new Result<TransactionServiceModel>();
            try
            {
                var transactionEntity = _unitOfWork.TransactionRepository.GetSingle(x => x.IsActive && x.Id == id && x.UserId == currrentUser.Id && x.GroupId == currentGroup.Id);
                if(transactionEntity != null)
                {
                    var transactionServiceModel = _mapper.Map<TransactionServiceModel>(transactionEntity);

                    result.IsSuccess = true;
                    result.Data = transactionServiceModel;
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

        public Result<TransactionServiceModel> Update(TransactionServiceModel group, CurrentUser currentUser, CurrentGroup currentGroup)
        {
            var result = new Result<TransactionServiceModel>();
            try
            {
                var transactionEntity = _unitOfWork.TransactionRepository.GetSingle(x => x.IsActive && x.Id == group.Id && x.UserId == currentUser.Id && x.GroupId == currentGroup.Id);
                if(transactionEntity != null)
                {
                    transactionEntity.Amount = group.Amount;
                    transactionEntity.ReceiverId = group.ReceiverId;
                    transactionEntity.SenderId = group.SenderId;
                    transactionEntity.IsComplete = group.IsComplete;
                    transactionEntity.UpdatedOn = Application.CurrentDate;
                    transactionEntity.UpdatedBy = currentUser.Id;
                    _unitOfWork.Save();

                    var transactionEntityService = _mapper.Map<TransactionServiceModel>(transactionEntity);
                    result.IsSuccess = false;
                    result.Data = transactionEntityService;
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
                result.ErrorMessage = ErrorMessage.Update;
                return result;
            }
        }
    }
}
