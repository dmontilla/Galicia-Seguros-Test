using Galicia.Test.Shared.Dto.Domicilio;

namespace Galicia.Test.Shared.Dto.Persona
{
    public class PersonaInfoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int DNI { get; set; }
        public string Telefono { get; set; }
        public virtual DomicilioInfoDto Domicilio { get; set; }
    }
}
