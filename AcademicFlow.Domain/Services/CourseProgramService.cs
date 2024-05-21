using AcademicFlow.Domain.Contracts.IRepositories;
using AcademicFlow.Domain.Contracts.IServices;

namespace AcademicFlow.Domain.Services
{
    public class CourseProgramService(ICourseProgramRepository courseProgramRepository) : ICourseProgramService
    {
        private readonly ICourseProgramRepository _courseProgramRepository = courseProgramRepository;
    }
}
