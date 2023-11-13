using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace frontend.Controllers
{
    public class TipoInteresController : Controller
    {
        Uri urlBase = new Uri("https://localhost:7095/api");
        private readonly HttpClient _client;
        public TipoInteresController()
        {
            _client = new HttpClient();
            _client.BaseAddress = urlBase;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<MtipoTarjeta> TipoList = new List<MtipoTarjeta>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/TipoTarjeta/mostrarTipoTarjeta").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                TipoList = JsonConvert.DeserializeObject<List<MtipoTarjeta>>(data);
            }

            List<Minteres> InteresList = new List<Minteres>();
            HttpResponseMessage res = _client.GetAsync(_client.BaseAddress + "/Interes/mostrarInteres/1").Result;

            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                InteresList = JsonConvert.DeserializeObject<List<Minteres>>(data);
            }

            ViewData["TipoList"] = TipoList;
            ViewData["InteresList"] = InteresList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> tiposTarjetas()
        {
            List<MtipoTarjeta> clientesList = new List<MtipoTarjeta>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/TipoTarjeta/mostrarTipoTarjeta").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                clientesList = JsonConvert.DeserializeObject<List<MtipoTarjeta>>(data);

            }
            return Json(clientesList);
        }

        [HttpPost]
        public async Task<IActionResult> insertTipoTarjeta(MtipoTarjeta tipoTarjeta)
        {
            string respuesta = "";
            bool bandera = true;
            string validacion = "";
            try
            {
                if (tipoTarjeta.idInteres == null)
                {
                    validacion += "*Debe de ingresar el tipo de interes<br>";
                    bandera = false;
                }
                if (string.IsNullOrEmpty(tipoTarjeta.tipo))
                {
                    validacion += "*Debe de ingresar el tipo de tarjeta<br>";
                    bandera = false;
                }
                if (tipoTarjeta.montoMaximo == null)
                {
                    validacion += "*Debe de ingresar el monto maximo<br>";
                    bandera = false;
                }
                else if (tipoTarjeta.montoMaximo <= 0)
                {
                    validacion += "*El monto maximo debe de ser mayor a cero<br>";
                    bandera = false;
                }

                if (string.IsNullOrEmpty(tipoTarjeta.descripcion))
                {
                    validacion += "*Debe de ingresar una descripcion<br>";
                    bandera = false;
                }

                if (bandera)
                {
                    string data = JsonConvert.SerializeObject(tipoTarjeta);
                    StringContent contenido = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/TipoTarjeta/insertarTipoTarjeta", contenido).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        respuesta = "El tipo de tarjeta se ingreso con exito!!!";
                    }
                    else
                    {
                        respuesta = "No se pudo ingresar el tipo de tarjeta :(";
                    }
                }

            }
            catch (Exception)
            {

                respuesta = "Ocurrio un error :(";
            }
            string[] arreglo = new string[] { respuesta, validacion };
            return Json(arreglo);
        }

        [HttpPost]
        public async Task<IActionResult> datosTipoTarjeta(MtipoTarjeta tipoTarjeta)
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/TipoTarjeta/datosTipoTarjeta/" + tipoTarjeta.idTipo).Result;
            List<MtipoTarjeta> tipoTarjetaList = new List<MtipoTarjeta>();

            if (response.IsSuccessStatusCode)
            {
                string datos = await response.Content.ReadAsStringAsync();
                tipoTarjetaList = JsonConvert.DeserializeObject<List<MtipoTarjeta>>(datos);

            }
            return Json(tipoTarjetaList);
        }

        [HttpPost]
        public async Task<IActionResult> editTipoTarjeta(MtipoTarjeta tipoTarjeta)
        {
            string respuesta = "";
            bool bandera = true;
            string validacion = "";
            try
            {
                if (tipoTarjeta.idInteres == null)
                {
                    validacion += "*Debe de ingresar el tipo de interes<br>";
                    bandera = false;
                }
                if (string.IsNullOrEmpty(tipoTarjeta.tipo))
                {
                    validacion += "*Debe de ingresar el tipo de tarjeta<br>";
                    bandera = false;
                }
                if (tipoTarjeta.montoMaximo == null)
                {
                    validacion += "*Debe de ingresar el monto maximo<br>";
                    bandera = false;
                }
                else if (tipoTarjeta.montoMaximo <= 0)
                {
                    validacion += "*El monto maximo debe de ser mayor a cero<br>";
                    bandera = false;
                }

                if (string.IsNullOrEmpty(tipoTarjeta.descripcion))
                {
                    validacion += "*Debe de ingresar una descripcion<br>";
                    bandera = false;
                }

                if (bandera)
                {
                    string data = JsonConvert.SerializeObject(tipoTarjeta);
                    StringContent contenido = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/TipoTarjeta/editarTipoTarjeta/" + tipoTarjeta.idTipo, contenido).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        respuesta = "El tipo de tarjeta se edito con exito!!!";
                    }
                    else
                    {
                        respuesta = "No se pudo editar el tipo de tarjeta :(";
                    }
                }

            }
            catch (Exception)
            {

                respuesta = "Ocurrio un error :(";
            }
            string[] arreglo = new string[] { respuesta, validacion };
            return Json(arreglo);
        }

        [HttpPost]
        public async Task<IActionResult> inhabilitarTipoTarjeta(MtipoTarjeta tipoTarjeta)
        {
            string respuesta = "";
            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/TipoTarjeta/inhabilitarTipoTarjeta/" + tipoTarjeta.idTipo).Result;

                if (response.IsSuccessStatusCode)
                {
                    respuesta = "El tipo de tarjeta se inhabilito con exito!!!";
                }
                else
                {
                    respuesta = "No se pudo inhabilitar el tipo de tarjeta :(";
                }

            }
            catch (Exception)
            {

                respuesta = "Ocurrio un error :(";
            }
            return Json(respuesta);
        }
    }
}
