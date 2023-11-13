using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace frontend.Controllers
{
    public class TarjetaController : Controller
    {
        Uri urlBase = new Uri("https://localhost:7095/api");
        private readonly HttpClient _client;
        public TarjetaController()
        {
            _client = new HttpClient();
            _client.BaseAddress = urlBase;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> insertTarjeta(Mtarjeta tarjeta)
        {
            string respuesta = "";
            bool bandera = true;
            string validacion = "";
            List<Mtarjeta> clientesList = new List<Mtarjeta>();

            try
            {
                if(tarjeta.idTipo == 0)
                {
                    validacion += "*Debe de ingresar el tipo de tarjeta<br>";
                    bandera = false;
                }
                if (bandera)
                {
                    string data = JsonConvert.SerializeObject(tarjeta);
                    StringContent contenido = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Tarjeta/insertTarjeta", contenido).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        respuesta = "La tarjeta se ingreso con exito!!!";
                        string datos = response.Content.ReadAsStringAsync().Result;
                        clientesList = JsonConvert.DeserializeObject<List<Mtarjeta>>(datos);
                    }
                    else
                    {
                        respuesta = "No se pudo ingresar la tarjeta :(";
                    }
                }

            }
            catch (Exception)
            {

                respuesta = "Ocurrio un error :(";
            }
            string[] arreglo = new string[] { respuesta, validacion, JsonConvert.SerializeObject(clientesList) };
            return Json(arreglo);
        }
    }
}
