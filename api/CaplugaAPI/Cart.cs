//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CaplugaAPI
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cart
    {
        public long CartID { get; set; }
        public long MedicalImplementsID { get; set; }
        public int Quantity { get; set; }
        public long UserID { get; set; }
    
        public virtual MedicalImplements MedicalImplements { get; set; }
        public virtual Users Users { get; set; }
    }
}
