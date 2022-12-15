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
    [Authorize]
    public class HomeController : Controller
    {
        private string ApiWeb = "http://apites-001-site1.atempurl.com";


        
        public ActionResult Index()
        {
            var nameUser = Session["UserName"];
            ViewBag.MSG = Session["UserName"];
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Index(DatosLogin dataUser)
        {
            RespuestaUsuarios respuestaUsuarios = new RespuestaUsuarios();
            MensajeError mensajeError = new MensajeError();
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
                        var nameUser = Session["UserName"];
                        ViewBag.MSG = Session["UserName"];
                        return View();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch(Exception ex)
            {
                mensajeError.Mensaje = "Error al obtener datos del usuario" + ex.Message;
                return Json(new { respuestaLogin = mensajeError, success = false });
            }
            
        }      
    }
}