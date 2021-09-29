using Galicia.Test.BusinessRules.Person;
using Galicia.Test.Core.Base;
using Galicia.Test.Core.Entitie;
using Galicia.Test.Shared.Dto.Persona;
using Galicia.Test.Shared.Profiles;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Galicia.Test.BusinessRules.Implementation.Person
{
    public class PeronaBusinessRules : IPersonaBusinessRules
    {
        private readonly IBaseRepository<Persona> personaRepository;

        public PeronaBusinessRules(IBaseRepository<Persona> personaRepository)
        {
            this.personaRepository = personaRepository;
        }
        public async Task<(string,bool)> AddPerson(PersonaForCreateDto persona)
        {
            var entitie = AutoMapperConfiguration.mapper.Map<Persona>(persona);
            if (entitie is null)
                return ("No se realizo la asignacion correctamente",false);

            if (await ExistDNI(0, persona.DNI))
                return ("Ya existe un DNI igual al suministrado", false);

            if (await ExistsTelefono(0, persona.Telefono))
                return ("Ya existe un Telefono igual al suministrado", false);

            personaRepository.Add(entitie);
            personaRepository.SaveChanges();

            return ("",true);
        }

        public async Task<bool> DeletePerson(int id)
        {
            var entitiePersona = await personaRepository.FindByIdAsync(id);
            if (entitiePersona is null)
                return false;
            
            personaRepository.DeleteById(id);
            personaRepository.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<PersonaInfoDto>> GetAll()
        {
            var entitie = await personaRepository.GetAllIncludeAsync(e => e.Domicilio);
            return AutoMapperConfiguration.mapper.Map<IEnumerable<PersonaInfoDto>>(entitie);
        }

        public async Task<PersonaInfoDto> GetPerson(int id)
        {
            Expression<Func<Persona, object>>[] joinTables = { m => m.Domicilio};
            var entitie = await personaRepository.FindByIncludeAsync(e => e.Id == id, joinTables);
            return AutoMapperConfiguration.mapper.Map<PersonaInfoDto>(entitie);
        }

        public async Task<(string, bool)> UpdatePerson(PersonaForUpdateDto persona)
        {
            var entitiePersona = await personaRepository.FindByIncludeAsync(e => e.Id == persona.Id, e => e.Domicilio);
            if (entitiePersona is null)
                return ("No se encuentra el registro", false);

            if (await ExistDNI(persona.Id, persona.DNI))
                return ("Ya existe un DNI igual al suministrado", false);

            if (await ExistsTelefono(persona.Id, persona.Telefono))
                return ("Ya existe un Telefono igual al suministrado", false);

            var entitieToUpdate = AutoMapperConfiguration.mapper.Map(persona, entitiePersona);
            personaRepository.Update(entitieToUpdate);
            personaRepository.SaveChanges();
            return ("",true);
        }

        private async Task<bool> ExistDNI(int id, int dni)
        {
            var result = await personaRepository.WhereAsync(e => e.DNI == dni && e.Id != id);
            if (result != null)
                return true;
            
            return false;
        }
        private async Task<bool> ExistsTelefono(int id, string telefono)
        {
            var result = await personaRepository.WhereAsync(e => e.Telefono == telefono && e.Id != id);
            if (result != null)
                return true;

            return false;
        }
    }
}
