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
    }
}