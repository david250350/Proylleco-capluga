using CaplugaAPI.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CaplugaAPI.Controllers
{
    public class CarritoController : ApiController
    {

        Utilitarios util = new Utilitarios();

        [HttpPost]
        [Route("RegistrarCarrito")]
        public string RegistrarCarrito(Cart cart)
        {
            using (var context = new CAPLUGAEntities())
            {
                var datos = (from x in context.Cart
                             where x.UserID == cart.UserID
                                && x.MedicalImplementsID == cart.MedicalImplementsID
                             select x).FirstOrDefault();

                if (datos == null)
                {
                    Cart registro = new Cart();
                    registro.UserID = cart.UserID;
                    registro.MedicalImplementsID = cart.MedicalImplementsID;
                    registro.Quantity = cart.Quantity;
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
        public List<FacturaEnt> ConsultaCarritoadm()
        {
            try
            {

                using (var context = new CAPLUGAEntities())
                {
                    context.Configuration.LazyLoadingEnabled = false;
                    return (from x in context.Detail
                            join p in context.MedicalImplements on x.MedicalImplementsID equals p.MedicalImplementsID
                            join z in context.MasterPurchase on x.MasterPurchaseID equals z.MasterPurchaseID 
                            join u in context.Users on z.UserID equals u.UserID
                            
                            select new FacturaEnt
                            { 
                            DetailID = x.DetailID,
                            MasterPurchaseID = x.MasterPurchaseID,
                           MedicalImplementsID = x.MedicalImplementsID,
                           PaymentStatus = x.PaymentStatus,
                           UserID = z.UserID,
                           Name = p.Name,
                           UserName = u.UserName,
                           Surnames = u.Surnames
                            
                            }).ToList();
                }
            }
            catch (Exception)
            {
                return new List<FacturaEnt>();
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

  


        [HttpPost]
        [Route("PagarCarrito")]
        public int PagarCarrito(Cart cart)
        {
            using (var context = new CAPLUGAEntities())
            {
                return context.PayCart(userID: cart.UserID);
            }
        }


        [HttpPut]
        [Route("ActualizarEstadoPago")]
        public string ActualizarEstadoPago(FacturaEnt entidad)
        {
            using (var context = new CAPLUGAEntities())
            {
                DocumentController documentController = new DocumentController();
                var factura = context.MasterPurchase.FirstOrDefault(u => u.MasterPurchaseID == entidad.MasterPurchaseID);
                List<Detail> detalles = (from x in context.Detail
                                         where x.MasterPurchaseID == entidad.MasterPurchaseID
                                         select x).ToList();
                var usuario = context.Users.FirstOrDefault(u => u.UserID == factura.UserID);

                byte[] pdf = documentController.ProductoPDF(factura, detalles, usuario);

                string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "Templates\\Facturacion.html";
                string html = File.ReadAllText(rutaArchivo);
                html = html.Replace("@@Nombre", usuario.UserName);
                html = html.Replace("@@Total", factura.TotalPurchase.ToString());

                util.EnviarCorreo(usuario.Email, "Factura de compra CAPLUGA", html, pdf, "CAPLUGA_Factura_" + factura.MasterPurchaseID + ".pdf");

                context.ApprovePaymentDetails(entidad.MasterPurchaseID);

                return "OK";
            }
        }

        [HttpGet]
        [Route("ConsultaFacturas")]
        public List<MasterPurchase> ConsultaFacturas(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from x in context.MasterPurchase
                        where x.UserID == q
                        select x).ToList();
            }
        }

        [HttpGet]
        [Route("ConsultaDetalleFactura")]
        public object ConsultaDetalleFactura(long q)
        {
            using (var context = new CAPLUGAEntities())
            {
                context.Configuration.LazyLoadingEnabled = false;
                return (from x in context.Detail
                        join y in context.MedicalImplements on x.MedicalImplementsID equals y.MedicalImplementsID
                        join z in context.MasterPurchase on x.MasterPurchaseID equals z.MasterPurchaseID
                        join c in context.Users on z.UserID equals c.UserID
                        join a in context.Addresses on c.AddressID equals a.AddressID
                        where x.MasterPurchaseID == q
                        select new
                        {
                            x.MasterPurchaseID,
                            y.Name,
                            x.PaidPrice,
                            x.PaidQuantity,
                            x.Tax,
                            x.PaymentStatus,
                            z.Users.UserName,
                            z.Users.Surnames,
                            x.MasterPurchase.PurchaseDate,
                            SubTotal = (x.PaidPrice * x.PaidQuantity),
                            Impuesto = (x.Tax * x.PaidQuantity),
                            Total = (x.PaidPrice * x.PaidQuantity) + (x.Tax * x.PaidQuantity)

                        }).ToList();
            }
        }

    }
}