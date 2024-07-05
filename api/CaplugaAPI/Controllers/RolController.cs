using System;
using System.Linq;
using System.Web.Http;
using System.IO;
using System.Collections.Generic;
using CaplugaAPI.Entities;
using System.Security.Cryptography;
using System.Data.SqlClient;
using System.Web.Security;


namespace CaplugaAPI.Controllers
{
    public class RolController : ApiController
    {

        [HttpPost]
        [Route("CrearRol")]
        public string CrearRol(RoleEnt entidad)
        {

            try
            {
                using (var context = new CAPLUGAEntities())
                {
                    context.InsertRol(entidad.RolName);
                    return "OK";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        [HttpGet]
        [Route("ListaRoles")]
        public List<RoleEnt> ListaRoles()
        {

            using (var context = new CAPLUGAEntities())
            {
                var datos = (from x in context.Roles
                             select x).ToList();

                List<RoleEnt> listaEntidadResultado = new List<RoleEnt>();
                foreach (var item in datos)
                {
                    listaEntidadResultado.Add(new RoleEnt
                    {
                        RoleID = item.RolesID,
                        RolName = item.RoleName
                    });
                }

                return listaEntidadResultado;
            }
        }

        [HttpGet]
        [Route("Rol")]
        public IHttpActionResult Rol(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                var rol = (from x in context.Roles
                           where x.RolesID == q
                           select new RoleEnt
                           {
                               RoleID = x.RolesID,
                               RolName = x.RoleName
                           }).FirstOrDefault();

                if (rol == null)
                {
                    return NotFound();
                }

                return Ok(rol);
            }
        }




        [HttpPut]
        [Route("ActualizarRol")]
        public string ActualizarRol(RoleEnt entidad)
        {
            try
            {

                using (var context = new CAPLUGAEntities())
                {
                    context.UpdateRol(entidad.RoleID, entidad.RolName);
                    return "OK";
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        [HttpDelete]
        [Route("EliminarRol")]
        public void EliminarRol(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                var datos = (from x in context.Roles
                             where x.RolesID == q
                             select x).FirstOrDefault();

                context.Roles.Remove(datos);
                context.SaveChanges();
            }
        }
    }
}