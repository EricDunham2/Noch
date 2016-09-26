﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using NochWeb.Models;
using NochDAL.Data;
using NochDAL;

namespace NochWeb.Controllers
{
    public class HomeController : Controller
    {
        private NochDBEntities _db = new NochDBEntities();

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = "Noch";

            return View();
        }

		[AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> RegisterConfirm(UserModel model)
        {
            // determine if new domain was created

            try
            {
                Users user = new Users()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Username = model.Username,
                    Password = model.Password,
                    Email = model.Email,
                    IsEmailConfirmed = false,
                    Phone = model.Phone,
                    Address = model.Address,
                    City = model.City,
                    Province = model.Province,
                    Country = model.Country,
                    PostalCode = model.PostalCode,
                    CreatedOn = DateTime.Now,
                    UpdatedOn = DateTime.Now
                };

                UserService.Register(user);

				return View("Index");
            }
            catch (Exception ex) { return View(); }
        }
    }
}