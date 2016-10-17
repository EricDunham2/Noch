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

            if (user != null)
            {
                ViewBag.Title = "Chat";
                ViewBag.Username = user.Username;
                ViewBag.UserID = user.UserID;

                // get all the domains for the user
                var domains = DomainService.GetDomainsForUser(user.UserID);
                foreach (Domains d in domains)
                {
                    d.Channels = ChannelService.GetChannelsForDomain(d.DomainID);
                }

                // set default channel if there isnt one
                if (Session["currchannel"] == null)
                    Session["currchannel"] = domains[0].Channels.ElementAt(0).ChannelID;

                return View(domains);
            }
            else
                return Redirect("Home/Index");
        }

        public void SendMessage(int userId, int channelId, string msg)
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
        }

        [HttpGet]
        public JsonResult GetMessages(int channelId, int messageCount)
        {
            var messages = MessageService.GetMessages(channelId, messageCount);

            var models = new List<MessageModel>();
            foreach(var message in messages)
            {
                var model = new MessageModel
                {
                    MessageID = message.MessageID,
                    ChannelID = message.ChannelID,
                    UserID = message.UserID,
                    Username = UserService.GetUsername(message.UserID),
                    Content = message.Content,
                    Timestamp = message.Timestamp,
                    IsEdited = message.IsEdited,
                    LastUpdated = message.UpdatedOn
                };
                models.Add(model);
            }

            Session["currchannel"] = channelId;

            return Json(models, JsonRequestBehavior.AllowGet);
        }
    }
}