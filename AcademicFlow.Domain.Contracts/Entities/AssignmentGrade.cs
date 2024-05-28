using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Helpers.Interfaces;

namespace AcademicFlow.Domain.Contracts.Entities
{
    public class AssignmentGrade: IModel
    {
        public int Id { get; set; }
        public int AssignmentEntryId { get; set; }
        public int Grade { get; set; }
        public int GradedById { get; set; }
        public string Comment { get; set; }
        public DateTime Modified {  get; set; }

        public virtual AssignmentEntry AssignmentEntry { get; set; }
        public virtual User User { get; set; }
    }
}
