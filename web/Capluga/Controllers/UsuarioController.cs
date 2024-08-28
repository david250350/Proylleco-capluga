using Capluga.Entities;
using Capluga.App_Start;
using Capluga.Models;
using System.Web.Mvc;

namespace Capluga.Controllers
{

    [OutputCache(NoStore = true, Duration = 0)]
    public class UsuarioController : Controller
    {

        UsuarioModel usuarioModel = new UsuarioModel();
        RolesModel rolesModel = new RolesModel();

        [AdminFilter]
        [HttpGet]
        public ActionResult Index()
        {
            var datos = usuarioModel.ConsultaUsuarios();
            return View(datos); ;
        }

        [HttpGet]
        public ActionResult ActualizarEstadoUsuario(long q)
        {
            var entidad = new UsuarioEnt();
            entidad.UserID = q;

            string respuesta = usuarioModel.ActualizarEstadoUsuario(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("Index", "Usuario");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido cambiar el estado del usuario";
                return View();
            }
        }


        [HttpGet]
        public ActionResult PerfilUsuario()
        {
            long q = long.Parse(Session["UserID"].ToString());
            var datos = usuarioModel.ConsultaUsuario(q);
            return View(datos);
        }

        [HttpPost]
        public ActionResult PerfilUsuario(UsuarioEnt entidad)
        {
        
            string respuesta = usuarioModel.ActualizarCuenta(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido actualizar su información";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarUsuario(long q)
        {
            // Obtener los detalles del usuario para editar
            var datos = usuarioModel.ConsultaUsuario(q);
            if (datos == null)
            {
                return HttpNotFound();
            }

            // Obtener los roles desde la API
            var roles = rolesModel.ListaRoles();

            // Crear SelectList usando las propiedades correctas
            ViewBag.Roles = new SelectList(roles, "RolesID", "RoleName"); // RolesID será el Value y RoleName el Text

            return View(datos);
        }

        [HttpPost]
        public ActionResult ActualizarUsuario(UsuarioEnt entidad)
        {
            string respuesta = usuarioModel.ActualizarCuenta(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("VistaProducto", "Producto");
            }
            else
            {
                // Recargar los roles si ocurre un error
                var roles = rolesModel.ListaRoles();
                ViewBag.Roles = new SelectList(roles, "RolesID", "RoleName"); // RolesID será el Value y RoleName el Text

                ViewBag.MensajeUsuario = "No se ha podido actualizar su información";
                return View(entidad);
            }
        }


    }
}