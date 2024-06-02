using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Entities;
using AutoMapper;

namespace AcademicFlow.Managers.Managers
{
    public class EducationBaseManager(IMapper mapper) : BaseManager(mapper)
    {
        public static string? UserRoleValidation(IQueryable<User> userQuery, HashSet<int>? userIds, RolesEnum role)
        {
            var notValidUsers = userQuery.Where(x => userIds.Contains(x.Id) && !x.UserRoles.Any(x => x.Role == role)).Select(x => x.Id).AsEnumerable();
            if (notValidUsers != null && notValidUsers.Any())
            {
                return $"Users with ids [{string.Join(", ", notValidUsers)}] have no {role} role. No one user is assigned";
            }
            return null;
        }
    }
}
