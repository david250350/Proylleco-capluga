using Capluga.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Capluga.Models
{
    public class CitaHorarioModel
    {
        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];

        public List<SelectListItem> verHorarios()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "verHorarios";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<SelectListItem>>().Result;
            }
        }

        public string RegistrarHorarios(HorarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "RegistrarHorarios";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }
        public List<HorarioEnt> ConsultaHorarios()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaHorarios";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<HorarioEnt>>().Result;
            }
        }

        public HorarioEnt ConsultaHorario(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaHorario?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<HorarioEnt>().Result;
            }
        }

        public string ActualizarHorarios(HorarioEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarHorarios";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }
    }
}