using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AcademicFlow.Domain.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task AddUser(User user)
        {
            var existingUser = _userRepository.GetAll().FirstOrDefault(u => u.PersonalCode == user.PersonalCode);
            if (existingUser != null)
                throw new ValidationException($"User with personal code {user.PersonalCode} already exists. His id is {existingUser.Id}");

            await _userRepository.AddAsync(user);
        }

        public IQueryable<User> GetUsers()
        {
            return _userRepository.GetAll().AsNoTracking();
        }
    }
}
