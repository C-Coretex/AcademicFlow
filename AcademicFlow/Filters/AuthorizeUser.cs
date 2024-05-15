using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Helpers;
using AcademicFlow.Managers.Contracts.IManagers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AcademicFlow.Filters
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthorizeUser : Attribute, IAuthorizationFilter
    {
        private readonly RolesEnum[] _roles;
        public static Func<IServiceScope> ServiceScopeFactory { get; set; }
        public AuthorizeUser(params RolesEnum[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userId = AuthorizationHelpers.GetUserIdIfAuthorized(context.HttpContext.Session);
            if (userId == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            using var scope = ServiceScopeFactory();
            var userManager = scope.ServiceProvider.GetService<IUserManager>()!;
            var user = userManager.GetUserById(userId.Value).Result;
        }
    }
}
