using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Helpers.Interfaces;

namespace AcademicFlow.Domain.Contracts.Entities
{
    public class AssignmentEntry : IModel
    {
        public int Id { get; set; }
        public int AssignmentTaskId { get; set; }
        public int CreatedById { get; set; }
        public string AssignmentFilePath { get; set; }
        public DateTime Modified { get; set; }

        public virtual AssignmentTask AssignmentTask { get; set; }
        public virtual AssignmentGrade AssignmentGrade { get; set; }
        public virtual User User { get; set; }
    }
}
