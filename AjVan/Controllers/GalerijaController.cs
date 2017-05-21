using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AjVan.Controllers
{
    public class GalerijaController : Controller
    {
        // GET: Galerija
        public ActionResult Index()
        {
            return View();
        }
    }
}