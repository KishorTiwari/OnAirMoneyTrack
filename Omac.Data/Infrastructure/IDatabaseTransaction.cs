using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Data.Infrastructure
{
    public interface IDatabaseTransaction: IDisposable
    {
        void commit();
        void Rollback();
    }
}
