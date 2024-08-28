﻿using CaplugaAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CaplugaAPI.Controllers
{
    public class UsuarioController : ApiController
    {

        [HttpGet]
        [Route("ConsultaUsuarios")]
        public List<UsuarioEnt> ConsultaUsuarios()
        {
            using (var conexion = new CAPLUGAEntities())
            {
                var datos = (from x in conexion.Users
                             select x).ToList();

                List<UsuarioEnt> listaEntidadResultado = new List<UsuarioEnt>();
                foreach (var item in datos)
                {
                    listaEntidadResultado.Add(new UsuarioEnt
                    {
                        UserID = item.UserID,
                        Email = item.Email,
                        State = item.State,
                        UserName = item.UserName,
                        Surnames = item.Surnames,
                        Age = item.Age,
                        PhoneNumber = item.PhoneNumber,
                        RolesID = item.RolesID.ToString(),
                        AddressID = item.AddressID.ToString()
                    });
                }
                return listaEntidadResultado;
            }
        }

        [HttpGet]
        [Route("ConsultaUsuario")]
        public UsuarioEnt ConsultaUsuario(long q)
        {
            using (var conexion = new CAPLUGAEntities())
            {
                var entidad = (from x in conexion.Users
                             where x.UserID == q
                             select x).FirstOrDefault();

                var ubicacion = (from x in conexion.Addresses
                                 where x.AddressID == entidad.AddressID
                                 select x).FirstOrDefault();

                UbicacionEnt ubicacionEnt = new UbicacionEnt();
                ubicacionEnt.AddressID = ubicacion.AddressID;
                ubicacionEnt.Country = ubicacion.Country;
                ubicacionEnt.State = ubicacion.State;
                ubicacionEnt.City = ubicacion.City;
                ubicacionEnt.District = ubicacion.District;
                ubicacionEnt.ZipCode = ubicacion.ZipCode;
                ubicacionEnt.Street = ubicacion.Street;

                UsuarioEnt usuario = new UsuarioEnt();
                usuario.UserID = q;
                usuario.UserName = entidad.UserName;
                usuario.Surnames = entidad.Surnames;
                usuario.Email = entidad.Email;
                usuario.Password = entidad.Password;
                usuario.State = entidad.State;
                usuario.Age = entidad.Age;
                usuario.PhoneNumber = entidad.PhoneNumber;
                usuario.RolesID = entidad.RolesID.ToString();
                usuario.AddressID = entidad.AddressID.ToString();
                usuario.Ubicacion = ubicacionEnt;

                return usuario;
            }
        }

        [HttpPut]
[Route("ActualizarCuenta")]
public string ActualizarCuenta(UsuarioEnt entidad)
{
    try
    {
        using (var conexion = new CAPLUGAEntities())
        {
            var usuario = (from x in conexion.Users
                           where x.UserID == entidad.UserID
                           select x).FirstOrDefault();

            if (usuario == null)
            {
                return "Usuario no encontrado";
            }

            usuario.Email = entidad.Email;
            // Remove this line to prevent changing the state
            // usuario.State = entidad.State;
            usuario.UserName = entidad.UserName;
            usuario.Surnames = entidad.Surnames;
            usuario.Age = entidad.Age;
            usuario.RolesID = Int64.Parse(entidad.RolesID);
            usuario.PhoneNumber = entidad.PhoneNumber;

            if (!string.IsNullOrEmpty(entidad.Password))
            {
                usuario.Password = entidad.Password;
            }

            var direccion = (from x in conexion.Addresses
                             where x.AddressID == usuario.AddressID
                             select x).FirstOrDefault();

            if (direccion == null)
            {
                direccion = new Addresses();
                usuario.AddressID = direccion.AddressID;
            }

            direccion.State = entidad.Ubicacion.State;
            direccion.City = entidad.Ubicacion.City;
            direccion.District = entidad.Ubicacion.District;
            direccion.ZipCode = entidad.Ubicacion.ZipCode;
            direccion.Street = entidad.Ubicacion.Street;

            conexion.SaveChanges();
            return "OK";
        }
    }
    catch (Exception ex)
    {
        return "Error: " + ex.Message;
    }
}

        [HttpPut]
        [Route("ActualizarEstadoUsuario")]
        public string ActualizarEstadoUsuario(UsuarioEnt entidad)
        {
            using (var conexion = new CAPLUGAEntities())
            {
                var usuario = (from x in conexion.Users
                             where x.UserID == entidad.UserID
                             select x).FirstOrDefault();

                usuario.State = (usuario.State == true ? false : true);
                conexion.SaveChanges();
                return "OK";
            }
        }

    }
}