using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Models.Enums;
using AcademicFlow.Domain.Contracts.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace AcademicFlow.Helpers
{
    public static class AuthorizationHelpers
    {
        public static void LoginUser(ISession session, int userId)
        {
            session.SetInt32(SessionParametersEnum.IsLoggedIn.ToString(), 1);
            session.SetInt32(SessionParametersEnum.UserId.ToString(), userId);
        }

        public static void LogoutUser(ISession session)
        {
            session.Remove(SessionParametersEnum.IsLoggedIn.ToString());
            session.Remove(SessionParametersEnum.UserId.ToString());
        }

        public static bool IsAuthorized(ISession session)
        {
            return session.GetInt32(SessionParametersEnum.IsLoggedIn.ToString()) == 1;
        }

        public static bool HasRole(IEnumerable<RolesEnum> roles, User user)
        {
            if (roles == null || !roles.Any())
                return true;
            return user.UserRoles.Any(ur => roles.Contains(ur.Role));
        }

        public static int? GetUserIdIfAuthorized(ISession session)
        {
            if (!IsAuthorized(session)) 
                return null;

            return session.GetInt32(SessionParametersEnum.UserId.ToString());
        }
    }
}
