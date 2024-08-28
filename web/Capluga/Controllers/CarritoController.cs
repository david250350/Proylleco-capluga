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

            int cantidadTotal = datos.Sum(x => x.Quantity);
            string mensajeUsuario = ObtenerMensajePorCantidad(cantidadTotal);

            ViewBag.MensajeUsuario = mensajeUsuario;

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
        [HttpGet]
        public ActionResult ConsultaFacturas()
        {
            var datos = modelCarrito.ConsultaFacturas(long.Parse(Session["UserID"].ToString()));
            return View(datos);
        }

        [HttpGet]
        public ActionResult ConsultaDetalleFactura(long q)
        {
            var datos = modelCarrito.ConsultaDetalleFactura(q);
            return View(datos);
        }



        private string ObtenerMensajePorCantidad(int cantidad)
        {
            if (cantidad == 1)
            {
                return "¿Estás seguro que es todo lo que necesitas? Te invitamos a revisar más productos.";
            }
            else if (cantidad > 1 && cantidad <= 5)
            {
                return "¡Gracias por agregar más productos! ¿Necesitas algo más?";
            }
            else if (cantidad > 5 && cantidad <= 10)
            {
                return "¡Estás haciendo una gran compra! Sigue agregando más si lo necesitas.";
            }
            else if (cantidad > 10 && cantidad <= 15)
            {
                return "¡Tienes muchos productos en tu carrito! ¿Listo para finalizar?";
            }
            else if (cantidad > 15)
            {
                return "¡Carrito lleno! Estás listo para una compra grande.";
            }
            else
            {
                return string.Empty;
            }
        }





    }
}
