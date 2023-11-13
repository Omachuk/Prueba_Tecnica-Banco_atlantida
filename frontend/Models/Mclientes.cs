using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
    public class Mclientes
    {
        public int idCliente { get; set; }
        [Required]
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        [Required]
        [DisplayName("Apellido")]
        public string apellido { get; set; }
        [Required]
        [DisplayName("Sexo")]
        public int sexo { get; set; }
        [Required]
        [DisplayName("Fecha nacimiento")]
        public DateTime fechaNacimiento { get; set; }
        [Required]
        [DisplayName("Dui")]
        public string dui { get; set; }
        [Required]
        [DisplayName("Direccion")]
        public string direccion { get; set; }
        [Required]
        [DisplayName("Telefono")]
        public string telefono { get; set; }
        [Required]
        [DisplayName("Email")]
        public string email { get; set; }
        [DisplayName("Estado")]
        public int estado { get; set; }
        [DisplayName("T. activas")]
        public int? cantidad { get; set; }
    }
}
