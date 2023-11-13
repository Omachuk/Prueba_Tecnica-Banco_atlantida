using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
    public class Minteres
    {
        public int idInteres { get; set; }
        [Required]
        [DisplayName("Nombre")]
        public string nombre { get; set; }
        [Required]
        [DisplayName("Interes")]
        public double? interes { get; set; }
        [Required]
        [DisplayName("Interes minimo")]
        public double? interesMinimo { get; set; }
        [Required]
        [DisplayName("Descripcion")]
        public string descripcion { get; set; }
        [DisplayName("Fecha de ingreso")]
        public DateTime fechaIngreso { get; set; }
        [DisplayName("Fecha de actualizacion")]
        public DateTime fechaUp { get; set; }
        [DisplayName("Estado")]
        public int estado { get; set; }
    }
}
