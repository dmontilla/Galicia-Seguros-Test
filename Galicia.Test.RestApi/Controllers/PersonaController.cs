using Galicia.Test.BusinessRules.Person;
using Galicia.Test.Shared.Dto.Persona;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Galicia.Test.RestApi.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaBusinessRules personBusinessRules;
        public PersonaController(IPersonaBusinessRules personBusinessRules)
        {
            this.personBusinessRules = personBusinessRules;
        }
        /// <summary>
        /// Devuelve todas las personas existentes
        /// </summary>
        /// <response code="200">Retorna Registros de persona</response>
        /// <response code="500">Retorna errror en ejecución</response>      
        [HttpGet]
        [Route("getall")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<PersonaInfoDto>>> GetAll()
        {
            try
            {
                var persons = await personBusinessRules.GetAll();
                return Ok(persons);
            }
            catch (Exception)
            {
                //TODO : LOGS
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno, consulte a sistemas");
            }
        }
        /// <summary>
        /// Obtiene la información de una persona
        /// </summary>
        /// <param name="id">Id de la persona</param>
        /// <returns>Retonra un resulado de tipo <see cref="PersonaInfoDto"/></returns>
        /// <response code="200">Retorna el registro de la persona</response>
        /// <response code="404">Retorna aviso de persona no encontrada</response>      
        /// <response code="500">Retorna error en ejecución</response>      
        [HttpGet]
        [Route("{id:int:required}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(PersonaInfoDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PersonaInfoDto>> Get([FromRoute] int id)
        {
            try
            {
                var person = await personBusinessRules.GetPerson(id);
                if (person is null)
                    return NotFound("Persona no encontrada");

                return Ok(person);
            }
            catch (Exception)
            {
                //TODO : LOGS
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno, consulte a sistemas");
            }
        }

        /// <summary>
        /// Registra los datos de una persona
        /// </summary>
        /// <param name="persona">Informacion de la persona a registrar</param>
        /// <response code="201">Retorna Registro creado</response>
        /// <response code="400">Retorna aviso de información mal asignada de la persona</response>      
        /// <response code="500">Retorna error en ejecución</response>      
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] PersonaForCreateDto persona)
        {
            try
            {
                var result = await personBusinessRules.AddPerson(persona);
                if (!result.Item2)
                    return BadRequest(result.Item1);

                return Created("Get", new { id = result.Item1 });
            }
            catch (Exception ex)
            {
                //TODO : LOGS
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno, consulte a sistemas");
            }
        }

        /// <summary>
        /// Realiza la actualización de los datos de una persona
        /// </summary>
        /// <param name="persona">Informacion de la persona a actualizar</param>
        /// <response code="204">Retorna Modificación realizada</response>
        /// <response code="400">Retorna aviso de información mal asignada de la persona</response>      
        /// <response code="500">Retorna error en ejecución</response>      
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] PersonaForUpdateDto persona)
        {
            try
            {
                var result = await personBusinessRules.UpdatePerson(persona);
                if (!result.Item2)
                    return BadRequest(result.Item1);

                return NoContent();
            }
            catch (Exception)
            {
                //TODO: LOGS
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno, consulte a sistemas");
            }
        }

        /// <summary>
        /// Elimina a una persona registrada
        /// </summary>
        /// <param name="id">Id de la persona a eliminar</param>
        /// <response code="204">Persona eliminada</response>
        /// <response code="404">Retorna Persona no encontrada</response>      
        /// <response code="500">Retorna error en ejecución</response>      
        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var result = await personBusinessRules.DeletePerson(id);
                if (!result)
                    return NotFound("Registro no encontrado");

                return NoContent();
            }
            catch (Exception)
            {
                //TODO: LOGS
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno, consulte a sistemas");
            }
        }
    }
}
