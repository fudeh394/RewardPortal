using RewardSystemWeb.Interfaces;
using RewardSystemWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace RewardSystemWeb.Services
{
    public class EmailService : IEmail
    {
        

        private EmailService()
        {
            

        }

        private bool SendMail_AlertMail(MailModel mailModel)
        {
            mailModel.From = "FirstAlert@firstbanknigeria.com";
            string subject = mailModel.Subject;
            NetworkCredential networkCredential1 = new NetworkCredential();
            string smtpIp = ConfigurationManager.AppSettings["SMTP_IP"];
            int smtpIpPort = int.Parse(ConfigurationManager.AppSettings["SMTP_IP_Port"]); //this.SMTP_IP_Port;
            NetworkCredential networkCredential2 = new NetworkCredential("firstalert", "password2$", "hocdc-05.nigeria.firstbank.local");
            SmtpClient smtpClient = new SmtpClient(smtpIp, smtpIpPort);
            Exception exception;
            try
            {
                string body = mailModel.MailContent;
                string str1 = "";
                string str2 = " <img src=cid:Image1 /> ";
                str1 = " <img src=cid:Image2 /> ";
                AlternateView alternateViewFromString = AlternateView.CreateAlternateViewFromString((str2 + body).ToString(), (Encoding)null, "text/html");
                LinkedResource linkedResource = new LinkedResource("~/assets/img/Logo1.jpg");
                linkedResource.ContentId = "Image1";
                alternateViewFromString.LinkedResources.Add(linkedResource);
                MailMessage message = new MailMessage(mailModel.From, mailModel.To, subject, body);
                try
                {
                    message.CC.Add(new MailAddress(mailModel.CC));
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                message.IsBodyHtml = true;
                message.AlternateViews.Add(alternateViewFromString);
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = (ICredentialsByHost)networkCredential2;
                smtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }




        public bool Send(MailModel mailModel)
        {
            var res = false;
            try
            {
                SmtpClient client = new SmtpClient();
                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.EnableSsl = true;
                client.Port = 25;
                client.Host = "10.1.15.251";        
              

                System.Net.NetworkCredential credentials = new NetworkCredential("firstalert", "password2$", "hocdc-05.nigeria.firstbank.local");

                client.UseDefaultCredentials = false;
                client.Credentials = credentials;

                //can be obtained from your model
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(mailModel.From);
                msg.To.Add(new MailAddress(mailModel.To));
                msg.Bcc.Add(new MailAddress(mailModel.BC));
                msg.CC.Add(new MailAddress(mailModel.CC));

                // add attachements
                if (mailModel.Attachement != null)
                {
                    MemoryStream ms = new MemoryStream(mailModel.Attachement);
                    var attch1 = new Attachment(ms, "Attachement");
                    msg.Attachments.Add(attch1);
                }

                //Add subjects and headers
                msg.Subject = mailModel.Subject;
                msg.IsBodyHtml = mailModel.isBodyHtml;
                msg.Body = mailModel.MailContent;

                client.Send(msg);
                res = true;
            }
            catch (Exception ex)
            {
                res = false;
                //res = ex.Message; ;

            }
            return res;
        }
    }
}
