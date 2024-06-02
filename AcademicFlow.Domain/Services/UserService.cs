using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AcademicFlow.Domain.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<User> AddUser(User user)
        {
            var existingUser = _userRepository.GetAll().Include(u => u.UserCredentials).FirstOrDefault(u => u.PersonalCode == user.PersonalCode);
            if (existingUser != null)
                throw new ValidationException($"User with personal code {user.PersonalCode} already exists. His id is {existingUser.Id}");

            return await _userRepository.AddAsync(user);
        }

        public async Task<User?> GetById(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public IQueryable<User> GetUsers()
        {
            return _userRepository.GetAll().Include(x => x.UserCredentials).Where(x => !x.IsDeleted).AsNoTracking();
        }
        public IQueryable<User> GetUsersWithRoles()
        {
            return _userRepository.GetAll().Include(x => x.UserRoles).Where(x => !x.IsDeleted).AsNoTracking();
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task<User> GetUserById(int userId)
        {
            var user = await _userRepository.GetAll().Include(u => u.UserRoles).Include(x => x.UserCredentials).FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<User?> GetUserByIdWithAssignments(int userId)
        {
            var user = await _userRepository.GetAll().Include(x => x.AssignmentTasks)
                                                     .ThenInclude(x => x.AssignmentEntries)
                                                     .ThenInclude(x => x.AssignmentGrade)
                                                     .FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(u => u.Id == userId);
            
            if (user == null)
                throw new ValidationException($"User with personal code {userId} does not exist.");
            user.IsDeleted = true;
            await _userRepository.UpdateAsync(user);
        }

        public async Task UpdateRoles(int userId, IEnumerable<RolesEnum> roles)
        {
            var user = _userRepository.GetAll(false).Include(u => u.UserRoles).FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new ValidationException($"User with id {userId} does not exist.");

            var rolesToDelete = user.UserRoles.ExceptBy(roles,x => x.Role).ToHashSet();
            var rolesToAdd = roles.Except(user.UserRoles.Select(x => x.Role)).ToHashSet();
            //Add roles

            user.UserRoles.AddRange(rolesToAdd.Select(x => new UserRole(user.Id,x)));

            //Delete roles
            foreach (var roleDel in rolesToDelete)
            {   
                user.UserRoles.Remove(roleDel);
            }
            await _userRepository.UpdateAsync(user);
        }
    }
}
