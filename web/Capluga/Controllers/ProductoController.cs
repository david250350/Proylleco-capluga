﻿using Capluga.Entities;
using Capluga.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capluga.Controllers
{
    public class ProductoController : Controller
    {

        ProductoModel modelProducto = new ProductoModel();


        [HttpGet]
        public ActionResult VistaProducto(int? page)
        {

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var datos = modelProducto.Productos();
            return View(datos.ToPagedList(pageNumber, pageSize));
        }



        [HttpGet]
        public ActionResult Productos()
        {
            var datos = modelProducto.Productos();
            if (datos == null || !datos.Any())
            {
                ViewBag.MensajeUsuario = "No se han encontrado productos.";
            }
            return View(datos);
        }

        [HttpGet]
        public ActionResult ProductosOrdenadosPorPrecio()
        {
            var datos = modelProducto.Productos().OrderBy(p => p.Price).ToList();
            return View("Productos", datos); // Reutiliza la vista "Productos"
        }

        [HttpGet]
        public ActionResult RegistrarProducto()
        {
            return View();
        }

        //esto hay que valorarlo 

        [HttpPost]
        public ActionResult RegistrarProducto(HttpPostedFileBase ImgProducto, ProductoEnt entidad)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            entidad.Image = string.Empty;
            entidad.State = true;

            long MedicalImplementsID = modelProducto.RegistrarProducto(entidad);

            if (MedicalImplementsID > 0)
            {
                if (ImgProducto != null)
                {

                    string extension = Path.GetExtension(Path.GetFileName(ImgProducto.FileName));
                    string ruta = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + MedicalImplementsID + extension;
                    ImgProducto.SaveAs(ruta);

                    entidad.Image = "/Images/" + MedicalImplementsID + extension;
                    entidad.MedicalImplementsID = MedicalImplementsID;

                    modelProducto.ActualizarRutaProducto(entidad);
                }
                return RedirectToAction("Productos", "Producto");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido registrar su producto";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarEstadoProducto(long q)
        {
            var entidad = new ProductoEnt();
            entidad.MedicalImplementsID = q;

            string respuesta = modelProducto.ActualizarEstadoProducto(entidad);

            if (respuesta == "OK")
            {
                return RedirectToAction("Productos", "Producto");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido cambiar el estado del producto";
                return View();
            }
        }

        [HttpGet]
        public ActionResult ActualizarProducto(long q)
        {
            var datos = modelProducto.Producto(q);
            return View(datos);
        }

        [HttpPost]
        public ActionResult ActualizarProducto(HttpPostedFileBase ImgProducto, ProductoEnt entidad)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            string respuesta = modelProducto.ActualizarProducto(entidad);

            if (respuesta == "OK")
            {
                if (ImgProducto != null)
                {
                    string extension = Path.GetExtension(Path.GetFileName(ImgProducto.FileName));
                    string ruta = AppDomain.CurrentDomain.BaseDirectory + "Images\\" + entidad.MedicalImplementsID + extension;
                    ImgProducto.SaveAs(ruta);

                    entidad.Image = "/Images/" + entidad.MedicalImplementsID + extension;
                    entidad.MedicalImplementsID = entidad.MedicalImplementsID;

                    modelProducto.ActualizarRutaProducto(entidad);
                }

                return RedirectToAction("Productos", "Producto");
            }
            else
            {
                ViewBag.MensajeUsuario = "No se ha podido actualizar la información del producto";
                return View();
            }
        }

        [HttpGet]
        public ActionResult DetallesProducto(long id)
        {
            var producto = modelProducto.Producto(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }



    }
}