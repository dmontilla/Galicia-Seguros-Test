using Galicia.Test.Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galicia.Test.Core.Entitie
{
    [Table("Persona")]
    public class Persona : IEntitie
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(20)]
        public string Apellido { get; set; }

        [Required]
        public int DNI { get; set; }

        [MaxLength(12)]
        public string Telefono { get; set; }

        public virtual Domicilio Domicilio { get; set; }

    }
}
