using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using Capluga.Entities;

namespace Capluga.Models
{
    public class RegisteredcoursesModel
    {

        public string rutaServidor = ConfigurationManager.AppSettings["RutaApi"];

        public string RegistrarInscripcion(InscripCursEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "RegistrarInscripcion";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }

        public List<InscripCursEnt> ConsultarInscripcion(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultarInscripcion?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<InscripCursEnt>>().Result;
            }
        }

        public List<FacturaCursoEnt> ConsultaFacturasCurso(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaFacturasCurso?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<FacturaCursoEnt>>().Result;
            }
        }

        public List<FacturaCursoEnt> ConsultaDetalleFacturaCurso(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ConsultaDetalleFacturaCurso?q=" + q;
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<FacturaCursoEnt>>().Result;
            }
        }

        public void EliminarRegistroCurso(long q)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "EliminarRegistroCurso?q=" + q;
                var res = client.DeleteAsync(urlApi).Result;
            }
        }

        public int Matricula(InscripCursEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "Matricula";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PostAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<int>().Result;
            }
        }

    
        public string ActualizarEstadoCurso(FacturaCursoEnt entidad)
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "ActualizarEstadoCurso";
                var jsonData = JsonContent.Create(entidad);
                var res = client.PutAsync(urlApi, jsonData).Result;
                return res.Content.ReadFromJsonAsync<string>().Result;
            }
        }


        public List<FacturaCursoEnt> EstadoCurso()
        {
            using (var client = new HttpClient())
            {
                var urlApi = rutaServidor + "EstadoCurso";
                var res = client.GetAsync(urlApi).Result;
                return res.Content.ReadFromJsonAsync<List<FacturaCursoEnt>>().Result; // Leer como lista de FacturaCursoEnt
            }
        }

    }
}