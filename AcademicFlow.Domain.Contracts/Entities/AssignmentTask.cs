using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Helpers.Interfaces;

namespace AcademicFlow.Domain.Contracts.Entities
{
    public class AssignmentTask : IModel
    {
        public int Id { get; set; }
        public int CreatedById { get; set; }
        public int CourseId { get; set; }
        public string AssignmentName { get; set; }
        public string AssignmentDescription { get; set; }
        public float AssignmentWeight { get; set; } = 1.0f;
        public DateTime Modified { get; set; }
        public DateTime Deadline { get; set; }

        public virtual ICollection<AssignmentEntry> AssignmentEntries { get; set; }
        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}
