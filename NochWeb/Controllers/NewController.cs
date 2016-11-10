using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NochWeb.Controllers
{
    public class NewController : Controller
    {
        // GET: New
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MakeDomain(string DomainName)
        {
            //make new domain here for the user that decided to make a new domain.


            //will redirect user to their chat room once done. redirects to home/index now.
            return RedirectToAction("Index", "Home");
        }
    }
}