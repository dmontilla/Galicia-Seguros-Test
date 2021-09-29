using AutoMapper;
using Galicia.Test.Core.Entitie;
using Galicia.Test.Shared.Dto.Domicilio;
using Galicia.Test.Shared.Dto.Persona;

namespace Galicia.Test.Shared.Profiles
{
    public static class AutoMapperConfiguration
    {
        public static IMapper mapper;
        public static void CreateMapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Persona, PersonaInfoDto>()
                    .ForPath(d => d.Domicilio, s => s.MapFrom(m => m.Domicilio));

                cfg.CreateMap<Domicilio, DomicilioInfoDto>();

                cfg.CreateMap<PersonaForCreateDto, Persona>()
                    .ForPath(d => d.Domicilio, s => s.MapFrom(m => m.Domicilio));

                cfg.CreateMap<DomicilioForCreateDto, Domicilio>();
                cfg.CreateMap<PersonaForUpdateDto, Persona>()
                    .ForPath(d => d.Domicilio, s => s.MapFrom(m => m.Domicilio));
            });

            mapper = config.CreateMapper();
        }
    }
}
