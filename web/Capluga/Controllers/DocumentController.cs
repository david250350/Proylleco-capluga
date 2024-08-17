using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using ClosedXML.Excel;
using System.Data;
using Capluga.Entities;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.Ajax.Utilities;
using Capluga.Models;
using iTextSharp.text.pdf.draw;

namespace Capluga.Controllers
{
    public class DocumentController : Controller
    {

        ProductoModel modelProducto = new ProductoModel();
        MedicalCoursesModel modelCurso = new MedicalCoursesModel();
        AgendaCitaModel modelAgenda = new AgendaCitaModel();
        CarritoModel modelCarrito = new CarritoModel();
        RegisteredcoursesModel modelRegid = new RegisteredcoursesModel();

        [HttpPost]
        public ActionResult ProductoPDF()
        {
            // Suponemos que esta llamada obtiene los datos necesarios
            List<ProductoEnt> productos = modelProducto.Productos();

            if (productos == null || !productos.Any())
            {
                return Content("No se encontraron datos para generar la factura.");
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document documento = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(documento, memoryStream);
                documento.Open();

                // Definir fuentes
                Font fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
                Font fontSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
                Font fontCuerpo = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);

                // Agregar un logo
                string imageURL = Server.MapPath("~/Images/Logo.png"); // Asegúrate de tener esta imagen en tu proyecto
                Image logo = Image.GetInstance(imageURL);
                logo.ScaleToFit(50f, 50f);
                logo.SpacingBefore = 10;
                logo.SpacingAfter = 10;
                logo.Alignment = Element.ALIGN_RIGHT;
                documento.Add(logo);

                // Título del documento
                documento.Add(new Paragraph("Lista de Productos", fontTitulo));

                // Línea separadora
                LineSeparator separator = new LineSeparator(1f, 100f, BaseColor.DARK_GRAY, Element.ALIGN_CENTER, -1);
                documento.Add(new Chunk(separator));

                // Datos del documento
                documento.Add(new Paragraph($"Fecha: {DateTime.Now.ToString("dd/MM/yyyy")}", fontCuerpo));
                documento.Add(new Paragraph("\n"));

                // Crear una tabla para los detalles de los productos
                PdfPTable table = new PdfPTable(new float[] { 3, 3, 1, 2 }) { WidthPercentage = 100 };
                table.DefaultCell.Padding = 5;

                // Definir el estilo del encabezado de la tabla
                BaseColor colorEncabezado = new BaseColor(0, 121, 182); // Un azul oscuro
                Font fontEncabezado = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);

