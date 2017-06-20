using Omack.Data.DAL;
using Omack.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Data.Infrastructure.Repositories
{
    public class TransactionRepository: GenericRepository<Transaction>, ITransactionInterface
    {
        public TransactionRepository(OmackContext context) : base(context)
        {

        }
    }
    public interface ITransactionInterface: IGenericRepository<Transaction>
    {

    }
}
