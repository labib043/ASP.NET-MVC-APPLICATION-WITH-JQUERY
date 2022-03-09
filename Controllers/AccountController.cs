using Bikemvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Bikemvc.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.LogInPage model)
        {
            using (var context = new BikeDbContext())
            {
                bool isValid = context.SignUps.Any(x => x.UserName == model.Name && x.Password == model.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, false);
                    return RedirectToAction("Bikes", "Bike");
                }
                ModelState.AddModelError("", "Invalid Login");
                return View();
            }


        }
        public ActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(SignUp model)
        {
            using (var context = new BikeDbContext())
            {
                context.SignUps.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}