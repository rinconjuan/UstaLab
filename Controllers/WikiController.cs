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
            return View();
        }

        public ActionResult PruebaVacio()
        {
            return View();
        }
    }
}