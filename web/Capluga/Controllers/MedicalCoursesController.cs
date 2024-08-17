using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Capluga.Entities;
using System.IO;
using Capluga.Models;
using PagedList;

namespace Capluga.Controllers
{
    public class MedicalCoursesController : Controller
    {
        MedicalCoursesModel CursoModel = new MedicalCoursesModel();


        [HttpGet]
        public ActionResult VistaCurso(int? page)
        {

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var datos = CursoModel.ConsultaCursos();
            return View(datos.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult RegistrarCursos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarCursos(HttpPostedFileBase ImgCurso, CursoEnt entidad)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
                entidad.Image = string.Empty;
            entidad.State = true;

            long medicalCourseID = CursoModel.RegistrarCursos(entidad);

            if (medicalCourseID > 0)
            {
                if (ImgCurso != null)
                {


                    string extension = Path.GetExtension(Path.GetFileName(ImgCurso.FileName));
                    string ruta = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + medicalCourseID + extension;
                    ImgCurso.SaveAs(ruta);

                    entidad.Image = "/Images/" + medicalCourseID + extension;
                    entidad.MedicalCourseID = medicalCourseID;

                    CursoModel.ActualizarRutaCurso(entidad);
                }
                return RedirectToAction("ConsultaCursos", "MedicalCourses");
               
        }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido registrar su producto";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarCurso(long q)
        {
            var datos = CursoModel.ConsultaCurso(q);
            if (datos == null)
            {
                return View();
            }
            if (datos.DateandTime == DateTime.MinValue)
            {
                datos.DateandTime = DateTime.Now; // Set a default value if needed
            }
            return View(datos);
        }


        [HttpPost]
        public ActionResult ActualizarCurso(HttpPostedFileBase ImgCurso, CursoEnt entidad)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
                string respuesta = CursoModel.ActualizarCurso(entidad);

            if (respuesta == "OK")
            {
                if (ImgCurso != null)
                {
                    string extension = Path.GetExtension(Path.GetFileName(ImgCurso.FileName));
                    string ruta = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + entidad.MedicalCourseID + extension;
                    ImgCurso.SaveAs(ruta);

                    entidad.Image = "/Images/" + entidad.MedicalCourseID + extension;
                    entidad.MedicalCourseID = entidad.MedicalCourseID;

                    CursoModel.ActualizarRutaCurso(entidad);
                }


                return RedirectToAction("ConsultaCursos", "MedicalCourses");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido actualizar la información del Curso";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ConsultaCursos()
        {
            var datos = CursoModel.ConsultaCursos();
            return View(datos);
        }



        [HttpGet]
        public ActionResult ActualizarEstadoCurso(long q)
        {
            var entidad = new CursoEnt();
            entidad.MedicalCourseID = q;

            string respuesta = CursoModel.ActualizarEstadoCurso(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("ConsultaCursos", "MedicalCourses");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido cambiar el estado del Curso";
                return View();
            }
        }


       
        [HttpGet]
        public ActionResult DetallesCurso(long id)
        {
            var curso = CursoModel.ConsultaCurso(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }
    
}
}