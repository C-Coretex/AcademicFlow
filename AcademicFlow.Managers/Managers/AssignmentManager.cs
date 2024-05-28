using AcademicFlow.Domain.Contracts.IServices;
using AcademicFlow.Managers.Contracts.IManagers;

namespace AcademicFlow.Managers.Managers
{
    public class AssignmentManager: IAssignmentManager
    {
        private readonly IAssignmentTaskService _assignmentTaskService;
        private readonly IAssignmentEntryService _assignmentEntryService;
        private readonly IAssignmentGradeService _assignmentGradeService;
        public AssignmentManager(IAssignmentTaskService assignmentTaskService, IAssignmentEntryService assignmentEntryService, IAssignmentGradeService assignmentGradeService)
        {
            _assignmentTaskService = assignmentTaskService;
            _assignmentEntryService = assignmentEntryService;
            _assignmentGradeService = assignmentGradeService;
        }


    }
}
