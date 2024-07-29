using Capluga.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Capluga.Models
{
    public class AgendaCitaModel
    {
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];

        public async Task<string> RegistrarCita(AgendaEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "RegistrarCita";
                var jsonData = JsonContent.Create(entidad);
                var res = await client.PostAsync(urlApi, jsonData);

                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadFromJsonAsync<string>();
                }
                else
                {
                    // Manejo adecuado de errores HTTP
                    return $"Error: {res.StatusCode}";
                }
            }
        }

        public List<AgendaEnt> ConsultaCitas()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaCitas";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<AgendaEnt>>().Result;
            }
        }

        public List<AgendaEnt> ConsultaCita(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaCita?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<AgendaEnt>>().Result;
            }
        }

        public void EliminarRegistroCita(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "EliminarRegistroCita?q=" + q;
                var res = client.DeleteAsync(urlApi).Result;
            }
        }
        public string Actualizarcita(AgendaEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "Actualizarcita";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

    }
}