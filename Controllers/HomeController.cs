using Newtonsoft.Json;
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
        private string ApiWeb = "http://damian16-001-site1.htempurl.com/";

        [HttpPost]
        public async Task<ActionResult> Index(DatosLogin dataUser)
        {
            RespuestaUsuarios respuestaUsuarios = new RespuestaUsuarios();
            MensajeErrorItem mensajeError = new MensajeErrorItem();
            var parametros = $"?email={dataUser.email}&password={dataUser.password}";
            try
            {
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
                        throw new Exception();
                    }
                }
            }
            catch(Exception ex)
            {
                mensajeError.MensajeError = "Error al obtener datos del usuario" + ex.Message;
                mensajeError.CodigoError = "APIR00";
                return Json(new { respuestaLogin = mensajeError, success = false });
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