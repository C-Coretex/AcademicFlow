﻿using AcademicFlow.Domain.Contracts.Entities;

namespace AcademicFlow.Domain.Contracts.IServices
{
    public interface IAssignmentEntryService
    {
        Task<AssignmentEntry?> GetById(int id);
    }
}
