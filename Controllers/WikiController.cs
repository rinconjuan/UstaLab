using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UstaLab.Controllers
{
    public class WikiController : Controller
    {
        // GET: Wiki
        public ActionResult PruebaRotorBloqueado()
        {
            var nameUser = Session["UserName"];
            ViewBag.MSG = Session["UserName"];
            return View();
        }

        public ActionResult PruebaVacio()
        {
            var nameUser = Session["UserName"];
            ViewBag.MSG = Session["UserName"];
            return View();
        }
    }
}