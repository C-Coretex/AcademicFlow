using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Contracts.Models.ProgramModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace AcademicFlow.Managers.Managers
{
    public class ProgramManager(IMapper mapper, ICourseProgramService courseProgramService, IProgramService programService, IUserService userService)
        : BaseManager(mapper), IProgramManager
    {
        private readonly ICourseProgramService _courseProgramService = courseProgramService;
        private readonly IProgramService _programService = programService;
        private readonly IUserService _userService = userService;

        public int? AddProgram(Program program)
        {
            return _programService.AddProgram(program);
        }

        public void DeleteProgram(int id)
        {
            _programService.DeleteProgram(id);
        }

        public Program? GetProgramById(int id)
        {
            return _programService.GetProgramById(id);
        }

        public void UpdateProgram(Program program)
        {
            _programService.UpdateProgram(program);
        }

        public IEnumerable<ProgramTableItem> GetProgramTableItemList(int? userId)
        {
            var programs = _programService.GetAll();
            if (userId.HasValue)
            {
                programs = programs.Where(x => x.Users != null && x.Users.Any(x => x.User.User.Id == userId));
            }
            return programs.ProjectTo<ProgramTableItem>(MapperConfig).AsEnumerable();
        }

        public void EditProgramUserRoles(int programId, int[] usersIds)
        {
            var users = _userService.GetUsers();
            var oldUsers = users
                .Where(x => x.UserRoles.Any(y => y.Courses != null && y.Courses.Any(z => z.Course.Id == programId)))
                .ToList();

            var toDeleteCourseUsers = oldUsers
                .Where(x => !usersIds.Contains(x.Id))
                .Select(x => x.UserRoles.Where(x => x.Role == RolesEnum.Student).FirstOrDefault())
                .Where(x => x != null)
                .Select(x => new ProgramUserRole()
                {
                    ProgramId = programId,
                    UserRoleId = x.Id
                });
            _programService.DeleteProgramUserRolesRange(toDeleteCourseUsers);

            var toInsertCourseUsers = users
                .Where(x => usersIds.Contains(x.Id))
                .Select(x => x.UserRoles.Where(x => x.Role == RolesEnum.Student).FirstOrDefault())
                .Where(x => x != null) 
                .Select(x => new ProgramUserRole()
                {
                    ProgramId = programId,
                    UserRoleId = x.Id
                });
            _programService.AddProgramUserRolesRange(toInsertCourseUsers);
        }
    }
}
