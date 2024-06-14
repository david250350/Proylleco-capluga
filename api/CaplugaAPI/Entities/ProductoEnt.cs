using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaplugaAPI.Entities
{
    public class ProductoEnt
    {

        public long CartID { get; set; }
        public long UserID { get; set; }
        public long MedicalImplementsID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal State { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string Image { get; set; }


    }
}
