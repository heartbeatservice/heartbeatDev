using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Mail;
using HBS.Entities;
namespace HBS.WebPortal.Controllers
{
    public class EmailController : ApiController
    {

        public bool postEmailMessage([FromBody] EmailMessage msg){

           // var queryString = Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value);
           
            
               // FullName=queryString["FullName"];
               // Message=queryString["Question"];

            bool result = false;
            if (msg != null && msg.Captcha=="qGphJD")
            {
                msg.ToEmail = "Umais@heartbeatservice.com";
                msg.serverAddress = "mail.heartbeat-biz.com";
                MailMessage mail = new MailMessage("info@heartbeat-biz.com", msg.ToEmail);
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = msg.serverAddress;
                mail.Subject = "Hello this is " + msg.FullName;
                mail.Body = msg.Question;
                client.Send(mail);
                result = true;
            }
            return result;
        }


        [AcceptVerbs("OPTIONS")]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);

            return resp;
        }  

    }
}
