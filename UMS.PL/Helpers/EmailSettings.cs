using System.Net;
using System.Net.Mail;
using UMS.DAL.Models;

namespace UMS.PL.Helpers
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);//Suppose To Be In AppSetting.json
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("mezog39@gmail.com", "ccgypexxgwlmvbow");
            Client.Send("mezog39@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
