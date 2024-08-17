using Capluga.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;

namespace Capluga.Models
{
    public class ApiResponse
    {
        public string Message { get; set; }
    }

    public class MedicalCoursesModel
    {

        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];

        public long RegistrarCursos(CursoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "RegistrarCursos";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<long>().Result;
            }
        }


        public List<CursoEnt> ConsultaCursos()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaCursos";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<CursoEnt>>().Result;
            }
        }

        public CursoEnt ConsultaCurso(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaCurso?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<CursoEnt>().Result;
            }
        }

        public string ActualizarRutaCurso(CursoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarRutaCurso";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public string ActualizarCurso(CursoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarCurso";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                var result = res.Content.ReadFromJsonAsync<ApiResponse>().Result;
                return result?.Message;
            }
        }


        public string ActualizarEstadoCurso(CursoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarEstadoCurso";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }



    }
}