using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Services.Filters.ServiceImplementations
{
    public interface IValidateEntityAccessService
    {
        bool Validate(int currentUserId, int entityId, string parameterName);
    }
}
