using Galicia.Test.Shared.Dto.Persona;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galicia.Test.BusinessRules.Person
{
    public interface IPersonaBusinessRules
    {
        /// <summary>
        /// Retorna todos los registros de Persona existentes
        /// </summary>
        /// <returns>Retorna <see cref="PersonaInfoDto>"/></returns>
        Task<IEnumerable<PersonaInfoDto>> GetAll();

        /// <summary>
        /// Retorna una persona segun el id suministrado
        /// </summary>
        /// <param name="id">Id de la persona a buscar</param>
        /// <returns>Retorna <see cref="IEnumerable{T}"/></returns>
        Task<PersonaInfoDto> GetPerson(int id);

        /// <summary>
        /// Agrega una nueva persona a la entidad
        /// </summary>
        /// <param name="persona">Datos del objeto persona</param>
        /// <returns>Retorna <see cref="(string,bool)"/></returns>
        Task<(string, bool)> AddPerson(PersonaForCreateDto persona);

        /// <summary>
        /// Actualiza los datos de una persona
        /// </summary>
        /// <param name="persona">Datos del objeto persona</param>
        /// <returns>Retorna <see cref="(string,bool)"/></returns>
        Task<(string, bool)> UpdatePerson(PersonaForUpdateDto persona);

        /// <summary>
        /// Elimina el registro de una persona
        /// </summary>
        /// <param name="id">Id de la persona a eliminar</param>
        /// <returns>Retorna <see cref="bool"/></returns>
        Task<bool> DeletePerson(int id);
    }
}
