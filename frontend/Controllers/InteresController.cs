using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace frontend.Controllers
{
    public class InteresController : Controller
    {
        Uri urlBase = new Uri("https://localhost:7095/api");
        private readonly HttpClient _client;

        public InteresController()
        {
            _client = new HttpClient();
            _client.BaseAddress = urlBase;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Minteres> InteresList = new List<Minteres>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Interes/mostrarInteres/2").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                InteresList = JsonConvert.DeserializeObject<List<Minteres>>(data);
            }
            return View(InteresList);
        }
        [HttpPost]
        public async Task<IActionResult> intereses()
        {
            List<Minteres> clientesList = new List<Minteres>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Interes/mostrarInteres/2").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                clientesList = JsonConvert.DeserializeObject<List<Minteres>>(data);

            }
            return Json(clientesList);
        }

        [HttpPost]
        public async Task<IActionResult> insertInteres(Minteres intereses)
        {
            string respuesta = "";
            bool bandera = true;
            string validacion = "";
            try
            {
                if (string.IsNullOrEmpty(intereses.nombre))
                {
                    validacion += "*Debe de ingresar el nombre del interes<br>";
                    bandera = false;
                }
                if (intereses.interes == null)
                {
                    validacion += "*Debe de ingresar el interes <br>";
                    bandera = false;
                }else if (!(intereses.interes >= 0 && intereses.interes <= 100))
                {
                    validacion += "*Solo se acepta interes de 0% al 100% <br>";
                    bandera = false;
                }

                if (intereses.interesMinimo == null)
                {
                    validacion += "*Debe de ingresar el interes minimo <br>";
                    bandera = false;
                }
                else if (!(intereses.interes >= 0 && intereses.interes <= 100))
                {
                    validacion += "*Solo se aceptan intereses minimos de 0% al 100% <br>";
                    bandera = false;
                }
                if (string.IsNullOrEmpty(intereses.descripcion))
                {
                    validacion += "*Debe de ingresar una descripcion del interes<br>";
                    bandera = false;
                }

                if (bandera)
                {
                    string data = JsonConvert.SerializeObject(intereses);
                    StringContent contenido = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Interes/insertarInteres", contenido).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        respuesta = "El interes se ingreso con exito!!!";
                    }
                    else
                    {
                        respuesta = "No se pudo ingresar el interes :(";
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
        public async Task<IActionResult> datosInteres(Minteres intereses)
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Interes/datosInteres/"+ intereses.idInteres).Result;
            
            List<Minteres> clientesList = new List<Minteres>();

            if (response.IsSuccessStatusCode)
            {
                string datos = await response.Content.ReadAsStringAsync();
                clientesList = JsonConvert.DeserializeObject<List<Minteres>>(datos);

            }
            return Json(clientesList);
        }

        [HttpPost]
        public async Task<IActionResult> editInteres(Minteres intereses)
        {
            string respuesta = "";
            bool bandera = true;
            string validacion = "";
            try
            {
                if (string.IsNullOrEmpty(intereses.nombre))
                {
                    validacion += "*Debe de ingresar el nombre del interes<br>";
                    bandera = false;
                }
                if (intereses.interes == null)
                {
                    validacion += "*Debe de ingresar el interes <br>";
                    bandera = false;
                }
                else if (!(intereses.interes >= 0 && intereses.interes <= 100))
                {
                    validacion += "*Solo se acepta interes de 0% al 100% <br>";
                    bandera = false;
                }

                if (intereses.interesMinimo == null)
                {
                    validacion += "*Debe de ingresar el interes minimo <br>";
                    bandera = false;
                }
                else if (!(intereses.interes >= 0 && intereses.interes <= 100))
                {
                    validacion += "*Solo se aceptan intereses minimos de 0% al 100% <br>";
                    bandera = false;
                }
                if (string.IsNullOrEmpty(intereses.descripcion))
                {
                    validacion += "*Debe de ingresar una descripcion del interes<br>";
                    bandera = false;
                }

                if (bandera)
                {
                    string data = JsonConvert.SerializeObject(intereses);
                    StringContent contenido = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/Interes/editarInteres/"+ intereses.idInteres, contenido).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        respuesta = "El interes se edito con exito!!!";
                    }
                    else
                    {
                        respuesta = "No se pudo editar el interes :(";
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
        public async Task<IActionResult> inhabilitarInteres(Minteres intereses)
        {
            string respuesta = "";
            try
            {
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Interes/inhabilitarInteres/" + intereses.idInteres).Result;

                if (response.IsSuccessStatusCode)
                {
                    respuesta = "El interes se inhabilito con exito!!!";
                }
                else
                {
                    respuesta = "No se pudo inhabilitar el interes :(";
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
