using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace frontend.Controllers
{
    public class MovimientoController : Controller
    {
        Uri urlBase = new Uri("https://localhost:7095/api");
        private readonly HttpClient _client;

        public MovimientoController()
        {
            _client = new HttpClient();
            _client.BaseAddress = urlBase;
        }
        public IActionResult pagos()
        {
            List<Mclientes> clientesList = new List<Mclientes>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/cliente/mostrarClientes").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                clientesList = JsonConvert.DeserializeObject<List<Mclientes>>(data);
            }

            List<Mtarjeta> tarjetasList = new List<Mtarjeta>();
            HttpResponseMessage res = _client.GetAsync(_client.BaseAddress + "/Tarjeta/mostrarTarjetas").Result;

            if (res.IsSuccessStatusCode)
            {
                string datos = res.Content.ReadAsStringAsync().Result;
                tarjetasList = JsonConvert.DeserializeObject<List<Mtarjeta>>(datos);
            }

            ViewData["clientesList"] = clientesList;
            ViewData["tarjetasList"] = tarjetasList;

            return View("Pagos");
        }

        [HttpPost]
        public async Task<IActionResult> ingresarPago(Mmovimiento movimiento)
        {
            string respuesta = "";
            bool bandera = true;
            string validacion = "";

            try
            {
                if (movimiento.idTarjeta == 0)
                {
                    validacion += "*Debe de elejir una tarjeta<br>";
                    bandera = false;
                }
                if (string.IsNullOrWhiteSpace(movimiento.monto.ToString()))
                {
                    validacion += "*Debe de ingresar un pago<br>";
                    bandera = false;
                }else if(movimiento.monto <= 0)
                {
                    validacion += "*El pago debe de ser mayor a cero<br>";
                    bandera = false;
                }
                if (movimiento.fechaCompra == DateTime.MinValue)
                {
                    validacion += "*Debe de ingresar una fecha de pago<br>";
                    bandera = false;
                }
                if (bandera)
                {
                    movimiento.descripcion = "Pago";
                    movimiento.tipoMovimiento = 2;

                    string data = JsonConvert.SerializeObject(movimiento);
                    StringContent contenido = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Movimiento/insertarMovimiento", contenido).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string res = response.Content.ReadAsStringAsync().Result;
                        if(res == "succes")
                        {
                            respuesta = "El pago se ingreso con exito!!!";
                        }
                        else
                        {
                            validacion = res;
                        }
                        
                    }
                    else
                    {
                        respuesta = "No se pudo ingresar el pago :(";
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

        public IActionResult compras()
        {
            List<Mclientes> clientesList = new List<Mclientes>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/cliente/mostrarClientes").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                clientesList = JsonConvert.DeserializeObject<List<Mclientes>>(data);
            }

            List<Mtarjeta> tarjetasList = new List<Mtarjeta>();
            HttpResponseMessage res = _client.GetAsync(_client.BaseAddress + "/Tarjeta/mostrarTarjetas").Result;

            if (res.IsSuccessStatusCode)
            {
                string datos = res.Content.ReadAsStringAsync().Result;
                tarjetasList = JsonConvert.DeserializeObject<List<Mtarjeta>>(datos);
            }

            ViewData["clientesList"] = clientesList;
            ViewData["tarjetasList"] = tarjetasList;

            return View("Compras");
        }

        [HttpPost]
        public async Task<IActionResult> ingresarCompras(Mmovimiento movimiento)
        {
            string respuesta = "";
            bool bandera = true;
            string validacion = "";

            try
            {
                if (movimiento.idTarjeta == 0)
                {
                    validacion += "*Debe de elejir una tarjeta<br>";
                    bandera = false;
                }
                if (string.IsNullOrWhiteSpace(movimiento.monto.ToString()))
                {
                    validacion += "*Debe de ingresar un pago<br>";
                    bandera = false;
                }
                else if (movimiento.monto <= 0)
                {
                    validacion += "*El pago debe de ser mayor a cero<br>";
                    bandera = false;
                }
                if (movimiento.fechaCompra == DateTime.MinValue)
                {
                    validacion += "*Debe de ingresar una fecha de pago<br>";
                    bandera = false;
                }
                if (string.IsNullOrEmpty(movimiento.descripcion))
                {
                    validacion += "*Debe de ingresar la descripcion de la compra<br>";
                    bandera = false;
                }
                if (bandera)
                {
                    movimiento.tipoMovimiento = 1;

                    string data = JsonConvert.SerializeObject(movimiento);
                    StringContent contenido = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Movimiento/insertarMovimiento", contenido).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string res = response.Content.ReadAsStringAsync().Result;
                        if (res == "succes")
                        {
                            respuesta = "La compra se ingreso con exito!!!";
                        }
                        else
                        {
                            validacion = res;
                        }

                    }
                    else
                    {
                        respuesta = "No se pudo ingresar la compra :(";
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

        public IActionResult historial()
        {
            List<Mclientes> clientesList = new List<Mclientes>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/cliente/mostrarClientes").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                clientesList = JsonConvert.DeserializeObject<List<Mclientes>>(data);
            }

            List<Mtarjeta> tarjetasList = new List<Mtarjeta>();
            HttpResponseMessage res = _client.GetAsync(_client.BaseAddress + "/Tarjeta/mostrarTarjetas").Result;

            if (res.IsSuccessStatusCode)
            {
                string datos = res.Content.ReadAsStringAsync().Result;
                tarjetasList = JsonConvert.DeserializeObject<List<Mtarjeta>>(datos);
            }

            ViewData["clientesList"] = clientesList;
            ViewData["tarjetasList"] = tarjetasList;

            return View("Historial");
        }

        [HttpPost]
        public async Task<IActionResult> getHistorial(Mmovimiento movimiento)
        {
            List<Mmovimiento> movimientosList = new List<Mmovimiento>();
            bool bandera = true;
            string validacion = "";

            try
            {
                if (movimiento.idTarjeta == 0)
                {
                    validacion += "*Debe de elejir una tarjeta de credito<br>";
                    bandera = false;
                }

                if (string.IsNullOrEmpty(movimiento.mes))
                {
                    validacion += "*Debe de ingresar el mes<br>";
                    bandera = false;
                }

                if (bandera)
                {
                    string data = JsonConvert.SerializeObject(movimiento);
                    StringContent contenido = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Movimiento/historialMovimiento", contenido).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string datos = await response.Content.ReadAsStringAsync();
                        movimientosList = JsonConvert.DeserializeObject<List<Mmovimiento>>(datos);

                    } 
                }
            }
            catch (Exception)
            {

                validacion = "Ocurrio un error :(";
            }
            string[] arreglo = new string[] { validacion, JsonConvert.SerializeObject(movimientosList) };
            return Json(arreglo);
        }

        [HttpPost]
        public async Task<IActionResult> historialCompras(Mmovimiento movimiento)
        {
            List<Mmovimiento> movimientosList = new List<Mmovimiento>();
            string total = "";

            try
            {
                DateTime fechaActual = DateTime.Now;
                movimiento.mes = fechaActual.ToString("yyyy-MM");

                string data = JsonConvert.SerializeObject(movimiento);
                StringContent contenido = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Movimiento/historialCompras", contenido).Result;

                if (response.IsSuccessStatusCode)
                {
                    string datos = await response.Content.ReadAsStringAsync();
                    movimientosList = JsonConvert.DeserializeObject<List<Mmovimiento>>(datos);

                }

                HttpResponseMessage res = _client.PostAsync(_client.BaseAddress + "/Movimiento/totalCompras", contenido).Result;
                
                if (res.IsSuccessStatusCode)
                {
                    total = await res.Content.ReadAsStringAsync();

                }
            }
            catch (Exception)
            {
            }
            string[] arreglo = new string[] { total, JsonConvert.SerializeObject(movimientosList) };
            return Json(arreglo);
        }
    }
}
