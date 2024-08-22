using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;

namespace CaplugaAPI.Entities
{
    public class Utilitarios
    {
        public void EnviarCorreo(string destino, string asunto, string contenido, byte[] archivoAdjunto = null, string nombreArchivoAdjunto = null)
        {
            MailAddress addressForm = new MailAddress(ConfigurationManager.AppSettings["cuentaCorreo"], "CAPLUGA");
            MailAddress addressTo = new MailAddress(destino);
            MailMessage message = new MailMessage(addressForm, addressTo);
            message.Subject = asunto;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.Normal;
            message.Body = contenido;

            // Adjuntar archivo si existe
            if (archivoAdjunto != null && nombreArchivoAdjunto != null)
            {
                // Crear el MemoryStream y mantenerlo abierto mientras se adjunta
                var stream = new MemoryStream(archivoAdjunto);
                var adjunto = new Attachment(stream, nombreArchivoAdjunto);
                message.Attachments.Add(adjunto);
            }
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["cuentaCorreo"], ConfigurationManager.AppSettings["claveCorreo"]);
            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                System.Diagnostics.Trace.TraceInformation("Exception caught in CreateTestMessage2(): {0}",
                    ex.ToString());
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                    ex.ToString());
            }
            finally
            {
                // Limpiar los recursos
                foreach (Attachment attachment in message.Attachments)
                {
                    attachment.ContentStream.Close(); // Cerrar el contenido del stream
                    attachment.Dispose(); // Limpiar los recursos
                }
            }
        }
    }
}