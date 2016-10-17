using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NochDAL.Data;

namespace NochDAL
{

    public class UserService : ServiceBase
    {
        public static void Register(Users user)
        {
            using (NochDBEntities _db = new NochDBEntities())
            {
                try
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();
                }
                catch(Exception ex) { }
            }
        }

        public static List<Domains> GetDomainsForUser(int userId)
        {
            using (NochDBEntities _db = new NochDBEntities())
            {
                var domains = new List<Domains>();

                try
                {
                    IQueryable<Domains> query = from d in _db.Domains
                                                  join ud in _db.UserDomains
                                                  on d.DomainID equals ud.DomainID
                                                  where ud.UserID == userId
                                                  select d;
                    domains = query.ToList();

                }
                catch (Exception ex) { }

                return domains;
            }
        }
    }
}