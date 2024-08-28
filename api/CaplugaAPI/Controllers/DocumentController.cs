using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using iTextSharp.text.pdf.draw;
using Org.BouncyCastle.Utilities;
using DocumentFormat.OpenXml.Office2013.Excel;

namespace CaplugaAPI.Controllers
{
    public class DocumentController : Controller
    {
        CarritoController carritoController = new CarritoController();
        MedicalImplements producto = new MedicalImplements();
        MedicalCourses curso = new MedicalCourses();


        [HttpPost]
        public byte[] ProductoPDF(MasterPurchase factura, List<Detail> detalles, Users usuario)
        {

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
                string imageURL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "Logo.png");
                Image logo = Image.GetInstance(imageURL);
                logo.ScaleToFit(50f, 50f);
                logo.SpacingBefore = 10;
                logo.SpacingAfter = 10;
                logo.Alignment = Element.ALIGN_RIGHT;
                documento.Add(logo);

                // Add company information
                Paragraph companyInfo = new Paragraph();
                companyInfo.Add(new Chunk("CAPLUGA FACTURA", fontTitulo));
                companyInfo.Add(Chunk.NEWLINE);
                companyInfo.Add(new Chunk("Dirección:  Cuidad Colón, San José, Costa Rica", fontCuerpo));
                companyInfo.Add(Chunk.NEWLINE);
                companyInfo.Add(new Chunk("Teléfono: +506 8405 7710", fontCuerpo));
                companyInfo.Add(Chunk.NEWLINE);
                companyInfo.Add(new Chunk("Email: capluga2020@gmail.com", fontCuerpo));
                documento.Add(companyInfo);

                // Línea separadora
                LineSeparator separator = new LineSeparator(1f, 100f, BaseColor.DARK_GRAY, Element.ALIGN_CENTER, -1);
                documento.Add(new Chunk(separator));

                // Datos del documento
                documento.Add(new Paragraph($"Factura #: {factura.MasterPurchaseID}", fontCuerpo));
                documento.Add(new Paragraph($"Fecha: {factura.PurchaseDate.ToString("dd/MM/yyyy")}", fontCuerpo));
                documento.Add(new Paragraph($"Cliente: {factura.Users.UserName} {factura.Users.Surnames}", fontCuerpo));
                documento.Add(new Paragraph("\n"));

                // Crear una tabla para los detalles de la factura
                PdfPTable table = new PdfPTable(new float[] { 3, 3, 1, 2, 2 }) { WidthPercentage = 100 };
                table.DefaultCell.Padding = 5;

                // Definir el estilo del encabezado de la tabla
                BaseColor colorEncabezado = new BaseColor(0, 121, 182); // Un azul oscuro
                Font fontEncabezado = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);

