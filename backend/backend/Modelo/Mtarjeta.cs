namespace backend.Modelo
{
    public class Mtarjeta
    {
        public int idTarjeta { get; set; }
        public int idCliente { get; set; }
        public int idTipo { get; set; }
        public string? numeroTarjeta { get; set; }
        public string? fechaExpira { get; set; }
        public double? saldoActual { get; set; }
        public double saldoDisponible { get; set; }
        public DateTime? fechaIngreso { get; set; }
        public DateTime? fechaUp { get; set; }
        public int? estado { get; set; }
        public int? cantidad { get; set; }
        //Datos para mostrar los datos del detalle de tarjeta
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public int? sexo { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public string? dui { get; set; }
        public string? direccion { get; set; }
        public string? telefono { get; set; }
        public string? email { get; set; }
        public string? tipo { get; set; }
        public double? interes { get; set; }
        public double? interesMinimo { get; set; }
        public double? montoMaximo { get; set; }
        //Calculos para mostrar en estado de cuenta
        public double? interesBonificable { get; set; }
        public double? cuotaMinima { get; set; }
        public double? saldoIntereses { get; set; }

    }
}
