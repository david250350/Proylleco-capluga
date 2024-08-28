using CaplugaAPI.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.util;
using System.Web.Http;

namespace CaplugaAPI.Controllers
{
    public class RegisteredcoursesController : ApiController
    {

        Utilitarios util = new Utilitarios();

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

                DocumentController documentController = new DocumentController();
                var factura = context.MasterPurchaseCurse.FirstOrDefault(c => c.MasterPurchaseCurseID == entidad.MasterPurchaseCurseID);
                List<DetailCurse> cursos = (from x in context.DetailCurse
                                               where x.MasterPurchaseCurseID == entidad.MasterPurchaseCurseID
                                               select x).ToList();
                var usuario = context.Users.FirstOrDefault(u => u.UserID == factura.UserID);
                byte[] pdf = documentController.CursoPDF(factura, cursos, usuario);

                string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "Templates\\Facturacion.html";
                string html = File.ReadAllText(rutaArchivo);
                html = html.Replace("@@Nombre", usuario.UserName);
                html = html.Replace("@@Total", factura.TotalPurchase.ToString());

                util.EnviarCorreo(usuario.Email, "Factura de compra CAPLUGA", html, pdf, "CAPLUGA_Factura_" + factura.MasterPurchaseCurseID + ".pdf");

                context.ApprovePaymentTuition(entidad.MasterPurchaseCurseID);

                return "OK"; // Asegúrate de que solo se retorna "OK"
            }
        }

        [HttpGet]
        [Route("EstadoCurso")]
        public List<FacturaCursoEnt> EstadoCurso()
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.DetailCurse
                            join p in context.MedicalCourses on x.MedicalCourseID equals p.MedicalCourseID
                            join z in context.MasterPurchaseCurse on x.MasterPurchaseCurseID equals z.MasterPurchaseCurseID
                            join u in context.Users on z.UserID equals u.UserID

                            select new FacturaCursoEnt
                            {
                                DetailCurseID = x.DetailCurseID,
                                MasterPurchaseCurseID = x.MasterPurchaseCurseID,
                                medicalCourseID = x.MedicalCourseID,
                                PaymentStatus = x.PaymentStatus,
                                PaidQuantity = x.PaidQuantity,
                                TotalPurchase = z.TotalPurchase,
                                UserID = z.UserID,
                                Name = p.Name,
                                UserName = u.UserName,
                                Surnames = u.Surnames

                            }).ToList();
                }
            }
            catch (Exception)
            {
                return new List<FacturaCursoEnt>();
            }
        }
    }
}