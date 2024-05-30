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

        public async Task<Program?> GetProgramByIdAsync(int id)
        {
            return await _programService.GetProgramByIdAsync(id);
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
            var users = _userService.GetUsers().Include(x => x.UserRoles).ThenInclude(x => x.Programs);
            var oldUsers = users
                .Where(x => x.UserRoles.Any(y => y.Courses != null && y.Courses.Any(z => z.Course.Id == programId)))
                .ToList();

            var toDeleteProgramUsers = oldUsers
                           .Where(x => !usersIds.Contains(x.Id))
                           .Select(x => x.UserRoles.Where(x => x.Role == RolesEnum.Student).FirstOrDefault())
                           .Select(x => x?.Programs?.Where(x => x.ProgramId == programId).FirstOrDefault())
                           .Where(x => x != null);
            await _programService.DeleteProgramUserRolesRangeAsync(toDeleteProgramUsers!);

            var toInsertProgramUsers = users
                .Where(x => usersIds.Contains(x.Id))
                .Select(x => x.UserRoles.Where(x => x.Role == RolesEnum.Student).FirstOrDefault())
                .Where(x => x != null)
                .Select(x => new ProgramUserRole()
                {
                    ProgramId = programId,
                    UserRoleId = x!.Id
                });
            await _programService.AddProgramUserRolesRangeAsync(toInsertProgramUsers);
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
