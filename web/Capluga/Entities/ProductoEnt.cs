using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capluga.Entities
{
    public class ProductoEnt
    {
    public long UserID { get; set; }
    public long MedicalImplementsID { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "El estado es obligatorio.")]
    public bool State { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un número positivo.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "La cantidad es obligatoria.")]
    [Range(0, double.MaxValue, ErrorMessage = "La cantidad debe ser un número positivo.")]
    public decimal Quantity { get; set; }

    public string Image { get; set; }
}
}