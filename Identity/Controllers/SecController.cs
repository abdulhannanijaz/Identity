﻿
using Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;


namespace Identity.Controllers
{
    [AllowAnonymous]
    public class SecController : Controller
    {
        // GET: Sec
        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            var model = new LogInModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult LogIn(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Don't do this in production!
            if (model.Email == "admin@admin.com" && model.Password == "password")
            {
                var identity = new ClaimsIdentity(new[] {
              
                new Claim(ClaimTypes.Email, "a@b.com"),
                new Claim(ClaimTypes.Country, "England")
            },
                    "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            // user authN failed
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }


        public ActionResult LogOut()
        {
            var auth = Request.GetOwinContext().Authentication;

            auth.SignOut("ApplicationCookie");

            return Content("Success");
        }


        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }

            return returnUrl;
        }
    }
}