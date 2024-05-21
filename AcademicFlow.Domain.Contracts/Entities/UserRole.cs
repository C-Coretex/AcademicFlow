using AcademicFlow.Domain.Contracts.Enums;
using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Helpers.Interfaces;


namespace AcademicFlow.Domain.Contracts.Entities
{
    public class UserRole : IModel
    {   
        public int Id { get; set; }
        public int UserId { get; set; }
        public RolesEnum Role { get; set; }
        public User User { get; set; }

        public virtual ICollection<CourseUserRole>? Courses { get; set; }
        public virtual ICollection<ProgramUserRole>? Programs { get; set; }

        public UserRole()
        { }

        public UserRole(int userId, RolesEnum role)
        {
            UserId = userId;
            Role = role;
        }
    }
}
