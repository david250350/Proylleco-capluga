using Capluga.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Web.Mvc;

namespace Capluga.Models
{
    public class UsuarioModel
    {
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];
        //---API LoginController
        public string RegistrarCuenta(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "RegistrarCuenta";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }
        public UsuarioEnt IniciarSesion(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "IniciarSesion";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<UsuarioEnt>().Result;
            }
        }

        public string RecuperarCuenta(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "RecuperarCuenta?Email=" + entidad.Email;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }
        //------------------------------------------------------
        //---API UsuarioController
        public List<SelectListItem> ConsultarProvincias()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultarProvincias";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        public List<UsuarioEnt> ConsultaUsuarios()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaUsuarios";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<UsuarioEnt>>().Result;
            }
        }

        public UsuarioEnt ConsultaUsuario(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaUsuario?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<UsuarioEnt>().Result;
            }
        }

        public string ActualizarCuenta(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarCuenta";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public string ActualizarEstadoUsuario(UsuarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarEstadoUsuario";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public List<SelectListItem> ListaRoles()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ListaRoles";

                HttpResponseMessage res = client.GetAsync(urlApi).GetAwaiter().GetResult();

                if (res.IsSuccessStatusCode)
                {
                    var ListaRoles = res.Content.ReadFromJsonAsync<List<RoleEnt>>().Result;

                    List<SelectListItem> listaCombo = new List<SelectListItem>();
                    foreach (var item in ListaRoles)
                    {
                        listaCombo.Add(new SelectListItem
                        {
                            Text = item.RolName,
                            Value = item.RoleID.ToString()
                        });
                    }

                    return listaCombo;

                }

                return new List<SelectListItem>();
            }
        }
        //------------------------------------------------------
    }
}