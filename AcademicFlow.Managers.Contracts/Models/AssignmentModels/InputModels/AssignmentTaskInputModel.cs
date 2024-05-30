namespace AcademicFlow.Managers.Contracts.Models.AssignmentModels.InputModels
{
    public class AssignmentTaskInputModel
    {
        public int CourseId { get; set; }
        public string AssignmentName { get; set; }
        public string AssignmentDescription { get; set; }
        public float AssignmentWeight { get; set; } = 1.0f;
        public DateTime Deadline { get; set; }
    }
}
