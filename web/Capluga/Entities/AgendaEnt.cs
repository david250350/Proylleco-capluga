using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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


        [Required(ErrorMessage = "El asunto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El asunto no puede tener más de 100 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres.")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Debe seleccionar un horario.")]
        public long ScheduleID { get; set; }

        public string UserName { get; set; }
        public string Surnames { get; set; }
        public string Dname { get; set; }
        public DateTime DateandTime { get; set; }

    }
}