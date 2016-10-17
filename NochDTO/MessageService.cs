using NochDAL;
using NochDAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NochDAL
{
    public class MessageService : ServiceBase
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
    }
}
