using Galicia.Test.BusinessRules.Person;
using Galicia.Test.Shared.Dto.Persona;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Galicia.Test.API.Controllers.ClientesController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonaBusinessRules personBusinessRules;

        public PersonController(IPersonaBusinessRules personBusinessRules)
        {
            this.personBusinessRules = personBusinessRules;
        }

        [HttpGet]
        [Route("{id:int:required}")]
        public async Task<ActionResult<PersonaInfoDto>> Get(int id)
        {
            try
            {
                var person = await personBusinessRules.GetPerson(id);
                if (person is null)
                    return NotFound("Recurso no encontrado");

                return Ok(person);
            }
            catch (Exception ex)
            {
                //TODO : LOGS
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno, consulte a sistemas");
            }
        }
        
    }
}
