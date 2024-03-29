﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UstaLab.Filtros;
using UstaLab.Models;
namespace UstaLab.Controllers
{
    
    public class PruebaVacioController : Controller
    {
        private string ApiWeb = "http://tesisfinal1628-001-site1.ftempurl.com";
        // GET: PruebaVacio
        
        [HttpPost]
        [ValidacionSesion]
        public async Task<ActionResult> GetImagen()
        {
            Imagenes imagen = new Imagenes();
            MensajeError mensajeError = new MensajeError();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiWeb);
                    var respuestaApi = await client.GetAsync("DescargarImagen?Modo=NOM").ConfigureAwait(false);
                    var respuestaBody = await respuestaApi.Content.ReadAsStringAsync();
                    if (respuestaApi.IsSuccessStatusCode)
                    {
                        imagen = JsonConvert.DeserializeObject<Imagenes>(respuestaBody);
                        var dataImagen = Convert.FromBase64String(imagen.DataImagen);
                        return Json(new {imagen = imagen.DataImagen, success = true });
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch(Exception ex)
            {
                mensajeError.Mensaje = "Error al obtener imagen" +  ex.Message;
                return Json(new { respuestaLogin = mensajeError, success = false });
            }            
        }

        [HttpPost]
        public async Task<ActionResult> PostAccion()
        {
            string keyPost = "";
            MensajeError mensajeError = new MensajeError();
            foreach (var key in HttpContext.Request.Form.Keys)
            {
                if (key.Equals("key"))
                    keyPost = JsonConvert.DeserializeObject<string>(HttpContext.Request.Form["key"]);
            }
            
            var parametros = $"?key={keyPost}";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiWeb);
                    var requestContent = new MultipartFormDataContent();
                    requestContent.Add(new StringContent(keyPost));

                    var respuestaApi = await client.PostAsync("Accion", requestContent).ConfigureAwait(false);
                    var respuestaBody = await respuestaApi.Content.ReadAsStringAsync();
                    if (respuestaApi.IsSuccessStatusCode)
                    {                        
                        return Json(new { respuestaAccion = "OK", success = true });
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError.Mensaje= "Error al obtener imagen" + ex.Message;
                return Json(new { respuestaLogin = mensajeError, success = false });
            }

        }

        [HttpPost]
        public async Task<ActionResult> PostVelocidad()
        {
            int velocidad = 0;
            MensajeError mensajeError = new MensajeError();
            foreach (var key in HttpContext.Request.Form.Keys)
            {
                if (key.Equals("velocidad"))
                    velocidad = JsonConvert.DeserializeObject<int>(HttpContext.Request.Form["velocidad"]);
            }

            var parametros = $"?velocidad={velocidad}";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiWeb);
                    var requestContent = new MultipartFormDataContent();
                    requestContent.Add(new StringContent(velocidad.ToString()));

                    var respuestaApi = await client.PostAsync("UpdateVelocidad", requestContent).ConfigureAwait(false);
                    var respuestaBody = await respuestaApi.Content.ReadAsStringAsync();
                    if (respuestaApi.IsSuccessStatusCode)
                    {
                        return Json(new { respuestaAccion = "OK", success = true });
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError.Mensaje = "Error al actualizar la velocidad" + ex.Message;
                return Json(new { respuestaLogin = mensajeError, success = false });
            }

        }

        [HttpPost]
        public async Task<ActionResult> ManagementVariador()
        {
            string accion = "";
            bool boolAccion = new bool();
            MensajeError mensajeError = new MensajeError();
            foreach (var key in HttpContext.Request.Form.Keys)
            {
                if (key.Equals("accion"))
                    accion = JsonConvert.DeserializeObject<string>(HttpContext.Request.Form["accion"]);
            }
            switch(accion){
                case "Run":
                    boolAccion = true;
                    break;  
                case "Stop":
                    boolAccion = false;
                    break;

            }
                
            var parametros = $"?funcion={boolAccion}";
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiWeb);
                    var requestContent = new MultipartFormDataContent();
                    requestContent.Add(new StringContent(boolAccion.ToString()));

                    var respuestaApi = await client.PostAsync("Run", requestContent).ConfigureAwait(false);
                    var respuestaBody = await respuestaApi.Content.ReadAsStringAsync();
                    if (respuestaApi.IsSuccessStatusCode)
                    {
                        return Json(new { respuestaAccion = "OK", success = true });
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                mensajeError.Mensaje = "Error al actualizar la velocidad" + ex.Message;
              
                return Json(new { respuestaLogin = mensajeError, success = false });
            }

        }
    }
}