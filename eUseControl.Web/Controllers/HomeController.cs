using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.Web.Attribute;
using eUseControl.Web.Models;
using eUseControl.Web.Models.User;

namespace eUseControl.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            UserData u = new UserData();
            u.Username = "Customer";
            u.Products = new List<string> { "Product #1", "Product #2", "Product #3", "Product #4", "Product #5", "Product #6" };

            return View(u);
        }

        public ActionResult Product()
        {
            var product = Request.QueryString["p"];

            UserData u = new UserData();
            u.Username = "Customer";
            u.SingleProduct = product;

            return View(u);
        }

        public ActionResult Store()
        {
            return View();
        }
        [AdminMod]
        public ActionResult SuccessfulOperation()
        {
            return View();
        }
    }
}