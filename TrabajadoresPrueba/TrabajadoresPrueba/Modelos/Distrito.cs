using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TrabajadoresPrueba.Modelos
{
    public class Distrito
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Distrito")]
        public string NombreDistrito { get; set; }
        public int IdProvincia { get; set; }
    }
}
