using NochDAL;
using NochDAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NochDAL
{
    public class MessageService : BaseService
    {
        public static void SendMessage (Messages message)
        {
            using (NochDBEntities _db = new NochDBEntities())
            {
                try
                {
                    _db.Messages.Add(message);
                    _db.SaveChanges();
                }
                catch (Exception ex) { }
            }
        }

        public static List<Messages> GetMessages (int channelId, int messageCount)
        {
            var messages = new List<Messages>();

            using (NochDBEntities _db = new NochDBEntities())
            {
                try
                {
                    IQueryable<Messages> query = (from m in _db.Messages
                                                  where m.ChannelID == channelId
                                                  select m).Take(messageCount);
                    messages = query.ToList();
                }
                catch (Exception ex) { }
            }

            return messages;
        }
    }
}
