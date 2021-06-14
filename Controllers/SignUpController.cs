using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Acme_Ecommerce.Models;
using Acme_Database_Utility;

namespace Acme_Ecommerce.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserModel user)
        {
            DatabaseUtil util = new DatabaseUtil();
            bool inserted = util.Insert(user);

            if (inserted)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Message = "User could not be inserted";
                ViewBag.Bool = false;
                return View();
            }

           
        }
    }
}