﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UstaLab.Models;
namespace UstaLab.Controllers
{
    
    public class HomeController : Controller
    {
        private string ApiWeb = "https://connectionapi20220316143145.azurewebsites.net";

        [HttpPost]
        public async Task<ActionResult> Index(DatosLogin dataUser)
        {
            RespuestaUsuarios respuestaUsuarios = new RespuestaUsuarios();
            MensajeErrorItem mensajeError = new MensajeErrorItem();
            var parametros = $"?email={dataUser.email}&contrasenia={dataUser.password}";
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiWeb);
                var respuestaApi = await client.GetAsync("GetUsuario" + parametros).ConfigureAwait(false);
                var respuestaBody = await respuestaApi.Content.ReadAsStringAsync();
                if (respuestaApi.IsSuccessStatusCode)
                {
                    respuestaUsuarios = JsonConvert.DeserializeObject<RespuestaUsuarios>(respuestaBody);
                    return View(respuestaUsuarios);

                }
                else
                {
                    if (respuestaBody.Contains("codigoError") || respuestaBody.Contains("mensajeError"))
                    {
                        mensajeError = JsonConvert.DeserializeObject<MensajeErrorItem>(respuestaBody);
                        return Json(new { respuestaLogin = mensajeError, success = false });
                    }
                    else
                    {
                        mensajeError.MensajeError = "Error desconocido, contacte al administrador";
                        mensajeError.CodigoError = "APIR00";
                        return Json(new { respuestaLogin = mensajeError, success = false });
                    }
                }
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}