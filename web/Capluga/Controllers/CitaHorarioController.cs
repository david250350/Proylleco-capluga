using Capluga.Models;
using Capluga.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace Capluga.Controllers
{
    public class CitaHorarioController : Controller
    {
        CitaHorarioModel claseHorario = new CitaHorarioModel();

        [HttpGet]
        public ActionResult RegistrarHorarios()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarHorarios(HorarioEnt entidad)
        {
            string respuesta = claseHorario.RegistrarHorarios(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("ConsultaHorarios", "CitaHorario");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido registrar su Horario";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ConsultaHorarios()
        {
            var datos = claseHorario.ConsultaHorarios();
            return View(datos);
        }

        [HttpGet]
        public ActionResult ActualizarHorarios(long q)
        {
            var datos = claseHorario.ConsultaHorario(q);
            return View(datos);
        }

        [HttpPost]
        public ActionResult ActualizarHorarios(HorarioEnt entidad)
        {
            string respuesta = claseHorario.ActualizarHorarios(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("ConsultaHorarios", "CitaHorario");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido actualizar la Horario";
                return View();
            }
        }
    }
}