using AcademicFlow.Domain.Entities;
using AcademicFlow.Managers.Contracts.Models.UserModels;

namespace AcademicFlow.Managers.Contracts.Models.CourseModels
{
    public class CourseWebModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreditPoints { get; set; }
        public string PublicId { get; set; }
        public string? ImageUrl { get; set; }
        public List<UserListModel> AssigmentUsers { get; set; }
    }
}
