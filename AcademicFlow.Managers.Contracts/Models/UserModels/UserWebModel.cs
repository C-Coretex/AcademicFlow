using AcademicFlow.Domain.Contracts.Entities;

namespace AcademicFlow.Managers.Contracts.Models.UserModels
{
    public class UserWebModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PersonalCode { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Age { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public UserRegistrationData UserRegistrationData { get; set; }
    }

    public class UserRegistrationData
    {
        public bool IsRegistered { get; set; } = false;
        public string RegistrationUrl { get; set; } = "";

        public UserRegistrationData()
        {}

        public UserRegistrationData(UserCredentials userCredentials)
        {
            if (userCredentials == null)
                return;

            IsRegistered = userCredentials.Username != null && userCredentials.PasswordHash != null;
            if (!IsRegistered)
                RegistrationUrl = userCredentials?.SecurityKey ?? "";
        }
    }


}
