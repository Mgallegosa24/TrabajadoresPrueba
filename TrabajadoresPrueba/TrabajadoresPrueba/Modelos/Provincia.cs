using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TrabajadoresPrueba.Modelos
{
    public class Provincia
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Provincia")]
        public string NombreProvincia { get; set; }
        public int IdDepartamento { get; set; }
    }
}
