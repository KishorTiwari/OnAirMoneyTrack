using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omack.Core.Authorization
{
    public class IsGroupAdmin: IAuthorizationRequirement
    {
        public IsGroupAdmin()
        {

        }
    }
}
