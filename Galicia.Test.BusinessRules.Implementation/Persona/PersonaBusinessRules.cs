using Galicia.Test.BusinessRules.Person;
using Galicia.Test.Core.Base;
using Galicia.Test.Core.Entitie;
using Galicia.Test.Infrastructure.Data;
using Galicia.Test.Shared.Dto.Persona;
using Galicia.Test.Shared.Profiles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galicia.Test.BusinessRules.Implementation.Person
{
    public class PeronaBusinessRules : IPersonaBusinessRules
    {
        private readonly IBaseRepository<Persona> basePersonaRepository;
        private readonly TestDbContext context;

        public PeronaBusinessRules(IBaseRepository<Persona> basePersonaRepository, TestDbContext context)
        {
            this.basePersonaRepository = basePersonaRepository ?? throw new ArgumentNullException(nameof(basePersonaRepository));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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

            basePersonaRepository.Add(entitie);
            basePersonaRepository.SaveChanges();

            return (entitie.Id.ToString(),true);
        }

        public async Task<bool> DeletePerson(int id)
        {
            var entitiePersona = await context.Personas.FindAsync(id);
            if (entitiePersona is null)
                return false;
            
            basePersonaRepository.DeleteById(id);
            basePersonaRepository.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<PersonaInfoDto>> GetAll()
        {
            var entitie = await context.Personas.AsNoTracking().ToListAsync();
            return AutoMapperConfiguration.mapper.Map<IEnumerable<PersonaInfoDto>>(entitie);
        }

        public async Task<PersonaInfoDto> GetPerson(int id)
        {
            var entitie = await GetPersonaByIdAsync(id);
            return AutoMapperConfiguration.mapper.Map<PersonaInfoDto>(entitie);
        }

        public async Task<(string, bool)> UpdatePerson(PersonaForUpdateDto persona)
        {
            var entitiePersona = await GetPersonaByIdAsync(persona.Id);
            if (entitiePersona is null)
                return ("No se encuentra el registro", false);

            if (await ExistDNI(persona.Id, persona.DNI))
                return ("Ya existe un DNI igual al suministrado", false);

            if (await ExistsTelefono(persona.Id, persona.Telefono))
                return ("Ya existe un Telefono igual al suministrado", false);

            var entitieToUpdate = AutoMapperConfiguration.mapper.Map(persona, entitiePersona);
            basePersonaRepository.Update(entitieToUpdate);
            basePersonaRepository.SaveChanges();
            return ("",true);
        }

        private async Task<bool> ExistDNI(int id, int dni)
        {
            var result = await context.Personas.Where(e => e.DNI == dni && e.Id != id).FirstOrDefaultAsync();
            if (result != null)
                return true;
            
            return false;
        }
        private async Task<bool> ExistsTelefono(int id, string telefono)
        {
            var result = await context.Personas.Where(e => e.Telefono == telefono && e.Id != id).FirstOrDefaultAsync();
            if (result != null)
                return true;

            return false;
        }
        private async Task<Persona> GetPersonaByIdAsync(int id)
        {
            return await context.Personas.Where(e => e.Id == id).Include(e => e.Domicilio).FirstOrDefaultAsync();
        }

    }
}
