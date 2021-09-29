using Galicia.Test.Shared.Dto.Domicilio;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Galicia.Test.Shared.Dto.Persona
{
    public class PersonaForUpdateDto
    {
        [Required(ErrorMessage = "El id de la persona es un valor requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es un valor requerido")]
        [MaxLength(20, ErrorMessage = "El maximo de caracteres compredidos es de 20")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El Apellido es un valor requerido")]
        [MaxLength(20, ErrorMessage = "El maximo de caracteres compredidos es de 20")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El DNI es un valor requerido")]
        public int DNI { get; set; }

        [MaxLength(12, ErrorMessage = "El maximo de caracteres comprendido es de 20")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Faltan datos para el domicilio")]
        public virtual DomicilioForCreateDto Domicilio { get; set; }
    }
}
