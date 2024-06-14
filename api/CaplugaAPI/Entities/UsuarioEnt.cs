using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaplugaAPI.Entities
{
    public class UsuarioEnt
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string Surnames { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool State { get; set; }
        public DateTime Age { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleID { get; set; }
        public string AddressID { get; set; }
        public virtual UbicacionEnt Ubicacion { get; set; }
    }
}
