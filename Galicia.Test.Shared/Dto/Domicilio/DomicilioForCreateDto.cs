using Galicia.Test.Shared.CustomValidations;
using System.ComponentModel.DataAnnotations;

namespace Galicia.Test.Shared.Dto.Domicilio
{
    public class DomicilioForCreateDto
    {
        [Required(ErrorMessage = "La callee es un valor requerido")]
        [MaxLength(70, ErrorMessage = "El maximo de caracteres compredidos es de 70")]
        public string Calle { get; set; }

        [Required(ErrorMessage = "La altura es un valor requerido")]
        [MinValuePropertyValidation(minValue: 0, ErrorMessage = "La altura de la calle debe ser mayor a 0")]
        public int Altura { get; set; }

        [Required(ErrorMessage = "El número de departamento es un valor requerido")]
        [MaxLength(12, ErrorMessage = "El maximo de caracteres compredidos es de 12")]
        public string Departamento { get; set; }

        [Required(ErrorMessage = "El Código Postal es un valor requerido")]
        [MinValuePropertyValidation(minValue: 0, ErrorMessage = "El codigo postal debe ser mayor a 0")]
        public int CodigoPostal { get; set; }
    }
}
