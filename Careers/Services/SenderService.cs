using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Careers.Services
{
    public class SenderService
    {
        public async Task SendEmail(string email, string subject, string message)
        {
            var mail = new MailMessage { Subject = subject, Body = message ,IsBodyHtml = true};
            mail.To.Add(email);
            mail.From = new MailAddress("sayrus719.noreply@gmail.com","Careers.com");

            var smtpServer = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("sayrus719.noreply@gmail.com", "P@ssword123456"),
                EnableSsl = true
            };

           await smtpServer.SendMailAsync(mail);
        }

        public bool SendSms(string number, string header, string message)
        {
            var login = "its.gov";
            var password = "w.i.g.a1";

            var indexOf = number.IndexOf('0');
            if (indexOf == 0)
                number = number.Substring(1, number.Length - 1);


            var date = DateTime.Now.ToString("yyyy-MM-dd" + " " + "HH.mm.ss");
            var s = "";
          
            string xmlDesen = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                               "<request>\n" +
                               "<head>\n" +
                               "<operation>submit</operation>\n" +
                               "<login>" + login + @"</login>\n" +
                               "<password>" + password + @"</password>\n" +
                               "<title>" + header + @"</title>\n" +
                               "<scheduled>" + date + @"</scheduled>\n" +
                               "<isbulk>false</isbulk>\n" +
                               "<controlid>" + Guid.NewGuid() + @"</controlid>\n" +
                               "</head>\n" +
                               "<body>\n" +
                               "<msisdn>994" + number + @"</msisdn>\n" +
                               "<message>" + message + @"</message>\n" +
                               "</body>\n" +
                               "</request>";
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://sms.atatexnologiya.az/bulksms/api");
            byte[] byteArray = Encoding.UTF8.GetBytes(xmlDesen);
            request.Method = "POST";
            request.ContentType = "application/xml;charset=utf-8";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            XmlTextReader XML = new XmlTextReader(new StringReader(responseFromServer));
            while (XML.Read())
            {
                if (XML.Name == "responsecode")
                {
                    s = (XML.ReadString());
                }
                if (s != "") { break; }
            }
            XML.Close();
            if (s == "000")
            {
                return true;
            }

            return false;
        }
    }
}
