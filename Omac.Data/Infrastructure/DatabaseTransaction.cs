using Microsoft.EntityFrameworkCore.Storage;
using Omack.Data.DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Data.Infrastructure
{
    public class DatabaseTransaction : IDatabaseTransaction
    {
        private IDbContextTransaction _transaction;
        private OmackContext _context;

        public DatabaseTransaction(OmackContext context)
        {
            _transaction = context.Database.BeginTransaction();
            _context = context;
        }
        public void commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
        public void Dispose()
        {
            _transaction.Dispose();                     
        }
    }
}
