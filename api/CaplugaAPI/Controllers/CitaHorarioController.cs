using CaplugaAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CaplugaAPI.Controllers
{
    public class CitaHorarioController : ApiController
    {


        [HttpGet]
        [Route("ConsultaHorarios")]
        public List<ScheduleAppointment> ConsultaHorarios()
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.ScheduleAppointment
                            select x).ToList();
                }
            }
            catch (Exception)
            {
                return new List<ScheduleAppointment>();
            }
        }

        [HttpGet]
        [Route("ConsultaHorario")]
        public ScheduleAppointment ConsultaHorario(long q)
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.ScheduleAppointment
                            where x.ScheduleID == q
                            select x).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost]
        [Route("RegistrarHorarios")]
        public string RegistrarHorarios(HorarioEnt entidad)
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    context.InsertScheduleAppointment(entidad.Dname, entidad.DateandTime);
                    return "OK";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }


        [HttpPut]
        [Route("ActualizarHorarios")]
        public string ActualizarHorarios(HorarioEnt entidad)
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    context.UpdateScheduleAppointment(entidad.ScheduleID, entidad.Dname, entidad.DateandTime);
                    return "OK";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        [HttpGet]
        [Route("verHorarios")]
        public List<System.Web.Mvc.SelectListItem> verHorarios()
        {
            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    var datos = context.ScheduleAppointment
                                        .Where(x => !x.IsBooked)
                                        .OrderBy(x => x.DateandTime)
                                        .ToList();
                    List<System.Web.Mvc.SelectListItem> horarios = new List<System.Web.Mvc.SelectListItem>();
                    foreach (var item in datos)
                    {
                        string displayText = $"{item.DateandTime.ToString("dddd", new System.Globalization.CultureInfo("es-ES"))}, {item.DateandTime.ToString("dd/MM/yyyy HH:mm")}";
                        horarios.Add(new System.Web.Mvc.SelectListItem
                        {
                            Value = item.ScheduleID.ToString(),
                            Text = displayText
                        });
                    }
                    return horarios;
                }
            }
            catch (Exception)
            {
                return new List<System.Web.Mvc.SelectListItem>();
            }
        }

    }
}