                // Añadir los encabezados de la tabla
                PdfPCell cell = new PdfPCell(new Phrase("Nombre del Producto", fontEncabezado)) { BackgroundColor = colorEncabezado };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Precio", fontEncabezado)) { BackgroundColor = colorEncabezado };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Cantidad", fontEncabezado)) { BackgroundColor = colorEncabezado };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Impuesto", fontEncabezado)) { BackgroundColor = colorEncabezado };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Total", fontEncabezado)) { BackgroundColor = colorEncabezado };
                table.AddCell(cell);

                // Rellenar la tabla con los detalles de los productos
                foreach (var item in detalles)
                {
                    using (var context = new CAPLUGAEntities())
                    {
                        producto = (from x in context.MedicalImplements
                                    where x.MedicalImplementsID == item.MedicalImplementsID
                                    select x).FirstOrDefault();
                    }
                    table.AddCell(new Phrase(producto.Name, fontCuerpo));
                    table.AddCell(new Phrase(item.PaidPrice.ToString("C", new System.Globalization.CultureInfo("es-CR")), fontCuerpo));
                    table.AddCell(new Phrase(item.PaidQuantity.ToString(), fontCuerpo));
                    table.AddCell(new Phrase(item.Tax.ToString("C", new System.Globalization.CultureInfo("es-CR")), fontCuerpo));
                    var subTotal = item.Tax + item.PaidPrice * item.PaidQuantity;
                    table.AddCell(new Phrase(subTotal.ToString("C", new System.Globalization.CultureInfo("es-CR")), fontCuerpo));
                }

                documento.Add(table);

                // Total general de los productos
                documento.Add(new Paragraph("Total General: " + factura.TotalPurchase.ToString("C", new System.Globalization.CultureInfo("es-CR")), fontTitulo));

                documento.Close();


                return memoryStream.ToArray();
            }
        }

        [HttpPost]
        public byte[] CursoPDF(MasterPurchaseCurse factura, List<DetailCurse> cursos, Users usuario)
        {
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
                string imageURL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "Logo.png");
                Image logo = Image.GetInstance(imageURL);
                logo.ScaleToFit(50f, 50f);
                logo.SpacingBefore = 10;
                logo.SpacingAfter = 10;
                logo.Alignment = Element.ALIGN_RIGHT;
                documento.Add(logo);

                // Add company information
                Paragraph companyInfo = new Paragraph();
                companyInfo.Add(new Chunk("CAPLUGA FACTURA", fontTitulo));
                companyInfo.Add(Chunk.NEWLINE);
                companyInfo.Add(new Chunk("Dirección:  Cuidad Colón, San José, Costa Rica", fontCuerpo));
                companyInfo.Add(Chunk.NEWLINE);
                companyInfo.Add(new Chunk("Teléfono: +506 8405 7710", fontCuerpo));
                companyInfo.Add(Chunk.NEWLINE);
                companyInfo.Add(new Chunk("Email: capluga2020@gmail.com", fontCuerpo));
                documento.Add(companyInfo);

                // Línea separadora
                LineSeparator separator = new LineSeparator(1f, 100f, BaseColor.DARK_GRAY, Element.ALIGN_CENTER, -1);
                documento.Add(new Chunk(separator));

                // Datos del documento
                documento.Add(new Paragraph($"Factura #: {factura.MasterPurchaseCurseID}", fontCuerpo));
                documento.Add(new Paragraph($"Fecha: {DateTime.Now.ToString("dd/MM/yyyy")}", fontCuerpo));
                documento.Add(new Paragraph($"Cliente: {factura.Users.UserName} {factura.Users.Surnames}", fontCuerpo));
                documento.Add(new Paragraph("\n"));

                // Crear una tabla para los detalles de los cursos
                PdfPTable table = new PdfPTable(new float[] { 3, 3, 1, 2, 2 }) { WidthPercentage = 100 };
                table.DefaultCell.Padding = 5;

                // Definir el estilo del encabezado de la tabla
                BaseColor colorEncabezado = new BaseColor(0, 121, 182); // Un azul oscuro
                Font fontEncabezado = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.WHITE);

                // Añadir los encabezados de la tabla
                PdfPCell cell = new PdfPCell(new Phrase("Nombre del Curso", fontEncabezado)) { BackgroundColor = colorEncabezado };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Precio", fontEncabezado)) { BackgroundColor = colorEncabezado };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Cantidad", fontEncabezado)) { BackgroundColor = colorEncabezado };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Impuesto", fontEncabezado)) { BackgroundColor = colorEncabezado };
                table.AddCell(cell);

                cell = new PdfPCell(new Phrase("Total", fontEncabezado)) { BackgroundColor = colorEncabezado };
                table.AddCell(cell);

                // Rellenar la tabla con los detalles 
                foreach (var item in cursos)
                {
                    using (var context = new CAPLUGAEntities())
                    {
                        curso = (from x in context.MedicalCourses
                                    where x.MedicalCourseID == item.MedicalCourseID
                                    select x).FirstOrDefault();
                    }
                    table.AddCell(new Phrase(curso.Name, fontCuerpo));
                    table.AddCell(new Phrase(item.PaidPrice.ToString("C", new System.Globalization.CultureInfo("es-CR")), fontCuerpo));
                    table.AddCell(new Phrase(item.PaidQuantity.ToString(), fontCuerpo));
                    table.AddCell(new Phrase(item.Tax.ToString("C", new System.Globalization.CultureInfo("es-CR")), fontCuerpo));
                    var subTotal = item.Tax + item.PaidPrice * item.PaidQuantity;
                    table.AddCell(new Phrase(subTotal.ToString("C", new System.Globalization.CultureInfo("es-CR")), fontCuerpo));
                }

                documento.Add(table);

                // Total general de los productos
                documento.Add(new Paragraph("Total General: " + factura.TotalPurchase.ToString("C", new System.Globalization.CultureInfo("es-CR")), fontTitulo));

                documento.Close();


                return memoryStream.ToArray();
            }
        }
    }
}