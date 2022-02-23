using marble.Context;
using marble.Models;
using marble.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Net.Mime;

namespace marble.Controllers
{
    public class ContactController : MyController
    {
        private MarbleEntityFrameWork context;
        public ContactController()
        {
            context = new MarbleEntityFrameWork();
        }
        // GET: Contact
        public ActionResult Contact()
        {
            ContactPageModel contact = new ContactPageModel();
            contact.ContactInfo = context.Contact.FirstOrDefault();
            return View(contact);
        }
            public ActionResult contactAdd(pmContactForm param)
        {
                
            if (param.name != null && param.email != null && param.message != null)
            {


                string to = param.email;
                string from = "info@palarchive.info"; 
                MailMessage message = new MailMessage(from, from);
                string mailbody = param.message;
                message.BodyEncoding = Encoding.UTF8;
                message.HeadersEncoding = Encoding.UTF8;
                message.SubjectEncoding = Encoding.UTF8;
                message.Subject =param.name;
                message.Body = mailbody;
                message.IsBodyHtml = true;
                string title = string.Format("<html><body style=\"border: 2px solid  antiquewhite;margin:5px;border-radius:17px;\"> <h1 style=\"text-align: center;box-shadow: 0px 0px 7px 11px antiquewhite;    background-color: antiquewhite;\">الأرشيف الفلسطيني ومركز التوثيق</h1><br />" +
                    "<h2 style=\"text-align: right;\">   الاسم : {0}  </h2><br />" +
                    "<h2 style=\"text-align: right;direction: rtl;\">   الأيميل : {1}  </h2><br />" +
                    "<h2 style=\"text-align: right;direction: rtl;\">   الرسالة : {2}  </h2><br />" +
                    "</body></html>", param.name,from,param.message);
                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(title, Encoding.UTF8, MediaTypeNames.Text.Html);

                message.AlternateViews.Add(avHtml);
                SmtpClient client = new SmtpClient("srvm11.trwww.com", 587);   
                System.Net.NetworkCredential basicCredential1 = new System.Net.NetworkCredential("info@palarchive.info", "Ahmed@#1234");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;
                try
                {
                    client.Send(message);
                }

                catch (Exception ex)
                {
                    throw ex;

                }

            }
        return RedirectToAction("Contact");
        }
    }
}