using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
    public class MtipoTarjeta
    {
        public int idTipo { get; set; }
        public int? idInteres { get; set; }
        [Required]
        [DisplayName("Tipo tarjeta")]
        public string tipo { get; set; }
        [Required]
        [DisplayName("Monto maximo")]
        public double? montoMaximo { get; set; }
        [Required]
        [DisplayName("Descripcion")]
        public string descripcion { get; set; }
        [Required]
        [DisplayName("Fecha de ingreso")]
        public DateTime fechaIngreso { get; set; }
        [Required]
        [DisplayName("Estado")]
        public int estado { get; set; }
        [Required]
        [DisplayName("Tipo de interes")]
        public string? nombre { get; set; }
        public double? interes { get; set; }
        public double? interesMinimo { get; set; }
    }
}
