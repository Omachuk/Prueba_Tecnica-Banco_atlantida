using backend.Datos;
using Microsoft.AspNetCore.Mvc;
using backend.Modelo;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TipoTarjetaController
    {
        [HttpGet]
        public async Task<ActionResult<List<MtipoTarjeta>>> mostrarTipoTarjeta()
        {
            var funcion = new DtipoTarjeta();
            var lista = await funcion.mostrarTipoTarjeta();
            return lista;
        }

        [HttpPost]
        public async Task<bool> insertarTipoTarjeta([FromBody] MtipoTarjeta parametros)
        {
            var funcion = new DtipoTarjeta();
            return await funcion.insertarTipoTarjeta(parametros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<MtipoTarjeta>>> datosTipoTarjeta(int id)
        {
            var funcion = new DtipoTarjeta();
            var parametros = new MtipoTarjeta();
            parametros.idTipo = id;
            return await funcion.datosTipoTarjeta(parametros);
        }

        [HttpPut("{id}")]
        public async Task<bool> editarTipoTarjeta(int id, [FromBody] MtipoTarjeta parametros)
        {
            var funcion = new DtipoTarjeta();
            parametros.idTipo = id;
            return await funcion.editarTipoTarjeta(parametros);
        }

        [HttpGet("{id}")]
        public async Task<bool> inhabilitarTipoTarjeta(int id)
        {
            var funcion = new DtipoTarjeta();
            var parametros = new MtipoTarjeta();
            parametros.idTipo = id;
            return await funcion.inhabilitarTipoTarjeta(parametros);

        }
    }
}
