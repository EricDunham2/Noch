using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using NochDAL.Data;
using NochWeb.Models;


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

        public ActionResult SaveAccountSettings(UserModel user)
        {


            return View();
        }
    }
}