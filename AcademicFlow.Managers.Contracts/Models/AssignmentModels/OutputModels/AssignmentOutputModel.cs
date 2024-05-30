using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Managers.Contracts.Models.CourseModels;
using AcademicFlow.Managers.Contracts.Models.UserModels;
using AutoMapper;

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
    }

    public class AssignmentsOutputModel
    {
        public AssignmentTaskOutputModel? AssignmentTaskOutputModel { get; set; }
        public IEnumerable<AssignmentEntityOutputModel> AssignmentEntityOutputModels { get; set; }

        public AssignmentsOutputModel()
        { }

        public AssignmentsOutputModel(AssignmentTask assignmentTask, IMapper mapper)
        {
            AssignmentTaskOutputModel = mapper.Map<AssignmentTaskOutputModel>(assignmentTask);
            AssignmentEntityOutputModels = assignmentTask.AssignmentEntries.Select(x => new AssignmentEntityOutputModel()
            {
                AssignmentEntryOutputModel = mapper.Map<AssignmentEntryOutputModel>(x),
                AssignmentGradeOutputModel = mapper.Map<AssignmentGradeOutputModel>(x.AssignmentGrade),
                GradeWithWeight = assignmentTask.AssignmentWeight * x.AssignmentGrade.Grade
            }).ToList();
        }
    }

    public class AssignmentsOutputModelByCourse
    {
        public CourseTableItem Course { get; set; }
        public IEnumerable<AssignmentsOutputModel> AssignmentsOutputModels { get; set; }

        public AssignmentsOutputModelByCourse()
        { }

        public AssignmentsOutputModelByCourse(Course course, IMapper mapper)
        {
            Course = mapper.Map<CourseTableItem>(course);
            AssignmentsOutputModels = course.AssignmentTasks.Select(x => new AssignmentsOutputModel(x, mapper)).ToList();
        }
    }
}
