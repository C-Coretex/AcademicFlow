using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Services;
using AcademicFlow.Managers.Contracts.IManagers;
using AcademicFlow.Managers.Extensions;
using AcademicFlow.Managers.Managers;
using AcademicFlow.Domain.Contracts.Enums;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicFlow.Tests
{
    public class CoursesManagerTests
    {
        #region Mocks
        private readonly Mock<ICourseRepository> _courseRepositoryMock = new();
        private readonly Mock<ICourseUserRoleRepository> _courseUserRoleRepositoryMock = new();
        private readonly Mock<IUserRoleRepository> _userRoleRepositoryMock = new();
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<ICourseProgramRepository> _courseProgramRepositoryMock = new();
        #endregion
        private readonly ICourseManager _courseManager;

        private List<Course> _courses = [];
        private List<CourseUserRole> _courseUserRoles = [];
        private List<CourseProgram> _coursePrograms = [];
     
        public CoursesManagerTests()
        {
            BuildDefaultMocks();
            var _courseService = new CourseService(_courseRepositoryMock!.Object, _courseUserRoleRepositoryMock!.Object);
            var _userService = new UserService(_userRepositoryMock!.Object);
            var _userRoleService = new UserRoleService(_userRoleRepositoryMock!.Object);
            var _courseProgramService = new CourseProgramService(_courseProgramRepositoryMock!.Object);
            var mapper = ServiceCollectionExtensions.GetMapperConfiguration().CreateMapper();

            _courseManager = new CourseManager(mapper, _courseService, _courseProgramService, _userService, _userRoleService);
        }

        [Fact]
        public async Task AddCourseAsync_ShouldReturnInt()
        {
            // Arrange
            var newCourse = new Course()
            {
                Name = "Test1",
                Description = "Test1a",
                CreditPoints = 19,
                ImageUrl = "heroimage.img",
                PublicId = "DatZ4019-LV"
            };

            var courseCount = _courses.Count();

            // Act
            var result = await _courseManager.AddCourseAsync(newCourse);

            // Assert
            Assert.NotNull(result);
            Assert.True(_courses.Count() == courseCount + 1);
        }

        [Fact]
        public async Task DeleteCourseAsync_ShouldReturnOk()
        {
            // Arrange
            var id = 1;
            var courseCount = _courses.Count();

            // Act
            await _courseManager.DeleteCourseAsync(id);

            // Assert
            Assert.True(_courses.Count() == courseCount - 1);
        }

        [Fact]
        public async Task GetCourseByIdAsync_ShouldReturnCourse()
        {
            // Arrange
            var id = 1;

            // Act
            var result = _courseManager.GetCourseById(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Course>(result);
        }

        [Fact]
        public async Task UpdateCourseAsync_ShouldReturnOk()
        {
            // Arrange
            var id = 1;

            var course = _courseManager.GetCourseById(id);
            Assert.NotNull(course);

            course.Name = "Course 1 Update";

            // Act
            await _courseManager.UpdateCourseAsync(course);

            // Assert
            Assert.True(_courses.FirstOrDefault(p => p.Id == id).Name == course.Name);
        }

        [Fact]
        public async Task GetCourseTableItemList_ShouldReturnIEnumerable()
        {
            // Arrange
            int? userId = null;
            int? programId = null;
            RolesEnum role = (RolesEnum)2;

            // Act
            var courses = _courseManager.GetCourseTableItemList(userId, role, programId).ToList();

            // Assert
            Assert.True(_courses.Count() == courses.Count());
        }

        [Fact]
        public async Task GetCourseUsers_ShouldReturnIEnumerable()
        {
            // Arrange
            int id = 1;
            RolesEnum role = (RolesEnum)2;
            // Act
            var courseUsers = _courseManager.GetCourseUsers(id, role).ToList();

            // Assert
            Assert.NotNull(courseUsers);
            Assert.IsType<List<User>>(courseUsers);
        }

        [Fact]
        public async Task GetCoursePrograms_ShouldReturnIEnumerable()
        {
            // Arrange
            int courseId = 1;

            // Act
            var coursePrograms = _courseManager.GetCoursePrograms(courseId).ToList();

            // Assert
            Assert.NotNull(coursePrograms);
            Assert.IsType<List<Domain.Contracts.Entities.Program>>(coursePrograms);
        }

        private void BuildDefaultMocks()
        {
            _courses = new List<Course>
            {
                new()
                {
                    Id = 1,
                    Name = "Course 1",
                    Description = "Description 1",
                    CreditPoints = 19,
                    ImageUrl = "heroimage.img",
                    PublicId = "DatZ4019-LV"
                }
            };

            _courseUserRoles = new List<CourseUserRole>
            {
                new() {
                    Id = 1,
                    CourseId = 1,
                    UserRoleId = 1,
                    Course = new()
                    {
                        Id = 1,
                        Name = "Course 1",
                        Description = "Description 1",
                        CreditPoints = 19,
                        ImageUrl = "heroimage.img",
                        PublicId = "DatZ4019-LV"
                    },
                    UserRole = new()
                    {
                        Id = 1,
                        UserId = 1,
                        Role =  (RolesEnum)2,
                        User = new()
                        {
                            Id = 1,
                            Name = "Name 1",
                            Surname = "Surname 1",
                            PersonalCode = "29567-26372"
                        }
                    }
                }
            };

            _coursePrograms = new List<CourseProgram>
            {
                new() {
                    Id = 1,
                    CourseId = 1,
                    ProgramId = 1,
                    Course = new()
                    {
                        Id = 1,
                        Name = "Course 1",
                        Description = "Description 1",
                        CreditPoints = 19,
                        ImageUrl = "heroimage.img",
                        PublicId = "DatZ4019-LV"
                    },
                    Program = new()
                    {
                        Id = 1,
                        Name = "Program 1",
                        SemesterNr = 1
                    }
                }
            };

            _courseRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Course>(), true)).Callback<Course, bool>((x, y) =>
            {
                x.Id = _courses.Max(u => u.Id) + 1;
                _courses.Add(x);
            }).Returns<Course, bool>((x, y) => Task.FromResult(x));

            _courseRepositoryMock.Setup(x => x.DeleteAsync(It.IsAny<int>(), true)).Callback<int, bool>((x, y) =>
            {
                var courseToRemove = _courses.FirstOrDefault(p => p.Id == x);

                if (courseToRemove != null)
                {
                    _courses.Remove(courseToRemove);
                }
            });

            _courseRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>(), true)).Returns<int, bool>((x, y) => Task.FromResult(_courses.FirstOrDefault(p => p.Id == x)));

            _courseRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Course>(), true)).Callback<Course, bool>((x, y) =>
            {
                var courseToUpdate = _courses.FirstOrDefault(p => p.Id == x.Id);

                if (courseToUpdate != null)
                {
                    courseToUpdate.Name = x.Name;
                }
            });

            _courseRepositoryMock.Setup(x => x.GetAll(true)).Returns(_courses.AsQueryable().BuildMock());

            _courseUserRoleRepositoryMock.Setup(x => x.GetAll(true)).Returns(_courseUserRoles.AsQueryable().BuildMock());

            _courseProgramRepositoryMock.Setup(x => x.GetAll(true)).Returns(_coursePrograms.AsQueryable().BuildMock());
        }
}
}
