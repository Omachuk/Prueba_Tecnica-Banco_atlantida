using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace frontend.Controllers
{
    public class ClienteController : Controller
    {
        Uri urlBase = new Uri("https://localhost:7095/api");
        private readonly HttpClient _client;

        public ClienteController() {
            _client = new HttpClient();
            _client.BaseAddress = urlBase;  
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Mclientes> clientesList = new List<Mclientes>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/cliente/mostrarClientes").Result;

            if(response.IsSuccessStatusCode) {
                string data = response.Content.ReadAsStringAsync().Result;
                clientesList = JsonConvert.DeserializeObject<List<Mclientes>>(data);
            }

            List<MtipoTarjeta> TipoList = new List<MtipoTarjeta>();
            HttpResponseMessage res = _client.GetAsync(_client.BaseAddress + "/TipoTarjeta/mostrarTipoTarjeta").Result;

            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                TipoList = JsonConvert.DeserializeObject<List<MtipoTarjeta>>(data);
            }

            List<Mtarjeta> tarjetasList = new List<Mtarjeta>();
            HttpResponseMessage resp = _client.GetAsync(_client.BaseAddress + "/Tarjeta/mostrarTarjetas").Result;

            if (resp.IsSuccessStatusCode)
            {
                string datos = resp.Content.ReadAsStringAsync().Result;
                tarjetasList = JsonConvert.DeserializeObject<List<Mtarjeta>>(datos);
            }

            ViewData["TipoList"] = TipoList;
            ViewData["clientesList"] = clientesList;
            ViewData["tarjetasList"] = tarjetasList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> clientes()
        {
            List<Mclientes> clientesList = new List<Mclientes>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/cliente/mostrarClientes").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                clientesList = JsonConvert.DeserializeObject<List<Mclientes>>(data);

            }
            return Json(clientesList);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCliente(Mclientes cliente)
        {
            string respuesta = "";
            bool bandera = true;
            string validacion = "";
            try
            {
                if (string.IsNullOrEmpty(cliente.nombre))
                {
                    validacion += "*Debe de ingresar el nombre del cliente<br>";
                    bandera = false;
                }
                if (string.IsNullOrEmpty(cliente.apellido))
                {
                    validacion += "*Debe de ingresar el apellido del cliente<br>";
                    bandera = false;
                }
                if (cliente.fechaNacimiento == DateTime.MinValue)
                {
                    validacion += "*Debe de ingresar la fecha de nacimiento del cliente<br>";
                    bandera = false;
                }
                if (string.IsNullOrEmpty(cliente.dui))
                {
                    validacion += "*Debe de ingresar el dui del cliente<br>";
                    bandera = false;
                }
                else
                {
                    string[] duiParts = cliente.dui.Split('-');
                    if (duiParts[0].Length != 8)
                    {
                        validacion += "*Debe de ingresar los 8 numeros del primer tramo del dui<br>";
                        bandera = false;
                    }
                    if (duiParts[1].Length != 1)
                    {
                        validacion += "*Debe de ingresar el ultimo numero del dui<br>";
                        bandera = false;
                    }
                }
                if (string.IsNullOrEmpty(cliente.email))
                {
                    validacion += "*Debe de ingresar el email del cliente<br>";
                    bandera = false;
                }
                else
                {
                    try
                    {
                        var mailAddress = new MailAddress(cliente.email);
                    }
                    catch (FormatException)
                    {
                        validacion += "*Debe de ingresar el email valido<br>";
                        bandera = false;
                    }
                }
                if (string.IsNullOrEmpty(cliente.direccion))
                {
                    validacion += "*Debe de ingresar la direccion del cliente<br>";
                    bandera = false;
                }

                if (bandera)
                {
                    string data = JsonConvert.SerializeObject(cliente);
                    StringContent contenido = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/cliente/insertarCliente", contenido).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        respuesta = "El cliente se ingreso con exito!!!";
                    }
                    else
                    {
                        respuesta = "No se pudo ingresar al cliente :(";
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
        public async Task<IActionResult> datosCliente(Mclientes cliente)
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/cliente/datosCliente/" + cliente.idCliente).Result;

            List<Mclientes> clientesList = new List<Mclientes>();

            if (response.IsSuccessStatusCode)
            {
                string datos = await response.Content.ReadAsStringAsync();
                clientesList = JsonConvert.DeserializeObject<List<Mclientes>>(datos);

            }
            return Json(clientesList);
        }

        [HttpPost]
        public async Task<IActionResult> editarCliente(Mclientes cliente)
        {
            string respuesta = "";
            bool bandera = true;
            string validacion = "";
            try
            {
                if (string.IsNullOrEmpty(cliente.nombre))
                {
                    validacion += "*Debe de ingresar el nombre del cliente<br>";
                    bandera = false;
                }
                if (string.IsNullOrEmpty(cliente.apellido))
                {
                    validacion += "*Debe de ingresar el apellido del cliente<br>";
                    bandera = false;
                }
                if (cliente.fechaNacimiento == DateTime.MinValue)
                {
                    validacion += "*Debe de ingresar la fecha de nacimiento del cliente<br>";
                    bandera = false;
                }
                if (string.IsNullOrEmpty(cliente.dui))
                {
                    validacion += "*Debe de ingresar el dui del cliente<br>";
                    bandera = false;
                }
                else
                {
                    string[] duiParts = cliente.dui.Split('-');
                    if (duiParts[0].Length != 8)
                    {
                        validacion += "*Debe de ingresar los 8 numeros del primer tramo del dui<br>";
                        bandera = false;
                    }
                    if (duiParts[1].Length != 1)
                    {
                        validacion += "*Debe de ingresar el ultimo numero del dui<br>";
                        bandera = false;
                    }
                }
                if (string.IsNullOrEmpty(cliente.email))
                {
                    validacion += "*Debe de ingresar el email del cliente<br>";
                    bandera = false;
                }
                else
                {
                    try
                    {
                        var mailAddress = new MailAddress(cliente.email);
                    }
                    catch (FormatException)
                    {
                        validacion += "*Debe de ingresar el email valido<br>";
                        bandera = false;
                    }
                }
                if (string.IsNullOrEmpty(cliente.direccion))
                {
                    validacion += "*Debe de ingresar la direccion del cliente<br>";
                    bandera = false;
                }

                if (bandera)
                {
                    string data = JsonConvert.SerializeObject(cliente);
                    StringContent contenido = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/cliente/editarCliente/" + cliente.idCliente, contenido).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        respuesta = "El cliente se edito con exito!!!";
                    }
                    else
                    {
                        respuesta = "No se pudo editar al cliente :(";
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
        public async Task<IActionResult> estadoAsync(int id)
        {
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/cliente/datosCliente/" + id).Result;

            List<Mclientes> clientesList = new List<Mclientes>();

            if (response.IsSuccessStatusCode)
            {
                string datos = response.Content.ReadAsStringAsync().Result;
                clientesList = JsonConvert.DeserializeObject<List<Mclientes>>(datos);

            }

            List<Mtarjeta> cuentaList = new List<Mtarjeta>();
            HttpResponseMessage res = _client.GetAsync(_client.BaseAddress + "/Tarjeta/datosCuenta/" + id).Result;

            if (res.IsSuccessStatusCode)
            {
                string data = res.Content.ReadAsStringAsync().Result;
                cuentaList = JsonConvert.DeserializeObject<List<Mtarjeta>>(data);
            }

            ViewData["clientesList"] = clientesList;
            ViewData["cuentaList"] = cuentaList;

            return View("EstadoCuenta");
        }
    }
}
