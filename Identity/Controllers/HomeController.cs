﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Identity.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var Claims = User.Identity as ClaimsIdentity;
            ViewBag.Country = Claims.FindFirst(ClaimTypes.Country).Value;

            return View();
        }
    }
}