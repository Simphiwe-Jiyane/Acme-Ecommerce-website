using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Acme_Database_Utility;
using Acme_Ecommerce.Models;

namespace Acme_Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {


            DatabaseUtil util = new DatabaseUtil();
            List<ProductModel> list = new List<ProductModel>();


            foreach (ProductSchema item in util.GetProducts())
            {
                list.Add(new ProductModel()
                {
                    id = item.id,
                    name = item.name,
                    type = item.type,
                    price = item.price
                });
            }

            return View(list);
        }


        public ActionResult Add()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Add(ProductModel product)
        {

            DatabaseUtil util = new DatabaseUtil();

            bool created = util.Insert(product);

            if (created)
            {
                ViewBag.Bool = true;
                ViewBag.Message = "Product created successfully";
            }
            else
            {
                ViewBag.Bool = false;
                ViewBag.Message = "Could not create product";
            }

            return View();
        }  


        [HttpGet]
        public ActionResult BuyProduct(ProductModel model)
        {
            ProductModel product = model;

            return View(product);
        }


        public ActionResult Checkout(ProductModel model)
        {

            UserModel user = (UserModel)Session["user"];
            DatabaseUtil util = new DatabaseUtil();
            
            if (model.price == 0)
            {
                ViewBag.Bool = false;
                return View();
            }

            try
            {
                if (user.GetCart().Count > 0)
                {
                    ViewBag.Bool = true;
                    List<ProductModel> list = user.GetCart();
                    return View(list);
                }
                else
                {

                    util.InsertOrder(user.Email, model.name);
                    ViewBag.Bool = true;
                    user.SetCart(model);
                    List<ProductModel> list = new List<ProductModel>() { model };
                    return View(list);
                }
            }
            catch (Exception)
            {
                ViewBag.Bool = false;
                return View();
            }

            
        }

        public ActionResult OrderHistory()
        {

            try
            {
                DatabaseUtil util = new DatabaseUtil();
                UserModel user = (UserModel)Session["user"];

                List<ProductSchema> list = util.GetOrders(user.Email);

                if(list.Count == 0)
                {
                    ViewBag.Bool = false;
                    return View();
                }
                else
                {
                    ViewBag.Bool = true;
                    List<ProductModel> products = new List<ProductModel>();
                    foreach(ProductSchema item in list)
                    {

                        products.Add(new ProductModel()
                        {
                            id = item.id,
                            name = item.name,
                        });

                    }

                    return View(products);
                }



            }
            catch (Exception)
            {
                ViewBag.Bool = false;
                return View();
            }
        }

        public ActionResult AdminPage()
        {


            DatabaseUtil util = new DatabaseUtil();

            List<ProductModel> list = new List<ProductModel>();


            foreach (ProductSchema item in util.GetAllOrder())
            {
                list.Add(new ProductModel()
                {
                    name = item.name,
                    id = item.id

                });
            }
            return View(list);
        }

        public ActionResult SearchByDepartment(string type)
        {

            List<ProductSchema> list = new List<ProductSchema>();

            try
            {
                DatabaseUtil util = new DatabaseUtil();

                list.Add(util.GetProducts().Find(prod => prod.type == type));

                foreach(ProductSchema item in list)
                {
                    if(item == null)
                    {
                        ViewBag.Bool = false;
                        return View();
                    }
                }

                ViewBag.Bool = true;

                List<ProductModel> products = new List<ProductModel>();

                foreach(ProductSchema item in list)
                {
                    products.Add(new ProductModel()
                    {
                        id = item.id,
                        name = item.name,
                        price = item.price,
                        type = item.type
                    });
                }
                return View(products);

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}