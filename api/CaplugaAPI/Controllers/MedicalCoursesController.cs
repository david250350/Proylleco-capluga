using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CaplugaAPI.Controllers
{
    public class MedicalCoursesController : ApiController
    {

        [HttpPost]
        [Route("RegistrarCursos")]
        public long RegistrarCursos(MedicalCourses medicalCourses)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.MedicalCourses.Add(medicalCourses);
                context.SaveChanges();
                return medicalCourses.MedicalCourseID;
            }
        }


        [HttpGet]
        [Route("ConsultaCursos")]
        public List<MedicalCourses> ConsultaCursos()
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.MedicalCourses.ToList();
            }
        }

        [HttpGet]
        [Route("ConsultaCurso")]
        public MedicalCourses ConsultaCurso(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from x in context.MedicalCourses
                        where x.MedicalCourseID == q
                        select x).FirstOrDefault();
            }
        }

        [HttpPut]
        [Route("ActualizarEstadoCurso")]
        public string ActualizarEstadoCurso(MedicalCourses medicalCourses)
        {
            using (var context = new CAPLUGAEntities())
            {
                var datos = context.MedicalCourses.Where(x => x.MedicalCourseID == medicalCourses.MedicalCourseID).FirstOrDefault();
                datos.State = (datos.State ? false : true);
                context.SaveChanges();
                return "OK";
            }
        }


        [HttpPut]
        [Route("ActualizarRutaCurso")]
        public string ActualizarRutaCurso(MedicalCourses medicalCourses)
        {
            using (var context = new CAPLUGAEntities())
            {
                var datos = context.MedicalCourses.Where(x => x.MedicalCourseID == medicalCourses.MedicalCourseID).FirstOrDefault();
                datos.Image = medicalCourses.Image;
                context.SaveChanges();
                return "OK";
            }
        }

       [HttpPut]
[Route("ActualizarCurso")]
public IHttpActionResult ActualizarCurso(MedicalCourses medicalCourses)
{
    using (var context = new CAPLUGAEntities())
    {
        var datos = context.MedicalCourses.Where(x => x.MedicalCourseID == medicalCourses.MedicalCourseID).FirstOrDefault();
        if (datos == null)
        {
            return Content(HttpStatusCode.NotFound, new { Message = "No se encontró el curso con el ID proporcionado." });
        }

        datos.Name = medicalCourses.Name;
        datos.Description = medicalCourses.Description;
        datos.Quantity = medicalCourses.Quantity;
        datos.Price = medicalCourses.Price;
        datos.DateandTime = medicalCourses.DateandTime;
        context.SaveChanges();
        
        return Ok(new { Message = "OK" });
    }
}

    }
}