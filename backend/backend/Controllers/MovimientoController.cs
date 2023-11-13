using backend.Datos;
using backend.Modelo;
using Microsoft.AspNetCore.Mvc;
using System;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class MovimientoController
    {
        [HttpPost]
        public async Task<string> insertarMovimiento([FromBody] Mmovimientos parametros)
        {
            string respuesta = "";
            bool bandera = true;
            List<Mtarjeta> tarjetas = new List<Mtarjeta>();
            var fun = new Dtarjeta();
            tarjetas = await fun.datosTarjetas(parametros.idTarjeta);

            if(parametros.tipoMovimiento == 1)
            {
                double saldoActual = (double)((parametros.monto * tarjetas[0].interes) + parametros.monto + tarjetas[0].saldoActual);
                if(saldoActual > tarjetas[0].montoMaximo)
                {
                    respuesta = "No puede sobrepasar su monto maximo";
                    bandera = false;
                }else if (saldoActual > tarjetas[0].saldoDisponible)
                {
                    respuesta = "No posee suficiente saldo disponible";
                    bandera = false;
                }
                else
                {
                    double saldoDisponible = (double)tarjetas[0].saldoDisponible - saldoActual;

                    await fun.upSaldoTarjeta(parametros.idTarjeta, saldoActual, saldoDisponible);
                }
            }
            else if(parametros.tipoMovimiento == 2)
            {
                if (tarjetas[0].saldoActual == 0)
                {
                    respuesta = "Esta tarjeta no posee deuda";
                    bandera = false;
                }
                else if ((tarjetas[0].saldoActual - parametros.monto) < 0)
                {
                    respuesta = "No se puede pagar mas de la deuda";
                    bandera = false;
                }
                else
                {
                    double saldoActual = (double)tarjetas[0].saldoActual - parametros.monto;
                    double saldoDisponible = (double)tarjetas[0].saldoDisponible + parametros.monto;

                    if(saldoDisponible > tarjetas[0].montoMaximo)
                    {
                        saldoDisponible = (double)tarjetas[0].montoMaximo;
                    }

                    await fun.upSaldoTarjeta(parametros.idTarjeta, saldoActual, saldoDisponible);
                }
            }
            
            if(bandera)
            {
                var funcion = new Dmovimientos();
                await funcion.insertarMovimiento(parametros);
                respuesta = "succes";
            }
                
            return respuesta;
        }

        [HttpPost]
        public async Task<ActionResult<List<Mmovimientos>>> historialMovimiento([FromBody] Mmovimientos parametros)
        {
            // Parsear el string a un objeto DateTime para el primer día del mes
            DateTime primerDia = DateTime.ParseExact(parametros.mes + "-01", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            // Obtener el último día sumando un mes y restando un día al primer día del siguiente mes
            DateTime ultimoDia = primerDia.AddMonths(1).AddDays(-1);

            var funcion = new Dmovimientos();
            var lista = await funcion.historialMovimiento(parametros.idTarjeta, primerDia, ultimoDia);
            return lista;
        }

        [HttpPost]
        public async Task<ActionResult<List<Mmovimientos>>> historialCompras([FromBody] Mmovimientos parametros)
        {
            // Parsear el string a un objeto DateTime para el primer día del mes
            DateTime primerDia = DateTime.ParseExact(parametros.mes + "-01", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            // Obtener el último día sumando un mes y restando un día al primer día del siguiente mes
            DateTime ultimoDia = primerDia.AddMonths(1).AddDays(-1);

            var funcion = new Dmovimientos();
            var lista = await funcion.historialCompras(parametros.idTarjeta, primerDia, ultimoDia);
            return lista;
        }

        [HttpPost]
        public async Task<double> totalCompras([FromBody] Mmovimientos parametros)
        {
            // Convertir el string a un objeto DateTime para el primer día del mes actual
            DateTime fecha = DateTime.ParseExact(parametros.mes + "-01", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

            // Obtener el primer día del mes pasado
            DateTime primerDiaMesPasado = fecha.AddMonths(-1);
            primerDiaMesPasado = new DateTime(primerDiaMesPasado.Year, primerDiaMesPasado.Month, 1);

            // Obtener el último día sumando un mes y restando un día al primer día del siguiente mes
            DateTime ultimoDia = fecha.AddMonths(1).AddDays(-1);

            var funcion = new Dmovimientos();
            return await funcion.totalCompras(parametros.idTarjeta, primerDiaMesPasado, ultimoDia);
        }
    }
}
