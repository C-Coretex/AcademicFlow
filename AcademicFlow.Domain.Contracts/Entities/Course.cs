﻿using AcademicFlow.Domain.Helpers.Interfaces;

namespace AcademicFlow.Domain.Contracts.Entities
{
    public class Course : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreditPoints { get; set; }

        /// <summary>
        /// Assigned propgrams
        /// </summary>
        public virtual ICollection<CourseProgram>? Programs { get; set; }

        /// <summary>
        /// Assigned users (as a Proffesor or as a Student)
        /// </summary>
        public virtual ICollection<CourseUserRole>? Users { get; set; }

        public Course() { }
        public Course(string name, string description, int creditPoints)
        {
            Name = name;
            Description = description;
            CreditPoints = creditPoints;
        }
    }
}
