using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutoNuoma.Controllers
{
    public class SubscriptionController : Controller
    {
        // GET: Subscription
        [Authorize(Roles = "Member")]
        public ActionResult SubscriptionCreate()
        {
            return View();
        }

        public void SendSubscription()
        {
            
        }

    }
}
