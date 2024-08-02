using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capluga.Entities
{
    public class FacturaEnt
    {


        public long MasterPurchaseID { get; set; }
        public long DetailID { get; set; }
        public long UserID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalPurchase { get; set; }

        public string PaymentStatus { get; set; }
        public string UserName { get; set; }
        public string Surnames { get; set; }

        public string Name { get; set; }
        public decimal PaidPrice { get; set; }
        public int PaidQuantity { get; set; }
        public decimal Tax { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Impuesto { get; set; }

        public decimal Total { get; set; }
        public long MedicalImplementsID { get; set; }

    }
}