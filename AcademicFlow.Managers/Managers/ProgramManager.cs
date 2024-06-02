using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Contracts.Models.ProgramModels;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AcademicFlow.Managers.Managers
{
    public class ProgramManager(IMapper mapper, IProgramService programService, IUserService userService)
        : BaseManager(mapper), IProgramManager
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

        public async Task EditProgramUserRolesAsync(int programId, int[] usersIds)
        {
            var users = usersIds.ToHashSet();
            var courseUsers = _programService.GetAllUserRoles().Where(x => x.ProgramId == programId && x.UserRole.Role == RolesEnum.Student);
            var toDelete = courseUsers.Where(x => !users.Contains(x.UserRole.UserId)).ToList();
            await _programService.DeleteProgramUserRolesRangeAsync(toDelete!);

            var oldUsersIds = courseUsers
                .Select(x => x.UserRole.UserId)
                .ToHashSet();
            var toInsertUserIds = users.Where(x => !oldUsersIds.Contains(x)).ToHashSet();
            var toInsert = _userService.GetUsers()
                .Where(x => toInsertUserIds.Contains(x.Id))
                .SelectMany(x => x.UserRoles)
                .Where(x => x.Role == RolesEnum.Student)
                .Select(x => new ProgramUserRole()
                {
                    ProgramId = programId,
                    UserRoleId = x!.Id
                });
            await _programService.AddProgramUserRolesRangeAsync(toInsert);
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
