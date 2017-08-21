using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Omack.Data.DAL;
using Omack.Services.Filters.ServiceImplementations;
using System;
using System.Linq;

namespace Omack.Api.Filters
{
    public class ValidateEntityAccess: ActionFilterAttribute
    {
        private IValidateEntityAccessService _validateEntityAccessService;
        private SiteUtils _siteUtils;

        public ValidateEntityAccess(IValidateEntityAccessService validateEntityAccessService, SiteUtils siteUtils)
        {
            _validateEntityAccessService = validateEntityAccessService;
            _siteUtils = siteUtils;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var currentUserId = _siteUtils.CurrentUser.Id;
            var entityId = context.ActionArguments.Values.OfType<Int32>().FirstOrDefault();
            var parameterName = context.ActionDescriptor.Parameters.Where(p => p.ParameterType == typeof(Int32)).FirstOrDefault().Name;
            var parameter = context.ActionDescriptor.Parameters; //ActionArguments.Values;
            if (entityId > 0)
            {
                var validate = _validateEntityAccessService.Validate(currentUserId, entityId, parameterName);
                if (!validate)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            else
            {
                context.Result = new BadRequestResult();
            }           
            base.OnActionExecuting(context);          
        }
    }
}
