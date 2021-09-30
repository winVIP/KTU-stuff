using AutoNuoma.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web.Mvc;
using System.Net.Http;
using System.Net;
using Microsoft.Owin.Security;
using System.Web;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Globalization;

namespace AutoNuoma.Controllers
{
    public class UserController : Controller
    {
        private const string AuthenticationType = "ApplicationCookie";
        private AutonuomaDbContext db = new AutonuomaDbContext();
        // GET: User
        
        [Authorize(Roles = "Admin")]
        public ActionResult MemberList()
        {
            List<Member> members = new List<Member>();
            foreach(User item in db.Users)
            {
                if(item.Type == 0)
                {
                    members.Add((Member)item);
                }
            }
            return View(members);
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection collection)
        {
            try
            {
                string email = null;
                string password = null;
                if (!string.IsNullOrEmpty(collection["Email"]))
                    email = collection["Email"].ToString();
                if (!string.IsNullOrEmpty(collection["Password"]))
                    password = Hash(collection["Password"].ToString());

                if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("", "Laukai privalo buti uzpildyti.");
                    return View();
                }
                    
                User user = db.Users.FirstOrDefault(x => x.Email == email && x.Password == password);

                if (user != null)
                {
                    string role = "Member";
                    if (user.Type == 1) role = "Admin";
                    ClaimsIdentity identity = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, role)
                    }, AuthenticationType);
                    AuthenticationManager.SignIn(identity);
                    
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Pateikėte neteisingus duomenis. Bandykite vėl.");
                return View();
            }
            catch
            {
                ModelState.AddModelError("", "Iskilo problema.");
                return View();
            }
        }
        
        [Authorize]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(AuthenticationType);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Account()
        {
            return View();
        }

        // GET: User/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult MemberDetails()//int id
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult MemberEdit()
        {
            return View();
        }

        // GET: User/Create
        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(Member member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    member.Type = 0;
                    member.Password = Hash(member.Password);
                    db.Users.Add(member);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Blogi duomenys.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Iskilo problema. " + ex);
                return View();
            }
        }

        // GET: User/Edit/5
        [Authorize]
        public ActionResult AccountEdit()//int id
        {
            var id = AuthenticationManager.User.Claims.ToArray()[1].Value;

            var user = db.Users.Find(int.Parse(id));
            var email = user.Email.ToString();
            var firstName = user.FirstName.ToString();
            var lastName = user.LastName.ToString();
            ViewBag.email = email;
            ViewBag.firstName = firstName;
            ViewBag.lastName = lastName;
            
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AccountEdit(FormCollection collection)
        {
            try
            {
                var id = AuthenticationManager.User.Claims.ToArray()[1].Value;
                db.Users.Find(int.Parse(id)).FirstName = collection["firstName"];
                db.Users.Find(int.Parse(id)).LastName = collection["lastName"];
                db.Users.Find(int.Parse(id)).Email = collection["email"];

                db.SaveChanges();

                return RedirectToAction("Account");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult MemberDelete()//int id
        {
            return RedirectToAction("MemberList");
        }

        [Authorize]
        public ActionResult AccountDelete()//int id
        {
            var id = AuthenticationManager.User.Claims.ToArray()[1].Value;
            db.Users.Remove(db.Users.Find(int.Parse(id)));
            
            AuthenticationManager.SignOut(AuthenticationType);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // POST: User/Delete/5
        [HttpPost]
        [Authorize]
        public ActionResult Delete()//int id
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private string Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}
