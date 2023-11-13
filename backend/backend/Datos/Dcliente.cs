using backend.Conexion;
using backend.Modelo;
using System.Data;
using System.Data.SqlClient;

namespace backend.Datos
{
    public class Dcliente
    {
        Conexionbd cn = new Conexionbd();
        public async Task <List<Mcliente>> mostrarCliente()
        {
            var lista = new List<Mcliente>();
            using( var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using(var cmd = new SqlCommand("mostrarCliente", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@estado", SqlDbType.Int)).Value = 2;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while(await item.ReadAsync())
                        {
                            var mcliente = new Mcliente();
                            mcliente.idCliente = (int)item["idCliente"];
                            mcliente.nombre = (string)item["nombre"];
                            mcliente.apellido = (string)item["apellido"];
                            mcliente.sexo = (int)item["sexo"];
                            mcliente.fechaNacimiento = (DateTime)item["fechaNacimiento"];
                            mcliente.dui = (string)item["dui"];
                            mcliente.direccion = (string)item["direccion"];
                            mcliente.telefono = (string)item["telefono"];
                            mcliente.email = (string)item["email"];
                            mcliente.estado = (int)item["estado"];
                            mcliente.cantidad = (int)item["cantidad"];

                            lista.Add(mcliente);
                        }
                    }
                }
            }
            return lista;
        }

        public async Task<bool> insertarCliente(Mcliente parametros)
        {
            bool success;
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("insertaCliente", sql))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", parametros.nombre);
                        cmd.Parameters.AddWithValue("@apellido", parametros.apellido);
                        cmd.Parameters.AddWithValue("@sexo", parametros.sexo);
                        cmd.Parameters.AddWithValue("@fechaNacimiento", parametros.fechaNacimiento);
                        cmd.Parameters.AddWithValue("@dui", parametros.dui);
                        cmd.Parameters.AddWithValue("@direccion", parametros.direccion);
                        cmd.Parameters.AddWithValue("@telefono", parametros.telefono);
                        cmd.Parameters.AddWithValue("@email", parametros.email);

                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception here
                        //Console.WriteLine("Error al insertar cliente: " + ex.Message);
                        success = false;
                    }
                }
            }

            return success;
        }

        public async Task<List<Mcliente>> datosCliente(Mcliente parametros)
        {
            var lista = new List<Mcliente>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("datosCliente", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idCliente", SqlDbType.Int)).Value = parametros.idCliente;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var mcliente = new Mcliente();
                            mcliente.idCliente = (int)item["idCliente"];
                            mcliente.nombre = (string)item["nombre"];
                            mcliente.apellido = (string)item["apellido"];
                            mcliente.sexo = (int)item["sexo"];
                            mcliente.fechaNacimiento = (DateTime)item["fechaNacimiento"];
                            mcliente.dui = (string)item["dui"];
                            mcliente.direccion = (string)item["direccion"];
                            mcliente.telefono = (string)item["telefono"];
                            mcliente.email = (string)item["email"];
                            mcliente.estado = (int)item["estado"];

                            lista.Add(mcliente);
                        }
                    }
                }
            }
            return lista;
        }

        public async Task<bool> editarCliente(Mcliente parametros)
        {
            bool success;
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("editarCliente", sql))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idCliente", parametros.idCliente);
                        cmd.Parameters.AddWithValue("@nombre", parametros.nombre);
                        cmd.Parameters.AddWithValue("@apellido", parametros.apellido);
                        cmd.Parameters.AddWithValue("@sexo", parametros.sexo);
                        cmd.Parameters.AddWithValue("@fechaNacimiento", parametros.fechaNacimiento);
                        cmd.Parameters.AddWithValue("@dui", parametros.dui);
                        cmd.Parameters.AddWithValue("@direccion", parametros.direccion);
                        cmd.Parameters.AddWithValue("@telefono", parametros.telefono);
                        cmd.Parameters.AddWithValue("@email", parametros.email);

                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception here
                        //Console.WriteLine("Error al insertar cliente: " + ex.Message);
                        success = false;
                    }
                }
            }

            return success;
        }
        public async Task<bool> eliminarCliente(Mcliente parametros)
        {
            bool success;
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("eliminarCliente", sql))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idCliente", parametros.idCliente);

                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception here
                        //Console.WriteLine("Error al insertar cliente: " + ex.Message);
                        success = false;
                    }
                }
            }

            return success;
        }
    }
}
