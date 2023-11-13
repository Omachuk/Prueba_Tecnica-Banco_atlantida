using backend.Datos;
using backend.Modelo;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TarjetaController
    {
        [HttpPost]
        public async Task<ActionResult<List<Mtarjeta>>> insertTarjeta([FromBody] Mtarjeta parametros)
        {
            var funcion = new Dtarjeta();
            // Crea una instancia de Random
            Random random = new Random();
            //se verifica si la tarjeta no existe
            do
            {
                parametros.numeroTarjeta = random.Next(1000, 9999) + "-" + random.Next(1000, 9999) + "-" + random.Next(1000, 9999) + "-" + random.Next(1000, 9999);
            } while (await funcion.verificaNumTarjeta(parametros.numeroTarjeta) == false);
            // Captura la fecha actual
            DateTime now = DateTime.Now;
            // Suma 10 años a la fecha actual y luego solo se toma el mes y año
            parametros.fechaExpira = now.AddYears(10).ToString().Substring(3, 7);
            parametros.saldoActual = 0;

            await funcion.insertarTarjeta(parametros);
            var fun = new Dtarjeta();
            var lista = await fun.detalleTarjeta(0, parametros.numeroTarjeta);
            return lista;
        }

        [HttpGet]
        public async Task<ActionResult<List<Mtarjeta>>> mostrarTarjetas()
        {
            var funcion = new Dtarjeta();
            var lista = await funcion.mostrarTarjetas();
            return lista;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Mtarjeta>>> datosCuenta(int id)
        {
            var funcion = new Dtarjeta();
            return await funcion.datosCuenta(id);
        }
    }
}
