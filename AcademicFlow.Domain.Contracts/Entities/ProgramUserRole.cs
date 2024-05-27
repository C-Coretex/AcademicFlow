using AcademicFlow.Domain.Helpers.Interfaces;

namespace AcademicFlow.Domain.Contracts.Entities
{
    public class ProgramUserRole : IModel
    {
        public int Id { get; set; }

        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }

        public int ProgramId { get; set; }
        public Program Program { get; set; }
    }
}
