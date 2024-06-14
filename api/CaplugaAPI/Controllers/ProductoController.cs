using CaplugaAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CaplugaAPI.Controllers
{
    public class ProductoController : ApiController
    {

        [HttpGet]
        [Route("Productos")]
        public List<MedicalImplements> Productos()
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return context.MedicalImplements.ToList();
            }
        }

        [HttpGet]
        [Route("Producto")]
        public MedicalImplements Producto(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from x in context.MedicalImplements
                        where x.MedicalImplementsID == q
                        select x).FirstOrDefault();
            }
        }

        [HttpPost]
        [Route("RegistrarProducto")]
        public long RegistrarProducto(MedicalImplements medicalImplements)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.MedicalImplements.Add(medicalImplements);
                context.SaveChanges();
                return medicalImplements.MedicalImplementsID;
            }
        }

        [HttpPut]
        [Route("ActualizarRutaProducto")]
        public string ActualizarRutaProducto(MedicalImplements medicalImplements)
        {
            using (var context = new CAPLUGAEntities())
            {
                var datos = context.MedicalImplements.Where(x => x.MedicalImplementsID == medicalImplements.MedicalImplementsID).FirstOrDefault();
                datos.Image = medicalImplements.Image;
                context.SaveChanges();
                return "OK";
            }
        }

        [HttpPut]
        [Route("ActualizarProducto")]
        public string ActualizarProducto(MedicalImplements medicalImplements)
        {
            using (var context = new CAPLUGAEntities())
            {
                var datos = context.MedicalImplements.Where(x => x.MedicalImplementsID == medicalImplements.MedicalImplementsID).FirstOrDefault();
                datos.Name = medicalImplements.Name;
                datos.Description = medicalImplements.Description;
                datos.Quantity = medicalImplements.Quantity;
                datos.Price = medicalImplements.Price;
                context.SaveChanges();
                return "OK";
            }
        }



        [HttpPut]
        [Route("ActualizarEstadoProducto")]
        public string ActualizarEstadoProducto(MedicalImplements medicalImplements)
        {
            using (var context = new CAPLUGAEntities())
            {
                var datos = context.MedicalImplements.Where(x => x.MedicalImplementsID == medicalImplements.MedicalImplementsID).FirstOrDefault();
                datos.State = (datos.State ? false : true);
                context.SaveChanges();
                return "OK";
            }
        }
    }
}