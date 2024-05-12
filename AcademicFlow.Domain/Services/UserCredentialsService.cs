using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Helpers.Helpers;
using Microsoft.EntityFrameworkCore;

namespace AcademicFlow.Domain.Services
{
    public class UserCredentialsService: IUserCredentialsService
    {
        private readonly IUserCredentialsRepository _userCredentialsRepository;
        public UserCredentialsService(IUserCredentialsRepository userCredentialsRepository)
        {
            _userCredentialsRepository = userCredentialsRepository;
        }

        public async Task AddUserCredentials(int userId, string username, string password)
        {
            var existingCredentials = await _userCredentialsRepository.GetByIdAsync(userId);
            if (existingCredentials == null)
                throw new Exception("UserId does not exist");

            var (hash, salt) = CryptographyHelper.GetHash(password);

            existingCredentials.Username = username;
            existingCredentials.PasswordHash = hash;
            existingCredentials.Salt = salt;
            existingCredentials.SecurityKey = null;

            await _userCredentialsRepository.UpdateAsync(existingCredentials);
        }

        public async Task<bool> IsUserCredentialsValid(string username, string password)
        {
            var userCredentials = await _userCredentialsRepository.GetAll().FirstOrDefaultAsync(x => x.Username == username);
            return IsUserCredentialsCorrect(userCredentials, password);
        }

        public async Task<User?> GetUserByCredentials(string username, string password)
        {
            var userCredentials = await _userCredentialsRepository.GetAll()
                                                       .Include(x => x.User)
                                                       .FirstOrDefaultAsync(x => x.Username == username);

            var isUserCredentialsCorrect = IsUserCredentialsCorrect(userCredentials, password);
            if (!isUserCredentialsCorrect)
                return null;

            return userCredentials!.User;
        }

        public async Task<bool> IsUsernameTaken(string username)
        {
            return await _userCredentialsRepository.GetAll().AnyAsync(x => x.Username == username);
        }

        private static bool IsUserCredentialsCorrect(UserCredentials? userCredentials, string password)
        {
            if (userCredentials == null)
                return false;

            return CryptographyHelper.IsHashSame(userCredentials.PasswordHash, password, userCredentials.Salt);
        }

        public async Task<User?> GetUserBySecretKey(string secretKey)
        {
            var userCredentials = await _userCredentialsRepository.GetAll()
                                                       .Include(x => x.User)
                                                       .FirstOrDefaultAsync(x => x.SecurityKey == secretKey);

            return userCredentials?.User;
        }

        public async Task<UserCredentials?> GetBySecretKey(string secretKey)
        {
            var userCredentials = await _userCredentialsRepository.GetAll()
                                                       .FirstOrDefaultAsync(x => x.SecurityKey == secretKey);

            return userCredentials;
        }
    }
}
