using Galicia.Test.BusinessRules.Person;
using Galicia.Test.Shared.Dto.Persona;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Galicia.Test.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaBusinessRules personBusinessRules;

        public PersonaController(IPersonaBusinessRules personBusinessRules)
        {
            this.personBusinessRules = personBusinessRules;
        }

        [HttpGet]
        [Route("getall")]
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

        [HttpGet]
        [Route("{id:int:required}")]
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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonaForCreateDto persona)
        {
            try
            {
                var result = await personBusinessRules.AddPerson(persona);
                if (!result.Item2)
                    return BadRequest(result.Item1);
                
                return Ok();
            }
            catch (Exception ex)
            {
                //TODO : LOGS
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno, consulte a sistemas");
            }
        }

        [HttpPut]
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

        [HttpDelete]
        [Route("{id:int}")]
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
