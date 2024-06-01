using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Services;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Extensions;
using AcademicFlow.Managers.Managers;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicFlow.Tests
{
    public class ProgramManagerTests
    {
        #region Mocks
        private readonly Mock<IProgramRepository> _programRepositoryMock = new();
        private readonly Mock<IProgramUserRoleRepository> _programUserRoleRepositoryMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        #endregion

        private readonly IProgramManager _programManager;

        private List<Program> _programs = [];
        private List<ProgramUserRole> _programUserRoles = [];

        public ProgramManagerTests()
        {
            BuildDefaultMocks();
            var _programService = new ProgramService(_programRepositoryMock!.Object, _programUserRoleRepositoryMock!.Object);
            var _userService = new UserService(_userRepositoryMock!.Object);
            var mapper = ServiceCollectionExtensions.GetMapperConfiguration().CreateMapper();

            _programManager = new ProgramManager(mapper, _programService, _userService);
        }

        [Fact]
        public async Task AddProgramAsync_ShouldReturnInt()
        {
            // Arrange
            var program = new Program("Test program", 1);
            var programCount = _programs.Count();

            // Act
            var result = await _programManager.AddProgramAsync(program);

            // Assert
            Assert.NotNull(result);
            Assert.True(_programs.Count() == programCount + 1);
        }

        [Fact]
        public async Task DeleteProgramAsync_ShouldReturnOk()
        {
            // Arrange
            var id = 1;
            var programCount = _programs.Count();

            // Act
            await _programManager.DeleteProgramAsync(id);

            // Assert
            Assert.True(_programs.Count() == programCount - 1);
        }

        [Fact]
        public async Task GetProgramByIdAsync_ShouldReturnProgram()
        {
            // Arrange
            var id = 1;

            // Act
            var result = await _programManager.GetProgramByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Program>(result);
        }

        [Fact]
        public async Task UpdateProgramAsync_ShouldReturnOk()
        {
            // Arrange
            var id = 1;

            var program = await _programManager.GetProgramByIdAsync(id);
            Assert.NotNull(program);

            program.Name = "Program 1 Update";

            // Act
            await _programManager.UpdateProgramAsync(program);

            // Assert
            Assert.True(_programs.FirstOrDefault(p => p.Id == id).Name == program.Name);
        }

        [Fact]
        public async Task GetProgramTableItemList_ShouldReturnIEnumerable()
        {
            // Arrange
            int? userId = null;

            // Act
            var programs = _programManager.GetProgramTableItemList(userId).ToList();

            // Assert
            Assert.True(_programs.Count() == programs.Count());
        }

        [Fact]
        public async Task GetProgramUsers_ShouldReturnIEnumerable()
        {
            // Arrange
            int id = 1;

            // Act
            var programUsers = _programManager.GetProgramUsers(id).ToList();

            // Assert
            Assert.NotNull(programUsers);
            Assert.IsType<List<User>>(programUsers);
        }

        private void BuildDefaultMocks()
        {
            _programs = new List<Program>
            {
                new() 
                {
                    Id = 1,
                    Name = "Program 1",
                    SemesterNr = 1,
                }
            };

            _programUserRoles = new List<ProgramUserRole>
            {
                new() {
                    Id = 1,
                    UserRoleId = 1,
                    ProgramId = 1,
                    Program = new() 
                    {
                        Id = 1,
                        Name = "Program 1",
                        SemesterNr = 1,
                    },
                    UserRole = new()
                    {
                        Id = 1,
                        UserId = 1,
                        User = new()
                        {
                            Id = 1,
                            Name = "Name 1",
                            Surname = "Surname 1",
                        }
                    }
                }
            };

            _programRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Program>(), true)).Callback<Program, bool>((x, y) =>
            {
                x.Id = _programs.Max(u => u.Id) + 1;
                _programs.Add(x);
            }).Returns<Program, bool>((x, y) => Task.FromResult(x));

            _programRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<int>(), true)).Callback<int, bool>((x, y) =>
            {
                var programToRemove = _programs.FirstOrDefault(p => p.Id == x);

                if (programToRemove != null)
                {
                    _programs.Remove(programToRemove);
                }
            });

            _programRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), true)).Returns<int, bool>((x, y) => Task.FromResult(_programs.FirstOrDefault(p => p.Id == x)));

            _programRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Program>(), true)).Callback<Program, bool>((x, y) =>
            {
                var programToUpdate = _programs.FirstOrDefault(p => p.Id == x.Id);

                if (programToUpdate != null)
                {
                    programToUpdate.Name = x.Name;
                }
            });

            _programRepositoryMock.Setup(x => x.GetAll(true)).Returns(_programs.AsQueryable().BuildMock());

            _programUserRoleRepositoryMock.Setup(x => x.GetAll(true)).Returns(_programUserRoles.AsQueryable().BuildMock());
        }
    }
}
