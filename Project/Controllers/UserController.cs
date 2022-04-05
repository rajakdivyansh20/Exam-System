using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class UserController : Controller
    {
        // Login
        public ActionResult Login()
        {
            UserModel user = new UserModel();
            return View(user);
        }


        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            try
            {
                ProjectHandler Handler = new ProjectHandler();
                UserModel model = Handler.Verify(user);
                if (model.Name == null)
                {
                    ViewBag.message = "Invalid Creadentials Provided";
                    return RedirectToAction("Login");
                }
                else
                    Session["user"] = model;
                    return RedirectToAction("Index","Home");
            }
            catch
            {
                ViewBag.message = "Invalid Creadentials Provided";
                return RedirectToAction("Login" );
            }
        }
        public ActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Register(UserModel user)
        {
            try
            {
                ProjectHandler Handler = new ProjectHandler();
                UserModel model = Handler.Registration(user);

                return RedirectToAction("Login");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
