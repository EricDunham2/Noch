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
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            Users user = (Users)HttpContext.Session["user"];
            ViewBag.Title = "Chat";
            ViewBag.Username = user.Username;
            ViewBag.UserID = user.UserID;

            // get all the domains for the user
            var domains = UserService.GetDomainsForUser(user.UserID);

            return View();
        }

        public ActionResult SendMessage(int userId, int channelId, string msg)
        {
            Messages message = new Messages
            {
                UserID = userId,
                ChannelID = channelId,
                Content = msg,
                Timestamp = DateTime.Now,
                IsEdited = false,
                UpdatedOn = DateTime.Now

            };

            MessageService.SendMessage(message);

            return View("Index");
        }
    }
}