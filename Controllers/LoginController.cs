using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Acme_Database_Utility;
using Acme_Ecommerce.Models;

namespace Acme_Ecommerce.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserModel user)
        {
            DatabaseUtil util = new DatabaseUtil();

            UserSchema schema = util.Login(user.Email, user.Password);

            try
            {
                if (schema != null)
                {
                    UserModel newUser = new UserModel()
                    {
                        Id = schema.Id,
                        Fullname = schema.Fullname,
                        Email = schema.Email,
                        Role = schema.Role,
                        isLoggedIn = true
          
                    };

                    if(newUser.Role == "admin")
                    {
                        Session["isAdmin"] = true;
                    }

                    Session["isLoggedIn"] = true;
                    Session["user"] = newUser;
                return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Bool = false;
                    ViewBag.Message = "Invalid credentials";

                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Bool = false;
                ViewBag.Message = "Something went wrong... Please try again";
                return View();
            }
            

        }


    }
}