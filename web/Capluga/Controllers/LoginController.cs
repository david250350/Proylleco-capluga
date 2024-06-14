using Capluga.Entities;
using Capluga.Models;
using System.Linq;
using System.Web.Mvc;

namespace Capluga.Controllers
{
    public class LoginController : Controller
    {

        UsuarioModel usuarioModel = new UsuarioModel();
        RolesModel rolesModel = new RolesModel();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("IniciarSesion", "Login");
        }


        [HttpGet]
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistrarCuenta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarCuenta(UsuarioEnt entidad)
        {
            string respuesta = usuarioModel.RegistrarCuenta(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("IniciarSesion", "Login");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido registrar su información";
                return View();
            }
        }

        [HttpPost]
        public ActionResult IniciarSesion(UsuarioEnt entidad)
        {
            var respuesta = usuarioModel.IniciarSesion(entidad);

            if (respuesta != null)
            {
                Session["UserID"] = respuesta.UserID;
                Session["Nombre"] = respuesta.UserName;
                Session["Rol"] = respuesta.RoleID;
                Session["AddressID"] = respuesta.AddressID;
                return RedirectToAction("Productos", "Home");
            }
            else
            {
                ViewBag.MensajeUsuario = "Usuario o contraseña incorrectas, por favor verifique sus credenciales.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult RecuperarCuenta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarCuenta(UsuarioEnt entidad)
        {
            string respuesta = usuarioModel.RecuperarCuenta(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("IniciarSesion", "Login");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido recuperar su información";
                return View();
            }
        }

    }
}