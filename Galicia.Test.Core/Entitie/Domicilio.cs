using Galicia.Test.Core.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Galicia.Test.Core.Entitie
{
    [Table("Domicilio")]
    public class Domicilio : IEntitie
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(70)]
        public string Calle { get; set; }

        [Required]
        public int Altura { get; set; }

        [Required]
        [MaxLength(12)]
        public string Departamento { get; set; }

        [Required]
        public int CodigoPostal { get; set; }

        public int IdPersona { get; set; }
        
        [ForeignKey("IdPersona")]
        public virtual Persona Persona { get; set; }
    }
}
