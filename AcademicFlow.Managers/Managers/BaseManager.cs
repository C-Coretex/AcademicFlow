using AutoMapper;

namespace AcademicFlow.Managers.Managers
{
    public class BaseManager
    {
        public BaseManager(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected IMapper Mapper;
        protected IConfigurationProvider MapperConfig => Mapper.ConfigurationProvider;
    }
}
