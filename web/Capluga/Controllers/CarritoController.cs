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
        public ActionResult ConsultaCarrito()
        {
            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["UserID"].ToString()));
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
        [HttpGet]
        public ActionResult ConsultaCarritoadm()
        {
            var datos = modelCarrito.ConsultaCarritoadm();
            return View(datos);
        }

        [HttpPost]
        public ActionResult PagarCarrito()
        {
            var entidad = new CarritoEnt();
            entidad.UserID = long.Parse(Session["UserID"].ToString());

            var respuesta = modelCarrito.PagarCarrito(entidad);
            var datos = modelCarrito.ConsultarCarrito(long.Parse(Session["UserID"].ToString()));
            Session["Cant"] = datos.AsEnumerable().Sum(x => x.Quantity);
            Session["SubT"] = datos.AsEnumerable().Sum(x => x.SubTotal);

            if (respuesta > 0)
            {
                return RedirectToAction("ConsultaFacturas", "Carrito");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido procesar su pago, verifica las unidades disponibles";
                return View("ConsultaCarrito", datos);
            }
        }

        [HttpGet]
        public ActionResult ActualizarEstadoPago(long q)
        {
            var entidad = new FacturaEnt();
            entidad.MasterPurchaseID = q;

            string respuesta = modelCarrito.ActualizarEstadoPago(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("ConsultaCarritoadm", "Carrito");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido cambiar el estado del usuario";
                return View();
            }
        }
    }
}
