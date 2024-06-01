using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Services;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Extensions;
using AcademicFlow.Managers.Managers;
using MockQueryable.Moq;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace AcademicFlow.Tests
{
    public class UserManagerTests
    {
        #region Mocks
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        #endregion

        private List<User> _users = [];
        private readonly IUserManager _userManager;
        public UserManagerTests()
        {
            BuildDefaultMocks();
            var _userService = new UserService(_userRepositoryMock!.Object);
            var mapper = ServiceCollectionExtensions.GetMapperConfiguration().CreateMapper();
            _userManager = new UserManager(mapper, _userService);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnUsers()
        {
            // Arrange
            var controllerUrl = "";

            // Act
            var result = await _userManager.GetUsers(controllerUrl).ToListAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count == _users.Count);
            Assert.True(result.All(u => _users.Any(x => x.Id == u.Id)));
        }

        [Fact]
        public async Task AddUser_ShouldAddUser_IfEverythingFine()
        {
            // Arrange
            var newUser = new User()
            {
                Name = "Test",
                Surname = "Test2",
                PersonalCode = "54321-01234",
            };

            // Act
            await _userManager.AddUser(newUser);
        }

        [Fact]
        public async Task AddUser_ShouldReturnException_IfPersonalCodeIsNotUnique()
        {
            // Arrange
            var newUser = new User()
            {
                Name = "Test",
                Surname = "Test2",
                PersonalCode = "52386-67110",
            };

            // Act
            var func = async () => await _userManager.AddUser(newUser);

            // Assert
            await Assert.ThrowsAsync<ValidationException>(func);
        }

        private void BuildDefaultMocks()
        {
            _users = new List<User>
            {
                new() {
                    Id = 1,
                    Name = "Name 1",
                    Surname = "Surname 1",
                    PersonalCode = "12345-67890",
                    Email = "test@email.com",
                    PhoneNumber = "+37125477836",
                    Age = 23,
                    IsDeleted = false,
                    UserCredentials = new ()
                    {
                        Id = 1,
                        Username = "Test 1",
                        PasswordHash = "Āsdasdasdasdasdad",
                        Salt = "asdasdasd",
                        SecurityKey = "gfdgdg",
                    }
                },
                new() {
                    Id = 2,
                    Name = "Name 2",
                    Surname = "Surname 2",
                    PersonalCode = "52386-67110",
                    Email = "test2@email.com",
                    IsDeleted = false,
                    UserCredentials = new ()
                    {
                        Id = 2,
                        Username = "Test 2",
                        PasswordHash = "Āsdasdasdasdasdad",
                        Salt = "asdasdasd",
                        SecurityKey = "gfdgdg",
                    }
                },
                new() {
                    Id = 3,
                    Name = "Name 3",
                    Surname = "Surname 3",
                    PersonalCode = "17345-67854",
                    IsDeleted = false,
                    UserCredentials = new ()
                    {
                        Id = 3,
                        Username = "Test 3",
                        PasswordHash = "Āsdasdasdasdasdad",
                        Salt = "asdasdasd",
                        SecurityKey = "gfdgdg",
                    }
                },
            };

            var asNoTracking = true;

            //mock IAsyncQueryable/IAsyncEnumerable
            var usersMock = _users.AsQueryable().BuildMock();
            _userRepositoryMock.Setup(x => x.GetAll(asNoTracking)).Returns(usersMock);
            _userRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>(), It.IsAny<bool>())).Callback<User, bool>((x, y) =>
            {
                x.Id = _users.Max(u => u.Id) + 1;
                _users.Add(x);
            });
        }
    }
}
