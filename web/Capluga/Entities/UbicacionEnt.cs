using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capluga.Entities
{
    public class UbicacionEnt
    {
        public long AddressID { get; set; }

        [Required(ErrorMessage = "El país es obligatorio.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "La provincia es obligatoria.")]
        public string State { get; set; }

        [Required(ErrorMessage = "El cantón es obligatorio.")]
        public string City { get; set; }

        [Required(ErrorMessage = "El distrito es obligatorio.")]
        public string District { get; set; }

        [Required(ErrorMessage = "La dirección exacta es obligatoria.")]
        [StringLength(100, ErrorMessage = "La dirección exacta no puede tener más de 100 caracteres.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "El código postal es obligatorio.")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "El código postal debe ser un número de 5 dígitos.")]
        public string ZipCode { get; set; }
    }

}
