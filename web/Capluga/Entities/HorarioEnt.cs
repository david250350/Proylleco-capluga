using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capluga.Entities
{
    public class HorarioEnt
    {

        public long ScheduleID { get; set; }
        [Required(ErrorMessage = "El nombre del día es obligatorio.")]
        public string Dname { get; set; }

        [Required(ErrorMessage = "La fecha y hora son obligatorias.")]
        [DataType(DataType.DateTime)]
        public DateTime DateandTime { get; set; }

    }
}
