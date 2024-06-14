using Capluga.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;

namespace Capluga.Models
{
    public class RolesModel
    {
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];

        public long CrearRol(RoleEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "CrearRol";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<long>().Result;
            }
        }


        public List<RoleEnt> ListaRoles()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ListaRoles";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<RoleEnt>>().Result;
            }
        }

        public RoleEnt Rol(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "Rol?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<RoleEnt>().Result;

            }
        }

        public string ActualizarRol(RoleEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarRol";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }
    }
}
