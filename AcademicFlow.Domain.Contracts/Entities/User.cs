using AcademicFlow.Domain.Contracts.Entities;
using AcademicFlow.Domain.Helpers.Interfaces;

namespace AcademicFlow.Domain.Entities
{
    public class User: IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PersonalCode { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Age { get; set; }
        public int? IsDeleted { get; set; }
        // public bool IsDeleted { get; set; } = false;

        public UserCredentials UserCredentials { get; set; }
        public List<UserRole> UserRoles { get; set; }
        
        public User()
        {}

        public User(string name, string surname, string personalCode, string? email = null, string? phoneNumber = null, int? age = null)
        {
            Name = name;
            Surname = surname;
            PersonalCode = personalCode;
            Email = email;
            PhoneNumber = phoneNumber;
            Age = age;
            IsDeleted = 0;
        }
    }
}
