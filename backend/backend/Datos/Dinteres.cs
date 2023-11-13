using backend.Conexion;
using backend.Modelo;
using System.Data.SqlClient;
using System.Data;

namespace backend.Datos
{
    public class Dinteres
    {
        Conexionbd cn = new Conexionbd();
        public async Task<List<Minteres>> mostrarInteres(Minteres parametros)
        {
            var lista = new List<Minteres>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("mostrarInteres", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@estado", SqlDbType.Int)).Value = parametros.estado;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var minteres = new Minteres();
                            minteres.idInteres = (int)item["idInteres"];
                            minteres.nombre = (string)item["nombre"];
                            minteres.interes = (double)item["interes"] * 100;
                            minteres.interesMinimo = (double)item["interesMinimo"] * 100;
                            minteres.descripcion = (string)item["descripcion"];
                            minteres.fechaIngreso = (DateTime)item["fechaIngreso"];
                            if (item["fechaUp"] == DBNull.Value)
                            {
                                minteres.fechaUp = DateTime.MinValue;
                            }
                            else
                            {
                                minteres.fechaUp = (DateTime)item["fechaUp"];
                            }
                            minteres.estado = (int)item["estado"];

                            lista.Add(minteres);
                        }
                    }
                }
            }

            return lista;
        }

        public async Task<bool> insertarInteres(Minteres parametros)
        {
            bool success;
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("insertaInteres", sql))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", parametros.nombre);
                        cmd.Parameters.AddWithValue("@interes", (parametros.interes/100));
                        cmd.Parameters.AddWithValue("@interesMinimo", (parametros.interesMinimo/100));
                        cmd.Parameters.AddWithValue("@descripcion", parametros.descripcion);

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

        public async Task<List<Minteres>> datosInteres(Minteres parametros)
        {
            var lista = new List<Minteres>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("datosInteres", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idInteres", SqlDbType.Int)).Value = parametros.idInteres;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var minteres = new Minteres();
                            minteres.idInteres = (int)item["idInteres"];
                            minteres.nombre = (string)item["nombre"];
                            minteres.interes = (double)item["interes"] * 100;
                            minteres.interesMinimo = (double)item["interesMinimo"] * 100;
                            minteres.descripcion = (string)item["descripcion"];
                            minteres.fechaIngreso = (DateTime)item["fechaIngreso"];
                            if (item["fechaUp"] == DBNull.Value)
                            {
                                minteres.fechaUp = DateTime.MinValue;
                            }
                            else
                            {
                                minteres.fechaUp = (DateTime)item["fechaUp"];
                            }

                            lista.Add(minteres);
                        }
                    }
                }
            }

            return lista;
        }

        public async Task<bool> editarInteres(Minteres parametros)
        {
            bool success;
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("editaInteres", sql))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idInteres", parametros.idInteres);
                        cmd.Parameters.AddWithValue("@nombre", parametros.nombre);
                        cmd.Parameters.AddWithValue("@interes", (parametros.interes/100));
                        cmd.Parameters.AddWithValue("@interesMinimo", (parametros.interesMinimo/100));
                        cmd.Parameters.AddWithValue("@descripcion", parametros.descripcion);

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

        public async Task<bool> inhabilitaInteres(Minteres parametros)
        {
            bool success;
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("inhabilitarInteres", sql))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idInteres", parametros.idInteres);

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
