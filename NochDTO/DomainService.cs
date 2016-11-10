using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NochDAL.Data;
using System.Data.Entity;
using System.Data.SqlClient;

namespace NochDAL
{
    public class DomainService : BaseService
    {
        public static List<Domains> GetDomainsForUser(int userId)
        {
            using (NochDBEntities _db = new NochDBEntities())
            {
                var domains = new List<Domains>();
                try
                {
                    IQueryable<Domains> query = (from d in _db.Domains
                                                join ud in _db.UserDomains
                                                on d.DomainID equals ud.DomainID
                                                where ud.UserID == userId
                                                select d).Include("Channels").AsNoTracking();
                    domains = query.ToList();
                }
                catch (Exception ex) { }

                return domains;
            }
        }

        public static void MakeDomain(Domains d, UserDomains ud)
        {
            using (NochDBEntities _db = new NochDBEntities())
            {
                try
                {  
                    _db.Domains.Add(d);
                    _db.SaveChanges();

                    ud.DomainID = d.DomainID;

                    _db.UserDomains.Add(ud);
                    _db.SaveChanges();

                }
                catch (Exception ex) { }
            }
        }
    }
}
