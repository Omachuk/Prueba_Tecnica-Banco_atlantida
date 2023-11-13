namespace backend.Modelo
{
    public class Mcliente
    {
        public int idCliente { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public int sexo { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string dui { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public int estado { get; set; }
        public int? cantidad { get; set; }
    }
}
