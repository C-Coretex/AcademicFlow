using AcademicFlow.Domain.Entities;
using AcademicFlow.Domain.Helpers.Interfaces;

namespace AcademicFlow.Domain.Contracts.Entities
{
    public class UserCredentials: IModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Salt { get; set; }
        public string? SecurityKey { get; set; } = null;

        public User User { get; set; }
    }
}
