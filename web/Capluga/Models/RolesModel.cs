using Capluga.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web.Mvc;

namespace Capluga.Models
{
    public class RolesModel
    {
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];

        public string CrearRol(RoleEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "CrearRol";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;

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

                if (res.IsSuccessStatusCode)
                {
                    return res.Content.ReadFromJsonAsync<RoleEnt>().Result;
                }
                else
                {
                    var errorContent = res.Content.ReadAsStringAsync().Result;
                    throw new Exception($"No se pudo obtener el rol: {errorContent}");
                }
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

        public void EliminarRol(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "EliminarRol?q=" + q;
                var res = client.DeleteAsync(urlApi).Result;
            }
        }

    }
}