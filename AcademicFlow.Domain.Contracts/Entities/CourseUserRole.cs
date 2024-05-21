using AcademicFlow.Domain.Helpers.Interfaces;

namespace AcademicFlow.Domain.Contracts.Entities
{
    public class CourseUserRole : IModel
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public int UserRoleId { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
