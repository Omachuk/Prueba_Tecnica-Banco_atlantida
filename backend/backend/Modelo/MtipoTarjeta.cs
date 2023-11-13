namespace backend.Modelo
{
    public class MtipoTarjeta
    {
        public int idTipo { get; set; }
        public int idInteres { get; set; }
        public string tipo { get; set; }
        public double montoMaximo { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaIngreso { get; set; }
        public int estado { get; set; }

        public string? nombre { get; set; }
        public double? interes { get; set; }
        public double? interesMinimo { get; set; }
    }
}
