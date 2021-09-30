using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net.Http;
using System.Net;
using Microsoft.Owin.Security;
using System.Security.Cryptography;
using System.Globalization;
using System.Net.Mail;

namespace AutoNuoma.Controllers
{
    public class ChatController : Controller
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        AutonuomaDbContext db = new AutonuomaDbContext();
        // GET: Chat/Chat/1
        [Authorize]
        public ActionResult Chat() //int id
        {
            
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ChatAdmin(int chatID) //int id
        {
            //var id = int.Parse(AuthenticationManager.User.Claims.ToArray()[1].Value);
            return View(db.Messages.Include("User").Where(b => b.ChatId == chatID));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChatAdmin(FormCollection collection)
        {
            try
            {
                Message newMessage = new Message();
                newMessage.ChatId = int.Parse(collection["chatID"]);
                newMessage.Text = collection["Text"];
                newMessage.Date = DateTime.UtcNow;
                newMessage.UserId = int.Parse(AuthenticationManager.User.Claims.ToArray()[1].Value);

                db.Messages.Add(newMessage);
                db.SaveChanges();

                return RedirectToAction("ChatList");
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/ChatUser/1
        [Authorize(Roles = "Member")]
        public ActionResult ChatUser() //int id
        {
            var id = int.Parse(AuthenticationManager.User.Claims.ToArray()[1].Value);
            return View(db.Messages.Include("User").Where(b => b.UserId == id));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult ChatUser(FormCollection collection)
        {
            try
            {
                var id = int.Parse(AuthenticationManager.User.Claims.ToArray()[1].Value);
                if (db.Chats.Where(b => b.UserId == id).Where(b => b.IsCaseClosed == false).Count() <= 0)
                {
                    Chat newChat = new Chat();
                    newChat.IsCaseClosed = false;
                    newChat.UserId = int.Parse(AuthenticationManager.User.Claims.ToArray()[1].Value);
                    newChat.CaseNumber = 1;
                    int chatID = db.Chats.Add(newChat).Id;

                    Message newMessage = new Message();
                    newMessage.ChatId = chatID;
                    newMessage.Text = collection["Text"];
                    newMessage.Date = DateTime.UtcNow;
                    newMessage.UserId = int.Parse(AuthenticationManager.User.Claims.ToArray()[1].Value);

                    db.Messages.Add(newMessage);
                    db.SaveChanges();

                    var fromAddress = new MailAddress("autonuomaisp@gmail.com", "AutoNuoma");
                    const string fromPassword = "autonuomaauto";

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                        Timeout = 20000
                    };

                    var toAddress = new MailAddress(db.Users.Find(id).Email, db.Users.Find(id).FirstName);
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = "Sukurta nauja byla",
                        Body = "Jūsų byla yra užregistruota ir bus peržiūrima."
                    })
                    {
                        smtp.Send(message);
                        ViewBag.Error = false;
                    }

                    return RedirectToAction("ChatUser");
                }
                else
                {
                    Message newMessage = new Message();
                    newMessage.ChatId = db.Chats.Where(b => b.UserId == id).Where(b => b.IsCaseClosed == false).First().Id;
                    newMessage.Text = collection["Text"];
                    newMessage.Date = DateTime.UtcNow;
                    newMessage.UserId = int.Parse(AuthenticationManager.User.Claims.ToArray()[1].Value);

                    db.Messages.Add(newMessage);
                    db.SaveChanges();

                    return RedirectToAction("ChatUser");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Chat/ChatList
        [Authorize(Roles = "Admin")]
        public ActionResult ChatList()
        {
            return View(db.Chats.Include("User"));
        }

        // GET: Chat/Create
        [Authorize]
        public ActionResult MessageCreate()
        {
            return RedirectToAction("ChatList");
        }

        //// POST: Chat/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Chat/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Chat/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Chat/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Chat/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
