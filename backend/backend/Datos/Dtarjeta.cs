using backend.Conexion;
using backend.Modelo;
using System.Data.SqlClient;
using System.Data;

namespace backend.Datos
{
    public class Dtarjeta
    {
        Conexionbd cn = new Conexionbd();

        public async Task<bool> verificaNumTarjeta(string tarjeta)
        {
            bool exists;
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("verificaNumTarjeta", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@numeroTarjeta", SqlDbType.VarChar, 19)).Value = tarjeta;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        await item.ReadAsync();
                        int cantidad = (int)item["cantidad"];

                        if(cantidad > 0)
                        {
                            exists = false;
                        }
                        else
                        {
                            exists = true;
                        }
                    }
                }
            }

            return exists;
        }

        public async Task<bool> insertarTarjeta(Mtarjeta parametros)
        {
            bool success;
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("insertaTarjeta", sql))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idCliente", parametros.idCliente);
                        cmd.Parameters.AddWithValue("@idTipo", parametros.idTipo);
                        cmd.Parameters.AddWithValue("@numeroTarjeta", parametros.numeroTarjeta);
                        cmd.Parameters.AddWithValue("@fechaExpira", parametros.fechaExpira);
                        cmd.Parameters.AddWithValue("@saldoDisponible", parametros.saldoDisponible);

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

        public async Task<List<Mtarjeta>> detalleTarjeta(int idCliente,string numeroTarjeta)
        {
            var lista = new List<Mtarjeta>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("detalleTarjeta", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idCliente", SqlDbType.Int)).Value = idCliente;
                    cmd.Parameters.Add(new SqlParameter("@numeroTarjeta", SqlDbType.VarChar, 19)).Value = numeroTarjeta;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var mtarjeta = new Mtarjeta();
                            mtarjeta.idCliente = (int)item["idCliente"];
                            mtarjeta.nombre = (string)item["nombre"];
                            mtarjeta.apellido = (string)item["apellido"];
                            mtarjeta.sexo = (int)item["sexo"];
                            mtarjeta.fechaNacimiento = (DateTime)item["fechaNacimiento"];
                            mtarjeta.dui = (string)item["dui"];
                            mtarjeta.direccion = (string)item["direccion"];
                            mtarjeta.telefono = (string)item["telefono"];
                            mtarjeta.email = (string)item["email"];
                            mtarjeta.estado = (int)item["estado"];
                            mtarjeta.numeroTarjeta = (string)item["numeroTarjeta"];
                            mtarjeta.fechaExpira = (string)item["fechaExpira"];
                            mtarjeta.tipo = (string)item["tipo"];
                            mtarjeta.interes = (double)item["interes"] * 100;
                            mtarjeta.interesMinimo = (double)item["interesMinimo"] * 100;
                            mtarjeta.montoMaximo = (double)item["montoMaximo"];

                            lista.Add(mtarjeta);
                        }
                    }
                }
            }

            return lista;
        }

        public async Task<List<Mtarjeta>> mostrarTarjetas()
        {
            var lista = new List<Mtarjeta>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("mostrarTarjetas", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var mtarjeta = new Mtarjeta();
                            mtarjeta.idTarjeta = (int)item["idTarjeta"];
                            mtarjeta.idCliente = (int)item["idCliente"];
                            mtarjeta.numeroTarjeta = (string)item["numeroTarjeta"];
                            mtarjeta.fechaExpira = (string)item["fechaExpira"];
                            mtarjeta.saldoActual = (double)item["saldoActual"];
                            mtarjeta.saldoDisponible = (double)item["saldoDisponible"];
                            mtarjeta.tipo = (string)item["tipo"];

                            lista.Add(mtarjeta);
                        }
                    }
                }
            }

            return lista;
        }

        public async Task<List<Mtarjeta>> datosTarjetas(int id)
        {
            var lista = new List<Mtarjeta>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("datosTarjeta", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idTarjeta", SqlDbType.Int)).Value = id;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        await item.ReadAsync();
                        var mtarjeta = new Mtarjeta();
                        mtarjeta.saldoActual = (double)item["saldoActual"];
                        mtarjeta.saldoDisponible = (double)item["saldoDisponible"];
                        mtarjeta.interes = (double)item["interes"];
                        mtarjeta.montoMaximo = (double)item["montoMaximo"];

                        lista.Add(mtarjeta);
                    }
                }
            }

            return lista;
        }

        public async Task<bool> upSaldoTarjeta(int id, double saldoActual, double saldoDisponible)
        {
            bool success;
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("upSaldoTarjeta", sql))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idTarjeta", id);
                        cmd.Parameters.AddWithValue("@saldoActual", saldoActual);
                        cmd.Parameters.AddWithValue("@saldoDisponible", saldoDisponible);

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

        public async Task<List<Mtarjeta>> datosCuenta(int id)
        {
            var lista = new List<Mtarjeta>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("datosCuenta", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idCliente", SqlDbType.Int)).Value = id;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var mtarjeta = new Mtarjeta();
                            mtarjeta.idCliente = (int)item["idCliente"];
                            mtarjeta.nombre = (string)item["nombre"];
                            mtarjeta.apellido = (string)item["apellido"];
                            mtarjeta.idTarjeta = (int)item["idTarjeta"];
                            mtarjeta.numeroTarjeta = (string)item["numeroTarjeta"];
                            mtarjeta.fechaExpira = (string)item["fechaExpira"];
                            mtarjeta.saldoActual = (double)item["saldoActual"];
                            mtarjeta.saldoDisponible = (double)item["saldoDisponible"];
                            mtarjeta.montoMaximo = (double)item["montoMaximo"];
                            mtarjeta.interes = (double)item["interes"];
                            mtarjeta.interesMinimo = (double)item["interesMinimo"];
                            
                            mtarjeta.interesBonificable = (double)item["saldoActual"]*(double)item["interes"];
                            mtarjeta.cuotaMinima = (double)item["saldoActual"]*(double)item["interesMinimo"];
                            mtarjeta.saldoIntereses = (double)item["saldoActual"]+ mtarjeta.interesBonificable;

                            lista.Add(mtarjeta);
                        }
                    }
                }
            }

            return lista;
        }
    }
}
