using AcademicFlow.Domain.Helpers.Interfaces;

namespace AcademicFlow.Domain.Contracts.Entities
{
    public class CourseProgram : IModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }
    }
}