                // Añadir los encabezados de la tabla
                PdfPCell cell = new PdfPCell(new Phrase("Nombre del Producto", fontEncabezado)) { BackgroundColor = colorEncabezado };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Descripción", fontEncabezado)) { BackgroundColor = colorEncabezado };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Cantidad", fontEncabezado)) { BackgroundColor = colorEncabezado };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Precio", fontEncabezado)) { BackgroundColor = colorEncabezado };
                table.AddCell(cell);

                // Rellenar la tabla con los detalles de los productos
                foreach (var item in productos)
                {
                    table.AddCell(new Phrase(item.Name, fontCuerpo));
                    table.AddCell(new Phrase(item.Description, fontCuerpo));
                    table.AddCell(new Phrase(item.Quantity.ToString(), fontCuerpo));
                    table.AddCell(new Phrase(item.Price.ToString("C", new System.Globalization.CultureInfo("es-CR")), fontCuerpo));
                }

                // Añadir la tabla al documento
                documento.Add(table);

                // Total general de los productos
                documento.Add(new Paragraph("Total: " + productos.Sum(x => x.Price * x.Quantity).ToString("C", new System.Globalization.CultureInfo("es-CR")), fontTitulo));

                documento.Close();

                byte[] bytes = memoryStream.ToArray();
                return File(bytes, "application/pdf", "Lista_Productos.pdf");
            }
        }

        [HttpPost]
        public ActionResult CursoPDF()
        {
            // Suponemos que esta llamada obtiene los datos necesarios
            List<CursoEnt> cursos = modelCurso.ConsultaCursos();

            if (cursos == null || !cursos.Any())
            {
                return Content("No se encontraron datos para generar el informe de cursos.");
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document documento = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(documento, memoryStream);
                documento.Open();

                // Definir fuentes
                Font fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
                Font fontSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
                Font fontCuerpo = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);

                // Agregar un logo
                string imageURL = Server.MapPath("~/Images/Logo.png"); // Asegúrate de tener esta imagen en tu proyecto
                Image logo = Image.GetInstance(imageURL);
                logo.ScaleToFit(50f, 50f);
                logo.SpacingBefore = 10;
                logo.SpacingAfter = 10;
                logo.Alignment = Element.ALIGN_RIGHT;
                documento.Add(logo);

                // Título del documento
                documento.Add(new Paragraph("Lista de Cursos", fontTitulo));

                // Línea separadora
                LineSeparator separator = new LineSeparator(1f, 100f, BaseColor.DARK_GRAY, Element.ALIGN_CENTER, -1);
                documento.Add(new Chunk(separator));

                // Datos del documento
                documento.Add(new Paragraph($"Fecha: {DateTime.Now.ToString("dd/MM/yyyy")}", fontCuerpo));
                documento.Add(new Paragraph("\n"));

                // Crear una tabla para los detalles de los cursos
                PdfPTable table = new PdfPTable(new float[] { 3, 3, 2, 1, 2, 1}) { WidthPercentage = 100 };
                table.DefaultCell.Padding = 5;

                // Definir el estilo del encabezado de la tabla
                BaseColor colorEncabezado = new BaseColor(0, 121, 182); // Un azul oscuro
                Font fontEncabezado = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);

                // Añadir los encabezados de la tabla
                table.AddCell(new PdfPCell(new Phrase("Nombre del Curso", fontEncabezado)) { BackgroundColor = colorEncabezado });
                table.AddCell(new PdfPCell(new Phrase("Descripción", fontEncabezado)) { BackgroundColor = colorEncabezado });
                table.AddCell(new PdfPCell(new Phrase("Fecha y Hora", fontEncabezado)) { BackgroundColor = colorEncabezado });
                table.AddCell(new PdfPCell(new Phrase("Cantidad", fontEncabezado)) { BackgroundColor = colorEncabezado });
                table.AddCell(new PdfPCell(new Phrase("Precio", fontEncabezado)) { BackgroundColor = colorEncabezado });
                table.AddCell(new PdfPCell(new Phrase("Estado", fontEncabezado)) { BackgroundColor = colorEncabezado });
               

                // Rellenar la tabla con los detalles de los cursos
                foreach (var item in cursos)
                {
                    table.AddCell(new Phrase(item.Name, fontCuerpo));
                    table.AddCell(new Phrase(item.Description, fontCuerpo));
                    table.AddCell(new Phrase(item.DateandTime.ToString("dd/MM/yyyy HH:mm"), fontCuerpo));
                    table.AddCell(new Phrase(item.Quantity.ToString(), fontCuerpo));
                    table.AddCell(new Phrase(item.Price.ToString("C", new System.Globalization.CultureInfo("es-CR")), fontCuerpo));
                    table.AddCell(new Phrase(item.State ? "Activo" : "Inactivo", fontCuerpo));
                   
                }

                // Añadir la tabla al documento
                documento.Add(table);

                // Total general de los precios de los cursos
                documento.Add(new Paragraph("Total: " + cursos.Sum(x => x.Price * x.Quantity).ToString("C", new System.Globalization.CultureInfo("es-CR")), fontTitulo));

                documento.Close();

                byte[] bytes = memoryStream.ToArray();
                return File(bytes, "application/pdf", "Lista_Cursos.pdf");
            }
        }


        [HttpPost]
        public ActionResult CitasPDF()
        {
            // Suponemos que esta llamada obtiene los datos necesarios
            List<AgendaEnt> citas = modelAgenda.ConsultaCitas();

            if (citas == null || !citas.Any())
            {
                return Content("No se encontraron datos para generar el informe de citas.");
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document documento = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(documento, memoryStream);
                documento.Open();

                // Definir fuentes
                Font fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
                Font fontSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
                Font fontCuerpo = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);

                // Agregar un logo
                string imageURL = Server.MapPath("~/Images/Logo.png"); // Asegúrate de tener esta imagen en tu proyecto
                Image logo = Image.GetInstance(imageURL);
                logo.ScaleToFit(50f, 50f);
                logo.SpacingBefore = 10;
                logo.SpacingAfter = 10;
                logo.Alignment = Element.ALIGN_RIGHT;
                documento.Add(logo);

                // Título del documento
                documento.Add(new Paragraph("Lista de Citas", fontTitulo));

                // Línea separadora
                LineSeparator separator = new LineSeparator(1f, 100f, BaseColor.DARK_GRAY, Element.ALIGN_CENTER, -1);
                documento.Add(new Chunk(separator));

                // Datos del documento
                documento.Add(new Paragraph($"Fecha: {DateTime.Now.ToString("dd/MM/yyyy")}", fontCuerpo));
                documento.Add(new Paragraph("\n"));

                // Crear una tabla para los detalles de las citas
                PdfPTable table = new PdfPTable(new float[] { 1, 2, 3, 2, 2, 2 }) { WidthPercentage = 100 };
                table.DefaultCell.Padding = 5;

                // Definir el estilo del encabezado de la tabla
                BaseColor colorEncabezado = new BaseColor(0, 121, 182); // Un azul oscuro
                Font fontEncabezado = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);

                // Añadir los encabezados de la tabla
                table.AddCell(new PdfPCell(new Phrase("ID", fontEncabezado)) { BackgroundColor = colorEncabezado });
                table.AddCell(new PdfPCell(new Phrase("Nombre del Paciente", fontEncabezado)) { BackgroundColor = colorEncabezado });
                table.AddCell(new PdfPCell(new Phrase("Dirección", fontEncabezado)) { BackgroundColor = colorEncabezado });
                table.AddCell(new PdfPCell(new Phrase("Asunto", fontEncabezado)) { BackgroundColor = colorEncabezado });
                table.AddCell(new PdfPCell(new Phrase("Descripción", fontEncabezado)) { BackgroundColor = colorEncabezado });
                table.AddCell(new PdfPCell(new Phrase("Fecha y Hora", fontEncabezado)) { BackgroundColor = colorEncabezado });

                // Rellenar la tabla con los detalles de las citas
                foreach (var item in citas)
                {
                    table.AddCell(new Phrase(item.AppointmentID.ToString(), fontCuerpo));
                    table.AddCell(new Phrase(item.UserName + " " + item.Surnames, fontCuerpo));
                    table.AddCell(new Phrase($"País: {item.Country}\nEstado: {item.State}\nCiudad: {item.City}\nDistrito: {item.District}\nOtras señas: {item.Street}\nCod. Postal: {item.ZipCode}", fontCuerpo));
                    table.AddCell(new Phrase(item.Name, fontCuerpo));
                    table.AddCell(new Phrase(item.Description, fontCuerpo));
                    table.AddCell(new Phrase(item.DateandTime.ToString("dd/MM/yyyy HH:mm"), fontCuerpo));
                }

                // Añadir la tabla al documento
                documento.Add(table);

                documento.Close();

                byte[] bytes = memoryStream.ToArray();
                return File(bytes, "application/pdf", "Lista_Citas.pdf");
            }
        }
        [HttpPost]
        public ActionResult DescargarPDF(int masterPurchaseID)
        {
            // Suponemos que esta llamada obtiene los datos necesarios
            List<FacturaEnt> facturas = modelCarrito.ConsultaDetalleFactura(masterPurchaseID);

            if (facturas == null || !facturas.Any())
            {
                return Content("No se encontraron datos para generar la factura.");
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document documento = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(documento, memoryStream);
                documento.Open();

                var primeraFactura = facturas.FirstOrDefault();

                if (primeraFactura != null)
                {
                    // Definir fuentes
                    Font fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
                    Font fontSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
                    Font fontCuerpo = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);

                    // Agregar un logo
                    string imageURL = Server.MapPath("~/Images/Logo.png"); // Asegúrate de tener esta imagen en tu proyecto
                    Image logo = Image.GetInstance(imageURL);
                    logo.ScaleToFit(50f, 50f);
                    logo.SpacingBefore = 10;
                    logo.SpacingAfter = 10;
                    logo.Alignment = Element.ALIGN_RIGHT;
                    documento.Add(logo);

                    // Título de la factura
                    documento.Add(new Paragraph("Factura", fontTitulo));

                    // Línea separadora
                    LineSeparator separator = new LineSeparator(1f, 100f, BaseColor.DARK_GRAY, Element.ALIGN_CENTER, -1);
                    documento.Add(new Chunk(separator));

                    // Datos de la factura
                    documento.Add(new Paragraph($"Factura #: {primeraFactura.MasterPurchaseID}", fontSubtitulo));
                    documento.Add(new Paragraph($"Fecha: {primeraFactura.PurchaseDate.ToString("dd/MM/yyyy")}", fontCuerpo));
                    documento.Add(new Paragraph($"Cliente ID: {primeraFactura.UserName}", fontCuerpo));
                    documento.Add(new Paragraph("\n"));

                    // Crear una tabla para los detalles de la factura
                    PdfPTable table = new PdfPTable(new float[] { 3, 1, 2, 1, 2 }) { WidthPercentage = 100 };
                    table.DefaultCell.Padding = 5;

                    // Definir el estilo del encabezado de la tabla
                    BaseColor colorEncabezado = new BaseColor(0, 121, 182); // Un azul oscuro
                    Font fontEncabezado = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);

                    // Añadir los encabezados de la tabla
                    PdfPCell cell = new PdfPCell(new Phrase("Nombre del Producto", fontEncabezado)) { BackgroundColor = colorEncabezado };
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Cantidad", fontEncabezado)) { BackgroundColor = colorEncabezado };
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Precio Unitario", fontEncabezado)) { BackgroundColor = colorEncabezado };
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Impuesto", fontEncabezado)) { BackgroundColor = colorEncabezado };
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Subtotal", fontEncabezado)) { BackgroundColor = colorEncabezado };
                    table.AddCell(cell);

                    // Rellenar la tabla con los detalles de la factura
                    foreach (var item in facturas)
                    {
                        table.AddCell(new Phrase(item.Name, fontCuerpo));
                        table.AddCell(new Phrase(item.PaidQuantity.ToString(), fontCuerpo));
                        table.AddCell(new Phrase(item.PaidPrice.ToString("C"), fontCuerpo));
                        table.AddCell(new Phrase(item.Tax.ToString("C"), fontCuerpo));
                        table.AddCell(new Phrase(item.SubTotal.ToString("C"), fontCuerpo));
                    }

                    // Añadir la tabla al documento
                    documento.Add(table);

                    // Total general de la factura
                    documento.Add(new Paragraph("Total: " + facturas.Sum(x => x.Total).ToString("C"), fontTitulo));

                    documento.Close();
                }

                byte[] bytes = memoryStream.ToArray();
                return File(bytes, "application/pdf", $"Factura_{masterPurchaseID}.pdf");
            }
        }

        [HttpPost]
        public ActionResult CFac(int masterPurchaseID)
        {
            // Suponemos que esta llamada obtiene los datos necesarios
            List<FacturaCursoEnt> facturas = modelRegid.ConsultaDetalleFacturaCurso(masterPurchaseID);

            if (facturas == null || !facturas.Any())
            {
                return Content("No se encontraron datos para generar la factura.");
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document documento = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(documento, memoryStream);
                documento.Open();

                var primeraFactura = facturas.FirstOrDefault();

                if (primeraFactura != null)
                {
                    // Definir fuentes
                    Font fontTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
                    Font fontSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.BLACK);
                    Font fontCuerpo = FontFactory.GetFont(FontFactory.HELVETICA, 10, BaseColor.BLACK);

                    // Agregar un logo
                    string imageURL = Server.MapPath("~/Images/Logo.png"); // Asegúrate de tener esta imagen en tu proyecto
                    Image logo = Image.GetInstance(imageURL);
                    logo.ScaleToFit(50f, 50f);
                    logo.SpacingBefore = 10;
                    logo.SpacingAfter = 10;
                    logo.Alignment = Element.ALIGN_RIGHT;
                    documento.Add(logo);

                    // Título de la factura
                    documento.Add(new Paragraph("Factura de Curso", fontTitulo));

                    // Línea separadora
                    LineSeparator separator = new LineSeparator(1f, 100f, BaseColor.DARK_GRAY, Element.ALIGN_CENTER, -1);
                    documento.Add(new Chunk(separator));

                    // Datos de la factura
                    documento.Add(new Paragraph($"Factura #: {primeraFactura.MasterPurchaseCurseID}", fontSubtitulo));
                    documento.Add(new Paragraph($"Fecha: {primeraFactura.PurchaseDate.ToString("dd/MM/yyyy")}", fontCuerpo));
                    documento.Add(new Paragraph($"Cliente: {primeraFactura.UserName} {primeraFactura.Surnames}", fontCuerpo));
                    documento.Add(new Paragraph("\n"));

                    // Crear una tabla para los detalles de la factura
                    PdfPTable table = new PdfPTable(new float[] { 3, 1, 2, 2, 2, 2, 2, 2 }) { WidthPercentage = 100 };
                    table.DefaultCell.Padding = 5;

                    // Definir el estilo del encabezado de la tabla
                    BaseColor colorEncabezado = new BaseColor(0, 121, 182); // Un azul oscuro
                    Font fontEncabezado = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);

                    // Añadir los encabezados de la tabla
                    string[] encabezados = { "Nombre", "Cantidad", "Precio X Und", "Impuesto X Und", "SubTotal", "Impuesto", "Total", "Pago" };
                    foreach (var encabezado in encabezados)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(encabezado, fontEncabezado)) { BackgroundColor = colorEncabezado };
                        table.AddCell(cell);
                    }

                    // Rellenar la tabla con los detalles de la factura
                    foreach (var item in facturas)
                    {
                        table.AddCell(new Phrase(item.Name, fontCuerpo));
                        table.AddCell(new Phrase(item.PaidQuantity.ToString(), fontCuerpo));
                        table.AddCell(new Phrase(item.PaidPrice.ToString("C"), fontCuerpo));
                        table.AddCell(new Phrase(item.Tax.ToString("C"), fontCuerpo));
                        table.AddCell(new Phrase(item.SubTotal.ToString("C"), fontCuerpo));
                        table.AddCell(new Phrase(item.Impuesto.ToString("C"), fontCuerpo));
                        table.AddCell(new Phrase(item.Total.ToString("C"), fontCuerpo));
                        table.AddCell(new Phrase(item.PaymentStatus, fontCuerpo));
                    }

                    // Añadir la tabla al documento
                    documento.Add(table);

                    // Total general de la factura
                    documento.Add(new Paragraph("Total: " + facturas.Sum(x => x.Total).ToString("C"), fontTitulo));

                    documento.Close();
                }

                byte[] bytes = memoryStream.ToArray();
                return File(bytes, "application/pdf", $"FacturaCurso_{masterPurchaseID}.pdf");
            }
        }
    }
}
