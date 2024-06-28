using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capluga.Entities
{
    public class CarritoEnt
    {
        public long CartID { get; set; }
        public long UserID { get; set; }
        public long MedicalImplementsID { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Impuestos { get; set; }
        public decimal Total { get; set; }
    }
}