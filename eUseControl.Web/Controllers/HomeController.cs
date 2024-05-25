using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.Review;
using eUseControl.Web.Attribute;
using eUseControl.Web.Extension;
using eUseControl.Web.Models;
using eUseControl.Web.Models.Product;
using eUseControl.Web.Models.User;

namespace eUseControl.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession _session;
        private readonly IProduct _product;
        private readonly ISessionAdmin _session_admin;
        public HomeController()
        {
            var bl = new BuisnessLogic.BuisnessLogic();
            _session = bl.GetSessionBL();
            _product = bl.GetProductBL();
            _session_admin = bl.GetSessionAdminBL();
        }
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Products = _product.GetAllProductsActionFlow();
            ViewBag.NewProducts = _product.GetNewProductsActionFlow();
            ViewBag.TopProducts = _product.GetTopProductsActionFlow();

            return View();
        }

        public ActionResult Product(string Art)
        {
            ViewBag.Product = _product.GetProductByArticleActionFlow(Art);
            ViewBag.ProductImgs = _product.GetProductImgsActionFlow(Art);
            ViewBag.ProdReview = _product.GetProductReviewsActionFlow(Art);
            return View();
        }

        public ActionResult Store()
        {
            ViewBag.Products =  _product.GetAllProductsActionFlow();
            return View();
        }
        [HttpPost]
        public ActionResult AddReview(ReviewData data)
        {
            var rData = new RRegisterData
            {
                UserId = 1,
                Article = "1",
                Message = data.Message,
                Rate = data.Rate,
            };

            return null; 
        }

    }
}