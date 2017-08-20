using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Omack.Data.DAL;
using Omack.Services.Filters.ServiceImplementations;

namespace Omack.Api.Filters
{
    public class ValidateEntityAccess: ActionFilterAttribute
    {
        private IValidateEntityAccessService _validateEntityAccessService;

        private SiteUtils _siteUtils { get; set; }

        private string _entity { get; set; }

        public ValidateEntityAccess(string entity)
        {
            _entity = entity;
        }
        public ValidateEntityAccess(IValidateEntityAccessService validateEntityAccessService)
        {
            _validateEntityAccessService = validateEntityAccessService;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var validate = _validateEntityAccessService.Validate(41, _entity);
            if (!validate)
            {
                context.Result = new UnauthorizedResult();
            }
            // actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            base.OnActionExecuting(context);          
        }
    }
}
