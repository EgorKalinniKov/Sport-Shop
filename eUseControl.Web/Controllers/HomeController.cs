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

        public ActionResult Store(SearchData data)
        {
            ViewBag.Products = _product.GetAllProductsActionFlow();
            if (data.Credential != null)
            {
                var Search = new PSearch
                {
                    Category = data.Category,
                    Credential = data.Credential,
                };
                ViewBag.Products = _product.SearchProductsAction(Search);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddReview(ReviewReg data)
        {
            var rData = new RRegisterData
            {
                UserId = 1,//System.Web.HttpContext.Current.GetMySessionObject().UserId;
                Username = "2",//System.Web.HttpContext.Current.GetMySessionObject().Username;
                Article = data.Article,
                Message = data.Message,
                Rate = data.Rate,
            };

            BaseResponces resp = _session.RegisterUReviewActionFlow(rData);

            return RedirectToAction("Product", "Home", new { Art = data.Article });
        }
        [HttpPost]
        public ActionResult Store(ProdStore data)
        {
            var Filter = new ProdFilter
            {
                Brend = data.Brend,
                Tag = data.Tag,
                LowPrice = data.LowPrice,
                HighPrice = data.HighPrice,
            };
            ViewBag.Products = _product.FilterProductsByAction(Filter);
            return View();
        }

    }
}