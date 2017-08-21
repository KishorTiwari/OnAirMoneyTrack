using Omack.Core.Models;
using Omack.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omack.Services.Services
{
    public interface ITransactionService
    {
        Result<IList<TransactionServiceModel>> GetAll(CurrentUser currentUser, CurrentGroup currentGroup);
        Result<TransactionServiceModel> GetById(int id, CurrentUser currrentUser, CurrentGroup currentGroup);
        Result<TransactionServiceModel> Add(TransactionServiceModel group, CurrentUser currentUser, CurrentGroup currentGroup);
        Result<TransactionServiceModel> Update(TransactionServiceModel group, CurrentUser currentUser, CurrentGroup currentGroup);
        Result<TransactionServiceModel> Delete(int Id, CurrentUser currentUser, CurrentGroup currentGroup);
    }
}
