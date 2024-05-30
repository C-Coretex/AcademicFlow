using AcademicFlow.Domain.Contracts.Enums;
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

            if (_roles != null && _roles.Length > 0)
            {
                using var scope = ServiceScopeFactory();
                var userManager = scope.ServiceProvider.GetService<IUserManager>()!;

                var user = userManager.GetUserById(userId.Value).Result;
                var isAuthorized = AuthorizationHelpers.HasRole(_roles, user);
                if (!isAuthorized)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
        }
    }
}
