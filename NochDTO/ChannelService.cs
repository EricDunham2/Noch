using NochDAL.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NochDAL
{
    public class ChannelService : BaseService
    {
        public static List<Channels> GetChannelsForDomain(int domainId)
        {
            using (NochDBEntities _db = new NochDBEntities())
            {
                var channels = new List<Channels>();

                try
                {
                    IQueryable<Channels> query = (from c in _db.Channels
                                                where c.DomainID == domainId
                                                select c).Include("Messages").AsNoTracking();
                    channels = query.ToList();

                }
                catch (Exception ex) { }

                return channels;
            }
        }

        public static void MakeChannel(Channels c)
        {
            using (NochDBEntities _db = new NochDBEntities())
            {
                try
                {
                    _db.Channels.Add(c);
                    _db.SaveChanges();
                }
                catch (Exception ex) { }
            }
        }
    }
}
