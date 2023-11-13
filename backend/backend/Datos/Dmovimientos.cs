using backend.Conexion;
using backend.Modelo;
using System.Data.SqlClient;
using System.Data;

namespace backend.Datos
{
    public class Dmovimientos
    {
        Conexionbd cn = new Conexionbd();
        public async Task<bool> insertarMovimiento(Mmovimientos parametros)
        {
            bool success;
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("insertaMovimiento", sql))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idTarjeta", parametros.idTarjeta);
                        cmd.Parameters.AddWithValue("@monto", parametros.monto);
                        cmd.Parameters.AddWithValue("@fechaCompra", parametros.fechaCompra);
                        cmd.Parameters.AddWithValue("@descripcion", parametros.descripcion);
                        cmd.Parameters.AddWithValue("@tipoMovimiento", parametros.tipoMovimiento);

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

        public async Task<List<Mmovimientos>> historialMovimiento(int id, DateTime inicioMes, DateTime finMes)
        {
            var lista = new List<Mmovimientos>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("historialMovimiento", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idTarjeta", SqlDbType.Int)).Value = id;
                    cmd.Parameters.Add(new SqlParameter("@inicioMes", SqlDbType.Date)).Value = inicioMes;
                    cmd.Parameters.Add(new SqlParameter("@finMes", SqlDbType.Date)).Value = finMes;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var mmovimientos = new Mmovimientos();
                            mmovimientos.numeroTarjeta = (string)item["numeroTarjeta"];
                            mmovimientos.monto = (double)item["monto"];
                            mmovimientos.fechaCompra = (DateTime)item["fechaCompra"];
                            mmovimientos.descripcion = (string)item["descripcion"];
                            mmovimientos.tipoMovimiento = (int)item["tipoMovimiento"];

                            lista.Add(mmovimientos);
                        }
                    }
                }
            }

            return lista;
        }

        public async Task<List<Mmovimientos>> historialCompras(int id, DateTime inicioMes, DateTime finMes)
        {
            var lista = new List<Mmovimientos>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("historialCompras", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idTarjeta", SqlDbType.Int)).Value = id;
                    cmd.Parameters.Add(new SqlParameter("@inicioMes", SqlDbType.Date)).Value = inicioMes;
                    cmd.Parameters.Add(new SqlParameter("@finMes", SqlDbType.Date)).Value = finMes;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var mmovimientos = new Mmovimientos();
                            mmovimientos.numeroTarjeta = (string)item["numeroTarjeta"];
                            mmovimientos.monto = (double)item["monto"];
                            mmovimientos.fechaCompra = (DateTime)item["fechaCompra"];
                            mmovimientos.descripcion = (string)item["descripcion"];
                            mmovimientos.tipoMovimiento = (int)item["tipoMovimiento"];

                            lista.Add(mmovimientos);
                        }
                    }
                }
            }

            return lista;
        }

        public async Task<double> totalCompras(int id, DateTime inicioMes, DateTime finMes)
        {
            double total = 0;
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("totalCompras", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@idTarjeta", SqlDbType.Int)).Value = id;
                    cmd.Parameters.Add(new SqlParameter("@inicioMes", SqlDbType.Date)).Value = inicioMes;
                    cmd.Parameters.Add(new SqlParameter("@finMes", SqlDbType.Date)).Value = finMes;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        await item.ReadAsync();
                        total = (double)item["total"];

                    }
                }
            }

            return total;
        }
    }
}
