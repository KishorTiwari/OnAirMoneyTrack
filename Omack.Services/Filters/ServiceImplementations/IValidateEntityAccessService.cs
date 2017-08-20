using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Services.Filters.ServiceImplementations
{
    public interface IValidateEntityAccessService
    {
        bool Validate(int id, string entity);
    }
}
