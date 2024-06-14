using Capluga.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capluga.Controllers
{
    public class HomeController : Controller
    {

        ProductoModel modelProducto = new ProductoModel();

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Productos()
        {
            var datos = modelProducto.Productos();
            return View(datos);
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Admin()
        {
            return View();
        }
    }
}