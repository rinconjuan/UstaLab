using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UstaLab.Models;

namespace UstaLab.Controllers
{
    public class FuenteController : Controller
    {
        private string ApiWeb = "http://damian16-001-site1.htempurl.com/";
        [HttpPost]
        public async Task<ActionResult> PararRotor( )
        {

            RespuestaFuente respuestaFuente = new RespuestaFuente();
            MensajeErrorItem mensajeError = new MensajeErrorItem();
            string accion = "";

            foreach (var key in HttpContext.Request.Form.Keys)
            {
                if (key.Equals("key"))
                    accion = JsonConvert.DeserializeObject<string>(HttpContext.Request.Form["key"]);
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiWeb);
                    var requestContent = new MultipartFormDataContent();
                    requestContent.Add(new StringContent(accion));
                    var respuestaApi = await client.PostAsync("ManagementSource",requestContent).ConfigureAwait(false);
                    var respuestaBody = await respuestaApi.Content.ReadAsStringAsync();
                    if (respuestaApi.IsSuccessStatusCode)
                    {
                        respuestaFuente.Accion = respuestaBody;
                        return Json(new { respuestaFuente = respuestaFuente, success = true });
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError.MensajeError = "Error al obtener datos del usuario" + ex.Message;
                mensajeError.CodigoError = "APIR00";
                return Json(new { respuestaLogin = mensajeError, success = false });
            }

        }

        [HttpPost]
        public async Task<ActionResult> GetImagen()
        {
            Registro imagen = new Registro();
            MensajeErrorItem mensajeError = new MensajeErrorItem();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiWeb);
                    var respuestaApi = await client.GetAsync("DescargarImagen?Modo=BLQ").ConfigureAwait(false);
                    var respuestaBody = await respuestaApi.Content.ReadAsStringAsync();
                    if (respuestaApi.IsSuccessStatusCode)
                    {
                        imagen = JsonConvert.DeserializeObject<Registro>(respuestaBody);
                        byte[] fileBytes = Encoding.ASCII.GetBytes(imagen.Imagen);
                        return File( fileBytes, System.Net.Mime.MediaTypeNames.Image.Jpeg, "Registro Rotor Bloqueado" + ".jpg");
                        //return Json(new { imagen = imagen, success = true });
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError.MensajeError = "Error al obtener imagen" + ex.Message;
                mensajeError.CodigoError = "APIR00";
                return Json(new { respuestaLogin = mensajeError, success = false });
            }
        }
    }
}