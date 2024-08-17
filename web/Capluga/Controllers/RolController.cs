using Capluga.Entities;
using Capluga.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capluga.Controllers
{
    public class RolController : Controller
    {

        RolesModel modelroles = new RolesModel();

        [HttpGet]
        public ActionResult ListaRoles()
        {
            var datos = modelroles.ListaRoles();
            return View(datos);
        }

        [HttpGet]
        public ActionResult CrearRol()
        {
            return View();
        }



        [HttpPost]
        public ActionResult CrearRol(RoleEnt entidad)
        {
          

            string respuesta = modelroles.CrearRol(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("ListaRoles", "Rol");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido registrar.";
                return View(entidad);
            }
        }


        [HttpGet]
        public ActionResult ActualizarRol(long q)
        {
            var datos = modelroles.Rol(q);
            return View(datos);
        }
        [HttpPost]
        public ActionResult ActualizarRol(RoleEnt entidad)
        {
           
            string respuesta = modelroles.ActualizarRol(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("ListaRoles", "Rol");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido actualizar la información";
                return View();
            }
        }

        [HttpGet]
        public ActionResult EliminarRol(long q)
        {
            modelroles.EliminarRol(q);


            
            return RedirectToAction("ListaRoles", "Rol");
        }
    }
}
