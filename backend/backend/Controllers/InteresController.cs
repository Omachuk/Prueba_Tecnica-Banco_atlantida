using backend.Datos;
using backend.Modelo;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class InteresController
    {
        [HttpGet("{estado}")]
        public async Task<ActionResult<List<Minteres>>> mostrarInteres(int estado)
        {
            var funcion = new Dinteres();
            var parametros = new Minteres();
            parametros.estado = estado;
            var lista = await funcion.mostrarInteres(parametros);
            return lista;
        }
        [HttpPost]
        public async Task<bool> insertarInteres([FromBody] Minteres parametros)
        {
            var funcion = new Dinteres();
            return await funcion.insertarInteres(parametros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Minteres>>> datosInteres(int id)
        {
            var funcion = new Dinteres();
            var parametros = new Minteres();
            parametros.idInteres = id;
            return await funcion.datosInteres(parametros);
        }

        [HttpPut("{id}")]
        public async Task<bool> editarInteres(int id, [FromBody] Minteres parametros)
        {
            var funcion = new Dinteres();
            parametros.idInteres = id;
            return await funcion.editarInteres(parametros);
        }

        [HttpGet("{id}")]
        public async Task<bool> inhabilitarInteres(int id)
        {
            var funcion = new Dinteres();
            var parametros = new Minteres();
            parametros.idInteres = id;
            return await funcion.inhabilitaInteres(parametros);

        }
    }
}
