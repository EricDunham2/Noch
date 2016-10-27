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
                int idxd = 0;
                while (Session["currchannel"] == null){
                    if(domains[idxd].Channels.Count != 0)
                        Session["currchannel"] = domains[idxd].Channels.ElementAt(0).ChannelID;
                    ++idxd;
                }
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

        public void MakeDomain(string newDomain, int userid)
        {

            Domains d = new Domains
            {
                Name = newDomain,
                UpdatedOn = DateTime.Now
            };

            UserDomains ud = new UserDomains
            {
                UserID = userid,
                DomainID  = -1
            };
            DomainService.MakeDomain(d,ud);
            Index();
        }

        [HttpGet]
        public JsonResult GetMessages(int channelId, int messageCount)
        {

            SortedList<int, string> nameLookUpTable;
            var messages = MessageService.GetMessages(channelId, messageCount);
            SortedList<int, SortedList<int, string>> chnlLookUpTable = (SortedList<int, SortedList<int, string>>)Session["ChnlLookUpTable"];
            if (chnlLookUpTable == null) { chnlLookUpTable = new SortedList<int, SortedList<int, string>>(); }
            if (chnlLookUpTable.ContainsKey(channelId)) { nameLookUpTable = chnlLookUpTable[channelId]; }
            else { nameLookUpTable = new SortedList<int, string>(); }

            var models = new List<MessageModel>();
            string username = "";
            foreach (var message in messages)
            {

                if (nameLookUpTable.ContainsKey(message.UserID))
                {
                    username = nameLookUpTable[message.UserID];
                }
                else
                {
                    username = UserService.GetUsername(message.UserID);
                    nameLookUpTable[message.UserID] = username;

                }

                var model = new MessageModel
                {
                    MessageID = message.MessageID,
                    ChannelID = message.ChannelID,
                    UserID = message.UserID,
                    Username = username,
                    Content = message.Content,
                    Timestamp = message.Timestamp,
                    IsEdited = message.IsEdited,
                    LastUpdated = message.UpdatedOn
                };
                models.Add(model);
            }

            Session["currchannel"] = channelId;
            chnlLookUpTable[channelId] = nameLookUpTable;
            Session["ChnlLookUpTable"] = chnlLookUpTable;

            return Json(models, JsonRequestBehavior.AllowGet);
        }
    }
}