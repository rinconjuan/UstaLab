using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UstaLab.Models;
using UstaLab.Utils;
namespace UstaLab.Controllers
{
    public class LoginController : Controller
    {
        private string ApiWeb = "https://connectionapi20220316143145.azurewebsites.net";
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login()
        {
            MensajeErrorItem mensajeError = new MensajeErrorItem();
            RespuestaLogin respuestaLogin = new RespuestaLogin();
            DatosLogin datosLogin = new DatosLogin();
            try
            {
               
                foreach (var key in HttpContext.Request.Form.Keys)
                {
                    if (key.Equals("data"))
                        datosLogin = JsonConvert.DeserializeObject<DatosLogin>(HttpContext.Request.Form["data"]);                    
                }
                var parametros = $"?email={datosLogin.email}&password={datosLogin.password}";

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiWeb);
                    var respuestaApi = await client.GetAsync("GetAutenticacion" + parametros).ConfigureAwait(false);
                    var respuestaBody = await respuestaApi.Content.ReadAsStringAsync();
                    respuestaLogin = JsonConvert.DeserializeObject<RespuestaLogin>(respuestaBody);
                    if (respuestaLogin.statusLogin)
                    {                        
                        return Json(new { respuestaLogin = respuestaLogin, success = true });
                    }
                    else
                    {
                        
                        mensajeError.MensajeError = "El usuario no existe o se ha digitado mal la contraseña";
                        mensajeError.CodigoError = "APIR00";
                        return Json(new { respuestaLogin = mensajeError, success = false });
                                                  
                    }
                }
            }
            catch(ExcepcionMessage ex)
            {                
                return Json(new { respuestaLogin = ex.Message, success = false});
            }           

                
        }

    }
}