using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capluga.Entities
{
    public class RoleEnt
    {

        public long RolesID { get; set; }


        [Required(ErrorMessage = "El nombre del rol es obligatorio.")]
        [StringLength(20, ErrorMessage = "El nombre del rol no puede tener más de 20 caracteres.")]
        public string RoleName { get; set; }

        public static implicit operator RoleEnt(int v)
        {
            throw new NotImplementedException();
        }
    }
}
