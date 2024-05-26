using AcademicFlow.Domain.Helpers.Interfaces;

namespace AcademicFlow.Domain.Contracts.Entities
{
    public class Program : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int SemesterNr {  get; set; }

        public ICollection<CourseProgram>? Courses { get; set; }
        public ICollection<ProgramUserRole>? UserRoles { get; set; }

        public Program() { }
        public Program(string name, int semesterNr)
        {
            Name = name;
            SemesterNr = semesterNr;
        }
    }
}
