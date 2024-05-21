﻿using AcademicFlow.Domain.Helpers.Interfaces;

namespace AcademicFlow.Domain.Contracts.Entities
{
    public class Program : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int SemesterNr {  get; set; }

        public virtual ICollection<CourseProgram>? Courses { get; set; }
        public virtual ICollection<ProgramUserRole>? Users { get; set; }
    }
}
