using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
            await _userRepository.AddAsync(user);
        }

        public IQueryable<User> GetUsers()
        {
            return _userRepository.GetAll().AsNoTracking();
        }
    }
}
