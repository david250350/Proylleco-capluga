using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capluga.Entities
{
    public class InscripCursEnt
    {
        public long RegisteredcoursesID { get; set; }
        public long UserID { get; set; }
        public long MedicalCourseID { get; set; }
        public int Quantity { get; set; }
        public DateTime Registrationdate { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public DateTime DateandTime { get; set; }
    }
}