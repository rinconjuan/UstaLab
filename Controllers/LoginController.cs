using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UstaLab.Modelos;
using UstaLab.Models;
using UstaLab.Utils;
namespace UstaLab.Controllers
{
    public class LoginController : Controller
    {
        private string ApiWeb = "http://damian16-001-site1.htempurl.com/";
        private string NameUser = "";
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login()
        {
            MensajeError mensajeError = new MensajeError();
            RespuestaUsuarios respuestaLogin = new RespuestaUsuarios();
            DatosLogin datosLogin = new DatosLogin();
            DateTime fechaActual = new DateTime();
            try
            {
               
                foreach (var key in HttpContext.Request.Form.Keys)
                {
                    if (key.Equals("data"))
                        datosLogin = JsonConvert.DeserializeObject<DatosLogin>(HttpContext.Request.Form["data"]);
                    if (key.Equals("horaActual"))
                        fechaActual = JsonConvert.DeserializeObject<DateTime>(HttpContext.Request.Form["horaActual"]);
                    
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
                        var paramAgenda = $"?FechaInicio={fechaActual.ToString("yyyy-MM-dd HH:mm:ss")}&FechaFin={(fechaActual.AddMinutes(30)).ToString("yyyyy-MM-dd HH:mm:ss")}&IdUser={respuestaLogin.Usuario.idUser}&Modo=LOG";

                        var respuestaAgenda = await client.GetAsync("GetAgenda" + paramAgenda).ConfigureAwait(false);
                        var resAgendaBody = await respuestaAgenda.Content.ReadAsStringAsync();
                        if (respuestaAgenda.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            SimpleUser sUser = new SimpleUser();
                            sUser.Nombres = respuestaLogin.Usuario.Nombres;
                            sUser.Apellidos = respuestaLogin.Usuario.Apellidos;
                            sUser.Email = respuestaLogin.Usuario.Email;

                            
                            var bodyAgenda = JsonConvert.DeserializeObject<RespuestaAgenda>(resAgendaBody);
                            var fechaFinSession = "";
                            foreach(var fecha in bodyAgenda.Agendas)
                            {
                                fechaFinSession = fecha.FechaFin.ToString("yyyy/MM/dd HH:mm:ss");                               

                            }
                            HttpCookie cokkieLog = new HttpCookie(sUser.Email);
                            cokkieLog.Name = sUser.Email;
                            cokkieLog.Expires = DateTime.Now.AddMinutes(1.0);
                            Request.Cookies.Add(cokkieLog);
                            Response.Cookies.Add(FormsAuthentication.GetAuthCookie(sUser.Email, true));
                            Session["UserName"] = respuestaLogin.Usuario.Nombres + " " + respuestaLogin.Usuario.Apellidos;
                            Session["User"] = sUser;



                            return Json(new { respuestaLogin = respuestaLogin, respuestaAgenda = fechaFinSession, paramAgenda = paramAgenda, success = true });

                        }
                        else
                        {
                            mensajeError.Mensaje = "Usted no esta agendado para esta franja horaria, intente mas tarde";
                            return Json(new { respuestaLogin = mensajeError, paramAgenda = paramAgenda,  success = false });
                        }
                        
                    }
                    else
                    {                        
                        mensajeError.Mensaje = "El usuario no existe o se ha digitado mal la contraseña";
                        return Json(new { respuestaLogin = mensajeError, success = false });
                                                  
                    }
                }
            }
            catch(ExcepcionMessage ex)
            {                
                return Json(new { respuestaLogin = ex.Message, success = false});
            }           

                
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public async Task<ActionResult> ConsultaUsuario()
        {
            MensajeError mensajeError = new MensajeError();
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
                    client.BaseAddress = new Uri(ApiWeb);
                    var respuestaApi = await client.GetAsync("GetDatos" + parametros).ConfigureAwait(false);
                    var respuestaBody = await respuestaApi.Content.ReadAsStringAsync();
                    respuestaLogin = JsonConvert.DeserializeObject<Usuarios>(respuestaBody);
                    if (respuestaLogin.idUser != 0)
                    {
                        DateTime fechaFin = fechaIni.AddMinutes(30);

                        
                        var paramAgenda = $"?FechaInicio={fechaIni.ToString("yyyyy-MM-dd HH:mm:ss")}&FechaFin={fechaFin.ToString("yyyyy-MM-dd HH:mm:ss")}&IdUser={respuestaLogin.idUser}&Modo=INS";


                        var respuestaAgenda = await client.GetAsync("GetAgenda" + paramAgenda).ConfigureAwait(false);
                        var resAgendaBody = await respuestaAgenda.Content.ReadAsStringAsync();
                        if (respuestaAgenda.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            return Json(new { respuestaLogin = respuestaLogin, success = true });

                        }
                        else   
                        {   
                            return Json(new { respuestaLogin = resAgendaBody, success = false });
                        }
                    }
                    else
                    {
                        mensajeError.Mensaje = "El email ingresado no se encuentra en el sistema";
                        return Json(new { respuestaLogin = mensajeError, success = false });

                    }
                }
            }
            catch (ExcepcionMessage ex)
            {
                return Json(new { respuestaLogin = ex.Message, success = false });
            }


        }

        [HttpPost]
        public async Task<ActionResult> Nuevousuario()
        {
            MensajeError mensajeError = new MensajeError();
            DatosUsuario datosUser = new DatosUsuario();
            RespuestaNuevoUsuario respuesta = new RespuestaNuevoUsuario();
            try
            {

                foreach (var key in HttpContext.Request.Form.Keys)
                {
                    if (key.Equals("data"))
                        datosUser = JsonConvert.DeserializeObject<DatosUsuario>(HttpContext.Request.Form["data"]);
                   
                }

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiWeb);
                    var requestContent = new StringContent(JsonConvert.SerializeObject(datosUser).ToString(), Encoding.UTF8, "application/json");
                    var respuestaApi = await client.PostAsync("AddUser", requestContent).ConfigureAwait(false);
                    var respuestaBody = await respuestaApi.Content.ReadAsStringAsync();
                    if (!respuestaApi.IsSuccessStatusCode)
                        throw new Exception(respuestaBody.ToString());

                    respuesta = JsonConvert.DeserializeObject<RespuestaNuevoUsuario>(respuestaBody);                    
                    return Json(new { respuestaLogin = respuesta, success = true });

                }
            }
            catch (Exception ex)
            {
                return Json(new { respuestaLogin = ex.Message, success = false });
            }


        }

    }
}