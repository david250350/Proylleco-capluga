using Capluga.Entities;
using Capluga.Models;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Capluga.Controllers
{
    public class AgendaCitaController : Controller
    {

        AgendaCitaModel modelAgenda = new AgendaCitaModel();
        CitaHorarioModel claseHorario = new CitaHorarioModel();

        [HttpGet]
        public ActionResult RegistrarCita()
        {

            ViewBag.Horarios = claseHorario.verHorarios();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegistrarCita(AgendaEnt entidad)
        {
            if (Session["UserID"] == null || Session["AddressID"] == null)
            {
                TempData["ErrorMessage"] = "No se ha iniciado sesión o la sesión ha caducado.";
                return RedirectToAction("RegistrarCita");
            }

            if (!long.TryParse(Session["UserID"].ToString(), out long userID) ||
                !long.TryParse(Session["AddressID"].ToString(), out long addressID))
            {
                TempData["ErrorMessage"] = "Error al obtener información de la sesión.";
                return RedirectToAction("RegistrarCita");
            }

            entidad.UserID = userID;
            entidad.AddressID = addressID;

            var respuesta = await modelAgenda.RegistrarCita(entidad);

            if (respuesta.Equals("OK", StringComparison.OrdinalIgnoreCase))
            {
                TempData["SuccessMessage"] = "La cita ha sido registrada con éxito.";
                return RedirectToAction("RegistrarCita");
            }
            else
            {
                TempData["ErrorMessage"] = "Error al registrar la cita: " + respuesta;
            }

            return RedirectToAction("RegistrarCita"); // Redireccionar para recargar la página y mostrar el mensaje apropiado.
        }

        [HttpGet]
        public ActionResult ConsultaCita()
        {
            var datos = modelAgenda.ConsultaCita(long.Parse(Session["UserID"].ToString()));
            return View(datos);
        }

        [HttpGet]
        public ActionResult ConsultaCitas()
        {
            var datos = modelAgenda.ConsultaCitas();
            return View(datos);
        }

        [HttpGet] // Asegúrate de usar HttpPost para operaciones que modifican datos
        public ActionResult EliminarRegistroCita(long q)
        {
            try
            {
                // Llama directamente al método sin intentar asignarlo a una variable
                modelAgenda.EliminarRegistroCita(q);

                // Si el método se completa sin errores, asume éxito
                TempData["SuccessMessage"] = "Cita eliminada exitosamente.";
                return RedirectToAction("ConsultaCita");
            }
            catch (Exception ex)
            {
                // Si hay una excepción, maneja el error
                TempData["ErrorMessage"] = "No se pudo eliminar la cita: " + ex.Message;
                return RedirectToAction("ConsultaCita");
            }
        }
    }
}