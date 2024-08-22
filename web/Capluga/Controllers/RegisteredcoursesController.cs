using Capluga.App_Start;
using Capluga.Entities;
using Capluga.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capluga.Controllers
{
    
    public class RegisteredcoursesController : Controller
    {
        RegisteredcoursesModel modelRegister = new RegisteredcoursesModel();

        [HttpGet]
        public ActionResult RegistrarInscripcion(int quantity, long medicalCourseID)
        {
            var entidad = new InscripCursEnt();
            entidad.UserID = long.Parse(Session["UserID"].ToString());
            entidad.MedicalCourseID = medicalCourseID;
            entidad.Quantity = quantity;
            entidad.Registrationdate = DateTime.Now;

            modelRegister.RegistrarInscripcion(entidad);

            var datos = modelRegister.ConsultarInscripcion(long.Parse(Session["UserID"].ToString()));
            Session["Cant"] = datos.AsEnumerable().Sum(x => x.Quantity);
            Session["SubT"] = datos.AsEnumerable().Sum(x => x.SubTotal);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ConsultarInscripcion()
        {
            var datos = modelRegister.ConsultarInscripcion(long.Parse(Session["UserID"].ToString()));
            Session["TotalPago"] = datos.AsEnumerable().Sum(x => x.Total);
            return View(datos);
        }

        [HttpGet]
        public ActionResult ConsultaFacturasCurso()
        {
            var datos = modelRegister.ConsultaFacturasCurso(long.Parse(Session["UserID"].ToString()));
            return View(datos);
        }

        [HttpGet]
        public ActionResult ConsultaDetalleFacturaCurso(long q)
        {
            var datos = modelRegister.ConsultaDetalleFacturaCurso(q);
            return View(datos);
        }

        [HttpPost]
        public ActionResult Matricula()
        {
            var entidad = new InscripCursEnt();
            entidad.UserID = long.Parse(Session["UserID"].ToString());

            var respuesta = modelRegister.Matricula(entidad);
            var datos = modelRegister.ConsultarInscripcion(long.Parse(Session["UserID"].ToString()));
            Session["Cant"] = datos.AsEnumerable().Sum(x => x.Quantity);
            Session["SubT"] = datos.AsEnumerable().Sum(x => x.SubTotal);

            if (respuesta > 0)
            {
                return RedirectToAction("ConsultaFacturasCurso", "Registeredcourses");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido procesar su pago, verifica las unidades disponibles";
                return View("ConsultarInscripcion", datos);
            }
        }

        [HttpGet]
        public ActionResult EliminarRegistroCurso(long q)
        {
            modelRegister.EliminarRegistroCurso(q);

            var datos = modelRegister.ConsultarInscripcion(long.Parse(Session["UserID"].ToString()));
            Session["Cant"] = datos.AsEnumerable().Sum(x => x.Quantity);
            Session["SubT"] = datos.AsEnumerable().Sum(x => x.SubTotal);
            return RedirectToAction("ConsultarInscripcion", "Registeredcourses");
        }

        [HttpGet]
        public ActionResult ActualizarEstadoPC(long q)
        {
            var entidad = new FacturaCursoEnt();
            entidad.MasterPurchaseCurseID = q;

            string respuesta = modelRegister.ActualizarEstadoPC(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("EstadoCurso", "Registeredcourses");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido cambiar el estado del usuario";
                return View();
            }
        }
        [AdminFilter]
        [HttpGet]
        public ActionResult EstadoCurso()
        {
            var datos = modelRegister.EstadoCurso();
            return View(datos);
        }

    }
}