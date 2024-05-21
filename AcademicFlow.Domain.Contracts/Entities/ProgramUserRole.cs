﻿using AcademicFlow.Domain.Helpers.Interfaces;

namespace AcademicFlow.Domain.Contracts.Entities
{
    public class ProgramUserRole : IModel
    {
        public int Id { get; set; }

        public int UserRoleId { get; set; }
        public virtual UserRole User { get; set; }

        public int ProgramId { get; set; }
        public virtual Program Program { get; set; }
    }
}
