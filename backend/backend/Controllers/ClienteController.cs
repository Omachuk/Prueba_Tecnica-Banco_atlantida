using Microsoft.AspNetCore.Mvc;
using backend.Modelo;
using backend.Datos;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ClienteController
    {
        [HttpGet]
        public async Task <ActionResult<List<Mcliente>>> mostrarClientes()
        {
            var funcion = new Dcliente();
            var lista = await funcion.mostrarCliente();
            return lista;
        }

        [HttpPost]
        public async Task<bool> insertarCliente([FromBody] Mcliente parametros)
        {
            var funcion = new Dcliente();
            return await funcion.insertarCliente(parametros);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Mcliente>>> datosCliente(int id)
        {
            var funcion = new Dcliente();
            var parametros = new Mcliente();
            parametros.idCliente = id;
            return await funcion.datosCliente(parametros);
        }

        [HttpPut("{id}")]
        public async Task<bool> editarCliente(int id, [FromBody] Mcliente parametros)
        {
            var funcion = new Dcliente();
            parametros.idCliente = id;
            return await funcion.editarCliente(parametros);
        }

        [HttpDelete("{id}")]
        public async Task<bool> eliminarCliente(int id)
        {
            var funcion = new Dcliente();
            var parametros = new Mcliente();
            parametros.idCliente = id;
            return await funcion.eliminarCliente(parametros);
        }

    }
}
