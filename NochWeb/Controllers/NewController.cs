using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NochWeb.Models;
using NochDAL.Data;
using NochDAL;


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
            Users user = (Users)HttpContext.Session["user"];

            Domains d = new Domains
            {
                Name = DomainName,
                UpdatedOn = DateTime.Now
            };

            UserDomains ud = new UserDomains
            {
                UserID = user.UserID,
                DomainID = -1
            };
            int id = DomainService.MakeDomain(d, ud);

            Channels c = new Channels
            {
                DomainID = id,
                Name = "Default",
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now
            };

            ChannelService.MakeChannel(c);

            //will redirect user to their chat room once done. redirects to home/index now.
            return RedirectToAction("Index", "Chat");
        }
    }
}