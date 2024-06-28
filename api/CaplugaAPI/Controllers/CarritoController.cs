using CaplugaAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CaplugaAPI.Controllers
{
    public class CarritoController : ApiController
    {

        [HttpPost]
        [Route("RegistrarCarrito")]
        public string RegistrarCarrito(Cart cart)
        {
            using (var context = new CAPLUGAEntities())
            {
                var datos = (from x in context.Cart
                             where x.UserID == cart.CartID
                                && x.MedicalImplementsID == cart.MedicalImplementsID
                             select x).FirstOrDefault();

                if (datos == null)
                {
                    context.Cart.Add(cart);
                    context.SaveChanges();
                }
                else
                {
                    datos.Quantity = cart.Quantity;
                    context.SaveChanges();
                }
                return "OK";
            }
        }

        [HttpGet]
        [Route("ConsultarCarrito")]
        public object ConsultarCarrito(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from x in context.Cart
                        join y in context.MedicalImplements on x.MedicalImplementsID equals y.MedicalImplementsID
                        where x.UserID == q
                        select new
                        {
                            x.CartID,
                            x.UserID,
                            x.MedicalImplementsID,
                            x.Quantity,
                            y.Name,
                            y.Price,
                            SubTotal = (y.Price * x.Quantity),
                            Impuestos = (y.Price * x.Quantity) * 0.13M,
                            Total = (y.Price * x.Quantity) + (y.Price * x.Quantity) * 0.13M
                        }).ToList();
            }
        }

        [HttpGet]
        [Route("ConsultaCarritoadm")]
        public List<Detail> ConsultaCarritoadm()
        {
            try
            {

                using (var context = new CAPLUGAEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.Detail
                            select x).ToList();
                }
            }
            catch (Exception)
            {
                return new List<Detail>();
            }
        }





        [HttpPost]
        [Route("PagarCarrito")]
        public int PagarCarrito(Cart cart)
        {
            using (var context = new CAPLUGAEntities())
            {
                return context.PayCart(cart.UserID);
            }
        }

        [HttpDelete]
        [Route("EliminarRegistroCarrito")]
        public void EliminarRegistroCarrito(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                var datos = (from x in context.Cart
                             where x.CartID == q
                             select x).FirstOrDefault();

                context.Cart.Remove(datos);
                context.SaveChanges();
            }
        }

        [HttpPut]
        [Route("ActualizarEstadoPago")]
        public string ActualizarEstadoPago(FacturaEnt entidad)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.ApprovePaymentDetails(entidad.MasterPurchaseID);

                return "OK";
            }

        }
    }
}