namespace AcademicFlow.Managers.Contracts.Models.CourseModels
{
    public class CourseTableItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreditPoints { get; set; }
        public string PublicId { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
