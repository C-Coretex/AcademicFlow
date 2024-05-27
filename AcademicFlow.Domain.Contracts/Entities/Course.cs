using AcademicFlow.Domain.Helpers.Interfaces;

namespace AcademicFlow.Domain.Contracts.Entities
{
    public class Course : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreditPoints { get; set; }
        public string PublicId { get; set; }
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Assigned propgrams
        /// </summary>
        public ICollection<CourseProgram>? Programs { get; set; }

        /// <summary>
        /// Assigned users (as a Professor or as a Student)
        /// </summary>
        public ICollection<CourseUserRole>? Users { get; set; }

        public Course() { }
        public Course(string name, string description, int creditPoints, string publicId, string? imageUrl)
        {
            Name = name;
            Description = description;
            CreditPoints = creditPoints;
            PublicId = publicId;
            ImageUrl = imageUrl;
        }
    }
}
