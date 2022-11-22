using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UstaLab.Controllers;
using UstaLab.Models;

namespace UstaLab.Filtros
{
    public class ValidacionSesion : ActionFilterAttribute
    {
        public SimpleUser usuario;
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                
                base.OnActionExecuted(filterContext);
                usuario = (SimpleUser)HttpContext.Current.Session["User"];
                if(usuario == null)
                {
                    if (filterContext.Controller is LoginController == false)
                        filterContext.HttpContext.Response.Redirect("/Login/Index");
                }
            }
            catch (Exception ex)
            {
                filterContext.HttpContext.Response.Redirect("/Login/Index");
            }
        }
    }
}