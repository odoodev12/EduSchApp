using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace SchoolApp.ClassLibrary
{
    public class EmailManagement
    {

        public bool SendEmail(string ToEmail,string subject,string message)
        {
            try
            {
                string EmailUserName = WebConfigurationManager.AppSettings["EmailUserName"];
                string EmailUserPassword = WebConfigurationManager.AppSettings["EmailUserPassword"];
                string HOST = WebConfigurationManager.AppSettings["HOST"];
                int PORT = Convert.ToInt32(WebConfigurationManager.AppSettings["PORT"]);
                bool SSL = Convert.ToBoolean(WebConfigurationManager.AppSettings["SSL"]);
                MailMessage mm = new MailMessage(EmailUserName, ToEmail);
                mm.Subject = subject;
                mm.Body = message;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = HOST;
                smtp.EnableSsl = SSL;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = EmailUserName;
                NetworkCred.Password = EmailUserPassword;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = PORT;
                smtp.Send(mm);
                return true;
            }
            catch(Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
                return false;
            }
        }
        public bool SendEmails(string ToEmail, string subject, string message, string FileName)
        {
            try
            {
                string EmailUserName = WebConfigurationManager.AppSettings["EmailUserName"];
                string EmailUserPassword = WebConfigurationManager.AppSettings["EmailUserPassword"];
                string HOST = WebConfigurationManager.AppSettings["HOST"];
                int PORT = Convert.ToInt32(WebConfigurationManager.AppSettings["PORT"]);
                bool SSL = Convert.ToBoolean(WebConfigurationManager.AppSettings["SSL"]);
                //MailMessage mm = new MailMessage(EmailUserName, ToEmail);
                //by shailesh parmar
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(EmailUserName);
                string[] Multi = ToEmail.Split(',');
                foreach (string item in Multi)
                {
                    mm.To.Add(new MailAddress(item));
                }
                //End
                if (FileName!="")
                {
                    Attachment data = new Attachment(FileName, MediaTypeNames.Application.Octet);
                    mm.Attachments.Add(data);
                }
                mm.Subject = subject;
                mm.Body = message;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = HOST;
                smtp.EnableSsl = SSL;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = EmailUserName;
                NetworkCred.Password = EmailUserPassword;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = PORT;
                smtp.Send(mm);
                return true;
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
                return false;
            }
        }
        public bool SendEmailToMultiple(string ToEmail, string subject, string message, string FileName)
        {
            try
            {
                ToEmail = ToEmail.Remove(ToEmail.Length - 1);
                string EmailUserName = WebConfigurationManager.AppSettings["EmailUserName"];
                string EmailUserPassword = WebConfigurationManager.AppSettings["EmailUserPassword"];
                string HOST = WebConfigurationManager.AppSettings["HOST"];
                int PORT = Convert.ToInt32(WebConfigurationManager.AppSettings["PORT"]);
                bool SSL = Convert.ToBoolean(WebConfigurationManager.AppSettings["SSL"]);
                MailMessage mm = new MailMessage(EmailUserName, ToEmail);
                if (FileName != "")
                {
                    Attachment data = new Attachment(FileName, MediaTypeNames.Application.Octet);
                    mm.Attachments.Add(data);
                }

                mm.From = new MailAddress(EmailUserName);
                string[] Multiple = ToEmail.Split(',');
                foreach (var item in Multiple)
                {
                    mm.To.Add(new MailAddress(item));
                }
                mm.Subject = subject;
                mm.Body = message;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = HOST;
                smtp.EnableSsl = SSL;
                NetworkCredential NetworkCred = new NetworkCredential();
                NetworkCred.UserName = mm.From.Address;
                NetworkCred.Password = EmailUserPassword;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = PORT;
                smtp.Send(mm);
                return true;
            }
            catch (Exception ex)
            {
                ErrorLogManagement.AddLog(ex);
                return false;
            }
        }
    }
}
