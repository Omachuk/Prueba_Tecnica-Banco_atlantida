using backend.Conexion;
using System.Data.SqlClient;
using System.Data;
using backend.Modelo;
using System.Collections.Generic;

namespace backend.Datos
{
    public class DtipoTarjeta
    {
        Conexionbd cn = new Conexionbd();
        public async Task<List<MtipoTarjeta>> mostrarTipoTarjeta()
        {
            var listaTipo = new List<MtipoTarjeta>();

            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("mostrarTipoTarjeta", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var mtipoTarjeta = new MtipoTarjeta();
                            mtipoTarjeta.idTipo = (int)item["idTipo"];
                            mtipoTarjeta.idInteres = (int)item["idInteres"];
                            mtipoTarjeta.tipo = (string)item["tipo"];
                            mtipoTarjeta.montoMaximo = (double)item["montoMaximo"];
                            mtipoTarjeta.descripcion = (string)item["descripcion"];
                            mtipoTarjeta.fechaIngreso = (DateTime)item["fechaIngreso"];
                            mtipoTarjeta.estado = (int)item["estado"];
                            mtipoTarjeta.nombre = (string)item["nombre"];
                            mtipoTarjeta.interes = (double)item["interes"]*100;
                            mtipoTarjeta.interesMinimo = (double)item["interesMinimo"]*100;

                            listaTipo.Add(mtipoTarjeta);
                        }
                    }
                }
            }

            return listaTipo;
        }

        public async Task<bool> insertarTipoTarjeta(MtipoTarjeta parametros)
        {
            bool success;
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("insertaTipoTarjeta", sql))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idInteres", parametros.idInteres);
                        cmd.Parameters.AddWithValue("@tipo", parametros.tipo);
                        cmd.Parameters.AddWithValue("@montoMaximo", parametros.montoMaximo);
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

        public async Task<List<MtipoTarjeta>> datosTipoTarjeta(MtipoTarjeta parametros)
        {
            var listaTipo = new List<MtipoTarjeta>();

            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("datosTipoTarjeta", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idTipo", SqlDbType.Int)).Value = parametros.idTipo;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var mtipoTarjeta = new MtipoTarjeta();
                            mtipoTarjeta.idTipo = (int)item["idTipo"];
                            mtipoTarjeta.idInteres = (int)item["idInteres"];
                            mtipoTarjeta.tipo = (string)item["tipo"];
                            mtipoTarjeta.montoMaximo = (double)item["montoMaximo"];
                            mtipoTarjeta.descripcion = (string)item["descripcion"];
                            mtipoTarjeta.fechaIngreso = (DateTime)item["fechaIngreso"];
                            mtipoTarjeta.estado = (int)item["estado"];
                            mtipoTarjeta.nombre = (string)item["nombre"];
                            mtipoTarjeta.interes = (double)item["interes"]*100;
                            mtipoTarjeta.interesMinimo = (double)item["interesMinimo"] * 100;

                            listaTipo.Add(mtipoTarjeta);
                        }
                    }
                }
            }

            return listaTipo;
        }

        public async Task<bool> editarTipoTarjeta(MtipoTarjeta parametros)
        {
            bool success;
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("editarTipoTarjeta", sql))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idTipo", parametros.idTipo);
                        cmd.Parameters.AddWithValue("@idInteres", parametros.idInteres);
                        cmd.Parameters.AddWithValue("@tipo", parametros.tipo);
                        cmd.Parameters.AddWithValue("@montoMaximo", parametros.montoMaximo);
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

        public async Task<bool> inhabilitarTipoTarjeta(MtipoTarjeta parametros)
        {
            bool success;
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("inhabilitarTipoTarjeta", sql))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idTipo", parametros.idTipo);

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
