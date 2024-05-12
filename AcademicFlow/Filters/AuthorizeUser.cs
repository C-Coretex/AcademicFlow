using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AcademicFlow.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthorizeUser : Attribute, IAuthorizationFilter
    {
        public AuthorizeUser(params RolesEnum[] roles)
        {
            
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuthorized = AuthorizationHelpers.IsAuthorized(context.HttpContext.Session);

            if (!isAuthorized)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
