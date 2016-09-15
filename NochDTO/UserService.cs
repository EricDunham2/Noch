using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NochDTO
{

    public class UserService
    {
        public static void Register(User user)
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
    }
}