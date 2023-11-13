namespace backend.Modelo
{
    public class Mmovimientos
    {
        public int idMovimiento { get; set; }
        public int idTarjeta { get; set; }
        public double monto { get; set; }
        public DateTime fechaCompra { get; set; }
        public DateTime? fechaIngreso { get; set; }
        public string? descripcion { get; set; }
        public int activo { get; set; }
        public int tipoMovimiento { get; set; }
        public string? numeroTarjeta { get; set; }
        public string? mes { get; set; }
        public double? total { get; set; }
    }
}
