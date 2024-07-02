using Capluga.Entities;
using Capluga.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capluga.Controllers
{
    public class CarritoController : Controller
    {
        CarritoModel modelCarrito = new CarritoModel();

        [HttpGet]
        public ActionResult RegistrarCarrito(int Quantity, long MedicalImplementsID)
        {
            var entidad = new CarritoEnt();
            entidad.UserID = long.Parse(Session["UserID"].ToString());
            entidad.MedicalImplementsID = MedicalImplementsID;
            entidad.Quantity = Quantity;

            modelCarrito.RegistrarCarrito(entidad);

            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["UserID"].ToString()));
            Session["Cant"] = datos.AsEnumerable().Sum(x => x.Quantity);
            Session["SubT"] = datos.AsEnumerable().Sum(x => x.SubTotal);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ConsultaCarrito(string orderByName, string orderByPrice)
        {
            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["UserID"].ToString()));

            if (!string.IsNullOrEmpty(orderByName))
            {
                datos = orderByName == "asc" ? datos.OrderBy(x => x.Name).ToList() : datos.OrderByDescending(x => x.Name).ToList();
            }

            if (!string.IsNullOrEmpty(orderByPrice))
            {
                datos = orderByPrice == "asc" ? datos.OrderBy(x => x.Price).ToList() : datos.OrderByDescending(x => x.Price).ToList();
            }

            Session["TotalPago"] = datos.AsEnumerable().Sum(x => x.Total);
            return View(datos);
        }




        [HttpGet]
        public ActionResult EliminarRegistroCarrito(long q)
        {
            modelCarrito.EliminarRegistroCarrito(q);

            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["UserID"].ToString()));
            Session["Cant"] = datos.AsEnumerable().Sum(x => x.Quantity);
            Session["SubT"] = datos.AsEnumerable().Sum(x => x.SubTotal);
            return RedirectToAction("ConsultaCarrito", "Carrito");
        }

    }
}