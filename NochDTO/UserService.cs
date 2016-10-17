using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NochDAL.Data;

namespace NochDAL
{

    public class UserService : BaseService
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
                catch (Exception ex) { }
            }
        }

        public static string GetUsername(int userId)
        {
            string username = "";
            using (NochDBEntities _db = new NochDBEntities())
            {
                try
                {
                    var query = from u in _db.Users
                                  where u.UserID == userId
                                  select u.Username;

                    username = query.FirstOrDefault();
                }
                catch (Exception ex) { }
            }

            return username;
        }
    }
}