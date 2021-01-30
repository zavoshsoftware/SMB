using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Helpers
{
    public class Message
    {
       
        public void SendEmail(string reciever,string subject, string message)
        {
           // SmtpClient client = new SmtpClient("https://ghanongostar.com/webmail");

            SmtpClient client = new SmtpClient("smtp.ds.network");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "163.47.74.144";
            client.Port = 25;

            //If you need to authenticate
            client.Credentials = new NetworkCredential("quote@smbcloudsolutions.com.au", "Tr%749mm#$");

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("quote@smbcloudsolutions.com.au");
            mailMessage.To.Add(reciever);
            mailMessage.Subject = subject;
            mailMessage.Body = message;
            
            mailMessage.IsBodyHtml = true;

            client.Send(mailMessage);
        }
    }
}