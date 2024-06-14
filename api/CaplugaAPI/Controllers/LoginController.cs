using System;
using System.Linq;
using System.Web.Http;
using System.IO;
using System.Collections.Generic;
using CaplugaAPI.Entities;
using System.Security.Cryptography;
using System.Data.SqlClient;


namespace CaplugaAPI.Controllers
{
    public class LoginController : ApiController
    {


        [HttpPost]
        [Route("RegistrarCuenta")]
        public string RegistrarCuenta(UsuarioEnt entidad)
        {
            using (var context = new CAPLUGAEntities())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        // Registro de la ubicación y se retorna el id de direción
                        var addressId = RegistrarUbicacion(context, entidad);

                        // Registro del usuario, se valida el id de direccion y se pasa por parametro
                        if (addressId > 0 && RegistrarUsuario(context, entidad, addressId))
                        {
                            dbContextTransaction.Commit();
                            return "OK";
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                    catch (Exception)
                    {
                        return string.Empty;
                    }
                }
            }
        }
        //Se registra la dirección
        private int RegistrarUbicacion(CAPLUGAEntities context, UsuarioEnt entidad)
        {
            Addresses addresses = new Addresses
            {
                Country = "Costa Rica",
                State = entidad.Ubicacion.State,
                City = entidad.Ubicacion.City,
                District = entidad.Ubicacion.District,
                Street = entidad.Ubicacion.Street,
                ZipCode = entidad.Ubicacion.ZipCode
            };

            context.Addresses.Add(addresses);
            context.SaveChanges();

            return (int)context.Addresses.OrderByDescending(a => a.AddressID).FirstOrDefault().AddressID;
        }

        //Se registra el usuario y se pone el id de la dirreción para hacer el ForengKey
        private bool RegistrarUsuario(CAPLUGAEntities context, UsuarioEnt entidad, int addressId)
        {
            Users user = new Users
            {
                UserName = entidad.UserName,
                Surnames = entidad.Surnames,
                Email = entidad.Email,
                Password = entidad.Password,
                RegistrationDate = DateTime.Now,
                State = true,
                Age = entidad.Age,
                PhoneNumber = entidad.PhoneNumber,
                RolesID = 2,
                AddressID = addressId
            };

            context.Users.Add(user);
            context.SaveChanges();

            return true;
        }

        [HttpPost]
        [Route("IniciarSesion")]
        public UsuarioEnt IniciarSesion(UsuarioEnt entidad)
        {
            using (var context = new CAPLUGAEntities())
            {
                var user = (from x in context.Users
                            where x.Email == entidad.Email
                            && x.Password == entidad.Password
                            && x.State
                            select x).FirstOrDefault();
                if (user != null)
                {
                    return usuarioRegistrado(user);
                }
                else
                {
                    return null;
                }

            }
        }

        private UsuarioEnt usuarioRegistrado(Users user)
        {
            UsuarioEnt usuario = new UsuarioEnt();
            usuario.UserID = user.UserID;
            usuario.UserName = user.UserName;
            usuario.Surnames = user.Surnames;
            usuario.Email = user.Email;
            usuario.Password = user.Password;
            usuario.RegistrationDate = user.RegistrationDate;
            usuario.State = user.State;
            usuario.Age = user.Age;
            usuario.PhoneNumber = user.PhoneNumber;
            usuario.RoleID = user.RolesID.ToString();
            usuario.AddressID = user.AddressID.ToString();

            return usuario;
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
        public Roles Rol(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from x in context.Roles
                        where x.RolesID == q
                        select x).FirstOrDefault();
            }
        }

        [HttpPost]
        [Route("CrearRol")]
        public long CrearRol(Roles roles)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Roles.Add(roles);
                context.SaveChanges();
                return roles.RolesID;

            }
        }

            [HttpPut]
            [Route("ActualizarRol")]
            public string ActualizarRol(Roles roles)
            {
                using (var context = new CAPLUGAEntities())
                {
                    var datos = context.Roles.Where(x => x.RolesID == roles.RolesID).FirstOrDefault();
                    datos.RoleName = roles.RoleName;      
                    context.SaveChanges();
                    return "OK";
                }
            }


       

    }
}