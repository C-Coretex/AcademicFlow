using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.Models.CourseModels;

namespace AcademicFlow.Managers.Contracts.IManagers
{
    public interface ICourseManager
    {
        Task<int?> AddCourseAsync(Course course); 
        Task DeleteCourseAsync(int id);

        Course? GetCourseByIdAsync(int id);

        Task UpdateCourseAsync(Course course);

        /// <summary>
        /// Gets Course Table;
        /// Without params - all courses;
        /// With userId and role - courses for individual the user (including by program);
        /// With program Id - courses for the program;
        /// </summary>
        /// <param name="userId"> add role to get courses for individual the user (including by program)</param>
        /// <param name="assingedProgramId">get for the program</param>
        /// <param name="role">add user id to get courses for individual the user (including by program)</param>
        /// <returns></returns>
        IEnumerable<CourseTableItem> GetCourseTableItemList(int? userId, RolesEnum? role, int? assingedProgramId);

        Task EditCourseProgramsAsync(int courseId, int[] programIds);
        Task EditCourseUserRoles(int courseId, int[] usersIds, RolesEnum role);

        IEnumerable<User> GetCourseUsers(int courseId, RolesEnum role);

        IEnumerable<Program> GetCoursePrograms(int courseId);
    }
}
