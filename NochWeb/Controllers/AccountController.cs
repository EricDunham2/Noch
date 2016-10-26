using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NochDAL.Data;

namespace NochWeb.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            Users user = (Users)HttpContext.Session["user"];

            return View();
        }

        public async Task<ActionResult> SaveAccountSettings(UserModel user)
        {
            
        }
    }
}