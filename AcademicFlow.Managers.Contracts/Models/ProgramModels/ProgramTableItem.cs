namespace AcademicFlow.Managers.Contracts.Models.ProgramModels
{
    public class ProgramTableItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SemesterNr { get; set; }
        public IEnumerable<string>? CourseNames { get; set; }
    }
}
