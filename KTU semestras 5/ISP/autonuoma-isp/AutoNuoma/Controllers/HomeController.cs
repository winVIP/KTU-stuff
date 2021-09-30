using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using AutoNuoma.Models;

namespace AutoNuoma.Controllers
{
    public class HomeController : Controller
    {
        private AutonuomaDbContext db = new AutonuomaDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewsletterCreate()
        {
            ViewBag.Error = false;
            return View();
        }

        [HttpPost]
        public ActionResult NewsletterCreate(FormCollection collection)
        {
            var subject = collection["LetterSubject"];
            var body = collection["LetterBody"];

            if (subject == "" || body == "")
            {
                ViewBag.Error = true;
                return View("NewsletterCreate");
            }

            var fromAddress = new MailAddress("autonuomaisp@gmail.com", "AutoNuoma");
            const string fromPassword = "autonuomaauto";

            var users = db.Users.ToList();

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };

            foreach (var user in users)
            {
                var toAddress = new MailAddress(user.Email, user.FirstName);
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                    ViewBag.Error = false;
                }
            }
            return View("NewsletterCreate");
        }

        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}