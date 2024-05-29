using AcademicFlow.Managers.Contracts.Models.UserModels;

namespace AcademicFlow.Managers.Contracts.Models.AssignmentModels.OutputModels
{
    public class AssignmentTaskOutputModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string AssignmentName { get; set; }
        public string AssignmentDescription { get; set; }
        public float AssignmentWeight { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime Modified { get; set; }

        public UserWebModel CreatedBy { get; set; }
    }

    public class AssignmentEntryOutputModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTime Modified { get; set; }

        public UserWebModel CreatedBy { get; set; }
    }

    public class AssignmentGradeOutputModel
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string Comment { get; set; }
        public DateTime Modified { get; set; }

        public UserWebModel GradedBy { get; set; }
    }

    public class AssignmentEntityOutputModel
    {
        public AssignmentEntryOutputModel? AssignmentEntryOutputModel { get; set; }
        public AssignmentGradeOutputModel? AssignmentGradeOutputModel { get; set; }

        public float? GradeWithWeight { get; set; }
        /*(AssignmentTaskOutputModel != null && AssignmentGradeOutputModel != null) ? 
                                         (AssignmentTaskOutputModel.AssignmentWeight + AssignmentGradeOutputModel.Grade) : null;*/
    }

    public class AssignmentsOutputModel
    {
        public AssignmentTaskOutputModel? AssignmentTaskOutputModel { get; set; }
        public IEnumerable<AssignmentEntityOutputModel> AssignmentEntityOutputModels { get; set; }
    }
}
