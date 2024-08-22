using System;
using System.ComponentModel.DataAnnotations;

namespace Capluga.Entities
{
    public class UsuarioEnt
    {
        public long UserID { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        public string Surnames { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "La contraseña temporal es obligatoria.")]
        public string Temporary { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool State { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime Age { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        public string PhoneNumber { get; set; }

        public string RoleID { get; set; }
        public string AddressID { get; set; }
        public virtual UbicacionEnt Ubicacion { get; set; }
    }
}
