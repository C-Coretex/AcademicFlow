using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.Models.UserModels;

namespace AcademicFlow.Managers.Contracts.Models.ProgramModels
{
    public class ProgramWebModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SemesterNr { get; set; }
        public List<UserListModel> Users { get; set; }
    }
}
