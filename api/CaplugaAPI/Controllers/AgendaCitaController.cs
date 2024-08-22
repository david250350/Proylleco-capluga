using CaplugaAPI.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace CaplugaAPI.Controllers
{
    
    public class AgendaCitaController : ApiController
    {
        Utilitarios util = new Utilitarios();

        [HttpPost]
        [Route("RegistrarCita")]
        public IHttpActionResult RegistrarCita(AppointmentScheduling appointmentScheduling)
        {
            using (var context = new CAPLUGAEntities())
            {
                var horario = context.ScheduleAppointment
                              .FirstOrDefault(h => h.ScheduleID == appointmentScheduling.ScheduleID);

                if (horario == null || horario.IsBooked)
                {
                    return BadRequest("El horario no está disponible.");
                }

                AppointmentScheduling nuevaCita = new AppointmentScheduling
                {
                    UserID = appointmentScheduling.UserID,
                    AddressID = appointmentScheduling.AddressID,
                    ScheduleID = appointmentScheduling.ScheduleID,
                    Name = appointmentScheduling.Name,
                    Description = appointmentScheduling.Description
                };

                horario.IsBooked = true;

                if (CorreoAgenda(nuevaCita.UserID, nuevaCita, horario))
                {
                    context.AppointmentScheduling.Add(nuevaCita);
                    context.SaveChanges();

                    return Ok("La cita ha sido registrada con éxito.");
                }
                return NotFound();
            }
        }

        [HttpGet]
        [Route("ConsultaCitas")]
        public List<AgendaEnt> ConsultaCitas()
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    var result = (from x in context.AppointmentScheduling
                                  join y in context.ScheduleAppointment on x.ScheduleID equals y.ScheduleID
                                  join z in context.Users on x.UserID equals z.UserID
                                  join u in context.Addresses on x.AddressID equals u.AddressID

                            
                            
                            select new AgendaEnt
                            {
                            AppointmentID = x.AppointmentID,
                            Name = x.Name,
                            Description = x.Description,
                            UserID = x.UserID,
                            UserName = z.UserName,
                            Surnames = z.Surnames,
                            ScheduleID = x.ScheduleID,
                            Dname = y.Dname,
                            DateandTime = y.DateandTime,

                            AddressID = u.AddressID,
                            Country = u.Country,
                            State = u.State,
                            City = u.City,
                            District = u.District,
                            Street = u.Street,
                            ZipCode = u.ZipCode,
                          

                            }).ToList();
                    return result;
                }
            }
            catch (Exception)
            {
                return new List<AgendaEnt>();
            }
        }

        [HttpGet]
        [Route("ConsultaCita")]
        public object ConsultaCita(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from x in context.AppointmentScheduling
                        join y in context.ScheduleAppointment on x.ScheduleID equals y.ScheduleID
                        join z in context.Users on x.UserID equals z.UserID 
                        where x.UserID == q
                        select new
                        {
                            x.AppointmentID,
                            x.Users.UserName,
                            x.Users.Surnames,
                            x.Name,
                            x.Description,
                            x.ScheduleAppointment.Dname,
                            x.ScheduleAppointment.DateandTime
                         
                        }).ToList();
            }
        }
   
        

        [HttpPut]
        [Route("Actualizarcita")]
        public string Actualizarcita(AgendaEnt entidad)
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    context.UpdateAppointment(entidad.AppointmentID, entidad.UserID, entidad.AddressID, entidad.Name, entidad.Description, entidad.ScheduleID);
                    return "OK";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        [HttpDelete]
        [Route("EliminarRegistroCita")]
        public IHttpActionResult EliminarRegistroCita(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                var cita = context.AppointmentScheduling.FirstOrDefault(x => x.AppointmentID == q);
                if (cita == null)
                {
                    return NotFound(); // Retorna un código de estado 404 si no se encuentra la cita
                }

                // Suponiendo que tienes una relación o puedes obtener el horario asociado directamente
                var horario = context.ScheduleAppointment.FirstOrDefault(h => h.ScheduleID == cita.ScheduleID);
                if (horario != null)
                {
                    horario.IsBooked = false; // Marca el horario como disponible
                    context.Entry(horario).State = System.Data.Entity.EntityState.Modified;
                }

                context.AppointmentScheduling.Remove(cita);
                context.SaveChanges();
                return Ok(); // Retorna un código de estado 200 indicando que todo salió bien
            }
        }
        public bool CorreoAgenda(long usuarioID, AppointmentScheduling cita, ScheduleAppointment horario)
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    var usuario = context.Users.FirstOrDefault(u => u.UserID == usuarioID);

                    string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "Templates\\AgendaCita.html";
                    string html = File.ReadAllText(rutaArchivo);
                    html = html.Replace("@@Nombre", usuario.UserName);
                    html = html.Replace("@@Hora", horario.DateandTime.ToString("dd/MM/yyyy"));
                    html = html.Replace("@@Asunto", cita.Name);
                    html = html.Replace("@@Decripcion", cita.Description);

                    util.EnviarCorreo(usuario.Email, "Confirmación de Cita", html);

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}