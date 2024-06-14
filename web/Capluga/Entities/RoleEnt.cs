using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capluga.Entities
{
    public class RoleEnt
    {

        public long RoleID { get; set; }
        public string RolName { get; set; }
        public static implicit operator RoleEnt(int v)
        {
            throw new NotImplementedException();
        }
    }
}
