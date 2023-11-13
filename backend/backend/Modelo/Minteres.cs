namespace backend.Modelo
{
    public class Minteres
    {
        public int idInteres { get; set; }
        public string nombre { get; set; }
        public double interes { get; set; }
        public double interesMinimo { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaIngreso { get; set; }
        public DateTime fechaUp { get; set; }
        public int estado { get; set; }
    }
}
