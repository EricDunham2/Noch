using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NochDAL.Data;
using System.Data.Entity;

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

        public static void MakeDomain(string newDomain, int userid)
        {
            using (NochDBEntities _db = new NochDBEntities())
            {

                try
                {
                   /* String query = "INSERT INTO dbo.SMS_PW (name,id,password,email) VALUES(@id,@username,@password, @email)";

                    SqlCommand command = new SqlCommand(query, db.Connection);
                    command.Parameters.Add("@id", "abc");
                    command.Parameters.Add("@username", "abc");
                    command.Parameters.Add("@password", "abc");
                    command.Parameters.Add("@email", "abc");

                    command.ExecuteNonQuery();*/

                }
                catch (Exception ex) { }

            }
        }
    }
}
