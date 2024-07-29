using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capluga.Entities
{
    public class AgendaEnt
    {
        public long AppointmentID { get; set; }
        public long UserID { get; set; }

        public long AddressID { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public virtual UbicacionEnt Ubicacion { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ScheduleID { get; set; }

        public string UserName { get; set; }
        public string Surnames { get; set; }
        public string Dname { get; set; }
        public DateTime DateandTime { get; set; }

    }
}