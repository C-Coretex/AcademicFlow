namespace AcademicFlow.Managers.Contracts.Models.UserModels
{
    public class UserListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PersonalCode { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Age { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
