using CaplugaAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CaplugaAPI.Controllers
{
    public class RegisteredcoursesController : ApiController
    {

        [HttpPost]
        [Route("RegistrarInscripcion")]
        public string RegistrarInscripcion(Registeredcourses registeredcourses)
        {
            
            using (var context = new CAPLUGAEntities())
            {
                var datos = (from x in context.Registeredcourses
                             where x.UserID == registeredcourses.UserID
                                && x.MedicalCourseID == registeredcourses.MedicalCourseID
                             select x).FirstOrDefault();

                if (datos == null)
                {
                    Registeredcourses registro = new Registeredcourses();
                    registro.UserID = registeredcourses.UserID;
                    registro.MedicalCourseID = registeredcourses.MedicalCourseID;
                    registro.Quantity = registeredcourses.Quantity;
                    registro.Registrationdate = registeredcourses.Registrationdate;
                    context.Registeredcourses.Add(registro);
                    context.SaveChanges();
                }
                else
                {
                    datos.Quantity = registeredcourses.Quantity;
                    context.SaveChanges();
                }
                return "OK";
            }
        }

        [HttpGet]
        [Route("ConsultarInscripcion")]
        public object ConsultarInscripcion(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from x in context.Registeredcourses
                        join y in context.MedicalCourses on x.MedicalCourseID equals y.MedicalCourseID
                        where x.UserID == q
                        select new
                        {
                            x.RegisteredcoursesID,
                            x.UserID,
                            x.MedicalCourseID,
                            x.Quantity,
                            x.Registrationdate,
                            y.Name,
                            y.Price,
                            SubTotal = (y.Price * x.Quantity),
                            Impuesto = (y.Price * x.Quantity) * 0.13M,
                            Total = (y.Price * x.Quantity) + (y.Price * x.Quantity) * 0.13M
                        }).ToList();
            }
        }

        [HttpGet]
        [Route("ConsultaFacturasCurso")]
        public List<MasterPurchaseCurse> ConsultaFacturasCurso(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from x in context.MasterPurchaseCurse
                        where x.UserID == q
                        select x).ToList();
            }
        }

        [HttpGet]
        [Route("ConsultaDetalleFacturaCurso")]
        public object ConsultaDetalleFacturaCurso(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from x in context.DetailCurse
                        join y in context.MedicalCourses on x.MedicalCourseID equals y.MedicalCourseID
                        join z in context.MasterPurchaseCurse on x.MasterPurchaseCurseID equals z.MasterPurchaseCurseID
                        join c in context.Users on z.UserID equals c.UserID
                        where x.MasterPurchaseCurseID == q
                        select new
                        {
                            x.MasterPurchaseCurseID,
                            y.Name,
                            x.PaidPrice,
                            x.PaidQuantity,
                            x.Tax,
                            x.PaymentStatus,
                            z.Users.UserName,
                            z.Users.Surnames,
                            x.MasterPurchaseCurse.PurchaseDate,
                            SubTotal = (x.PaidPrice * x.PaidQuantity),
                            Impuesto = (x.Tax * x.PaidQuantity),
                            Total = (x.PaidPrice * x.PaidQuantity) + (x.Tax * x.PaidQuantity)

                        }).ToList();
            }
        }

        [HttpDelete]
        [Route("EliminarRegistroCurso")]
        public void EliminarRegistroCurso(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                var datos = (from x in context.Registeredcourses
                             where x.RegisteredcoursesID == q
                             select x).FirstOrDefault();

                context.Registeredcourses.Remove(datos);
                context.SaveChanges();
            }
        }

        [HttpPost]
        [Route("Matricula")]
        public int Matricula(Registeredcourses registeredcourses)
        {
            using (var context = new CAPLUGAEntities())
            {
                return context.tuition(registeredcourses.UserID);
            }
        }

        [HttpPut]
        [Route("ActualizarEstadoPC")]
        public string ActualizarEstadoPC(FacturaCursoEnt entidad)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.ApprovePaymentTuition(entidad.MasterPurchaseCurseID);

                return "OK"; // Asegúrate de que solo se retorna "OK"
            }
        }

        [HttpGet]
        [Route("EstadoCurso")]
        public List<DetailCurse> EstadoCurso()
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.DetailCurse
                            select x).ToList();
                }
            }
            catch (Exception)
            {
                return new List<DetailCurse>();
            }
        }
    }
}