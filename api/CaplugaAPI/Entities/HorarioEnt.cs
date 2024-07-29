using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaplugaAPI.Entities
{
    public class HorarioEnt
    {
        public long ScheduleID { get; set; }
        public string Dname { get; set; }
        public DateTime DateandTime { get; set; }
        public bool IsBooked { get; set; }

    }
}