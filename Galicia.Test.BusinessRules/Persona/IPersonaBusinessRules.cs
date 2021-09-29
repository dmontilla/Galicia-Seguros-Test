using Galicia.Test.Core.Entitie;
using Galicia.Test.Shared.Dto.Persona;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galicia.Test.BusinessRules.Person
{
    public interface IPersonaBusinessRules
    {
        Task<IEnumerable<PersonaInfoDto>> GetAll();
        Task<PersonaInfoDto> GetPerson(int id);
        Task<(string, bool)> AddPerson(PersonaForCreateDto persona);
        Task<(string, bool)> UpdatePerson(PersonaForUpdateDto persona);
        Task<bool> DeletePerson(int id);
    }
}
