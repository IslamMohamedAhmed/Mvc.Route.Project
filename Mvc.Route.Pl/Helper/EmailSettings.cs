using System.Net;
using System.Net.Mail;
using Mvc.Route.Pl.Models;

namespace Mvc.Route.Pl.Helper
{
    public class EmailSettings
    {
        public static void SendEmail(EmailModel model)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            //rfmoxgyrzjbdfjsz
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("nanorules714@gmail.com", "rfmoxgyrzjbdfjsz");
            client.Send("nanorules714@gmail.com", model.To, model.Subject, model.Body);
        }
    }
}
