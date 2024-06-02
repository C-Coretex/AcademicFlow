using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Contracts.Models;
using AcademicFlow.Managers.Contracts.Models.ProgramModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Data;

namespace AcademicFlow.Managers.Managers
{
    public class ProgramManager(IMapper mapper, IProgramService programService, IUserService userService)
        : EducationBaseManager(mapper), IProgramManager
    {
        private readonly IProgramService _programService = programService;
        private readonly IUserService _userService = userService;

        public async Task<int?> AddProgramAsync(Program program)
        {
            return await _programService.AddProgramAsync(program);
        }

        public async Task DeleteProgramAsync(int id)
        {
            await _programService.DeleteProgramAsync(id);
        }

        public Program? GetProgramById(int id)
        {
            return _programService.GetAll().Where(x => x.Id == id).FirstOrDefault();
        }

        public async Task UpdateProgramAsync(Program program)
        {
            await _programService.UpdateProgramAsync(program);
        }

        public IEnumerable<ProgramTableItem> GetProgramTableItemList(int? userId)
        {
            var programs = _programService.GetAll();
            if (userId.HasValue)
            {
                programs = programs.Where(x => x.UserRoles != null && x.UserRoles.Any(x => x.UserRole.Id == userId && x.UserRole.Role == RolesEnum.Student));
            }
            return programs.ProjectTo<ProgramTableItem>(MapperConfig).AsEnumerable();
        }

        public async Task<ResponseModel> EditProgramUserRolesAsync(int programId, int[] usersIds)
        {
            var response = new ResponseModel();
            var userQuery = _userService.GetUsers();
            var users = usersIds.ToHashSet();
            var errors = UserRoleValidation(userQuery, users, RolesEnum.Student);
            if (errors != null)
            {
                response.Error = errors;
                return response;
            }

            var courseUsers = _programService.GetAllUserRoles().Where(x => x.ProgramId == programId && x.UserRole.Role == RolesEnum.Student);
            var toDelete = courseUsers.Where(x => !users.Contains(x.UserRole.UserId)).ToList();
            await _programService.DeleteProgramUserRolesRangeAsync(toDelete!);

            var oldUsersIds = courseUsers
                .Select(x => x.UserRole.UserId)
                .ToHashSet();
            var toInsertUserIds = users.Where(x => !oldUsersIds.Contains(x)).ToHashSet();
            var toInsert = userQuery
                .Where(x => toInsertUserIds.Contains(x.Id))
                .SelectMany(x => x.UserRoles)
                .Where(x => x.Role == RolesEnum.Student)
                .Select(x => new ProgramUserRole()
                {
                    ProgramId = programId,
                    UserRoleId = x!.Id
                });
            await _programService.AddProgramUserRolesRangeAsync(toInsert);
            response.IsSuccesful = true;
            return response;
        }

        public IEnumerable<User> GetProgramUsers(int programId)
        {
            return _programService
                .GetAllUserRoles()
                .Where(x => x.ProgramId == programId)
                .Select(x => x.UserRole.User);
        }
    }
}
