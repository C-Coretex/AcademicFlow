using AcademicFlow.Models.Enums;

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
    }
}
