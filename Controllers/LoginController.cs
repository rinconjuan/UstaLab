using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UstaLab.Models;
using UstaLab.Utils;
namespace UstaLab.Controllers
{
    public class LoginController : Controller
    {
        private string ApiWeb = "http://damian16-001-site1.htempurl.com/";
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login()
        {
            MensajeErrorItem mensajeError = new MensajeErrorItem();
            RespuestaUsuarios respuestaLogin = new RespuestaUsuarios();
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
                    var respuestaApi = await client.GetAsync("GetUsuario" + parametros).ConfigureAwait(false);
                    var respuestaBody = await respuestaApi.Content.ReadAsStringAsync();
                    respuestaLogin = JsonConvert.DeserializeObject<RespuestaUsuarios>(respuestaBody);
                    if (respuestaLogin.EstadoLogin)
                    {
                        Session["UserName"] = respuestaLogin.Usuario.Nombres + " " + respuestaLogin.Usuario.Apellidos;
                        //Session.Timeout = 12;
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

        [HttpPost]
        public async Task<ActionResult> ConsultaUsuario()
        {
            MensajeErrorItem mensajeError = new MensajeErrorItem();
            Usuarios respuestaLogin = new Usuarios();
            string email = "";
            DateTime fechaIni = new DateTime();
            try
            {

                foreach (var key in HttpContext.Request.Form.Keys)
                {
                    if (key.Equals("data"))
                        email = JsonConvert.DeserializeObject<string>(HttpContext.Request.Form["data"]);
                    if (key.Equals("fechaini"))
                        fechaIni = JsonConvert.DeserializeObject<DateTime>(HttpContext.Request.Form["fechaini"]);
                }
                var parametros = $"?email={email}";

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44393/");
                    var respuestaApi = await client.GetAsync("GetDatos" + parametros).ConfigureAwait(false);
                    var respuestaBody = await respuestaApi.Content.ReadAsStringAsync();
                    respuestaLogin = JsonConvert.DeserializeObject<Usuarios>(respuestaBody);
                    if (respuestaLogin.idUser != 0)
                    {
                        DateTime fechaFin = fechaIni.AddMinutes(30);
                        var paramAgenda = $"?FechaInicio={fechaIni.ToString("yyyy-M-dd hh:mm:ss")}&FechaFin={fechaFin.ToString("yyyy-M-dd hh:mm:ss")}&IdUser={respuestaLogin.idUser}&Modo=INS";
                        var respuestaAgenda = await client.GetAsync("GetAgenda" + paramAgenda).ConfigureAwait(false);
                        var resAgendaBody = await respuestaAgenda.Content.ReadAsStringAsync();
                        if (respuestaAgenda.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            return Json(new { respuestaLogin = respuestaLogin, success = true });

                        }
                        else   
                        {   
                            return Json(new { respuestaLogin = respuestaLogin, success = false });
                        }
                    }
                    else
                    {
                        mensajeError.MensajeError = "El email ingresado no se encuentra en el sistema";
                        mensajeError.CodigoError = "APIR00";
                        return Json(new { respuestaLogin = mensajeError, success = false });

                    }
                }
            }
            catch (ExcepcionMessage ex)
            {
                return Json(new { respuestaLogin = ex.Message, success = false });
            }


        }

    }
}