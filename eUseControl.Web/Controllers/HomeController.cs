using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
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

        public ActionResult Store(ProdStore data)
        {

            data.Products = _product.GetAllProductsActionFlow();

            if(!String.IsNullOrEmpty(data.SelectedCategory) && (data.SelectedCategory != "All"))
                data.Products = data.Products.Where(x => x.Category == data.SelectedCategory).ToList();

            switch(data.SelectedCategory)
            {
                case "Swimming":    ViewBag.Swimming = "active"; break;
                case "Basketball":  ViewBag.Basketball = "active"; break;
                case "Football":    ViewBag.Football = "active"; break;
                case "Volleyball":  ViewBag.Volleyball = "active"; break;
                case "Gym":         ViewBag.Gym = "active"; break;
            }

            if (!String.IsNullOrEmpty(data.SelectedBrend))
                data.Products = data.Products.Where(x => x.Brend == data.SelectedBrend).ToList();

            if (!String.IsNullOrEmpty(data.SelectedTag))
                data.Products = data.Products.Where(x => x.Tag == data.SelectedTag).ToList();

            if (!String.IsNullOrEmpty(data.SelectedName))
                data.Products = data.Products.Where(x => x.Name.Contains(data.SelectedName) || x.Article.Contains(data.SelectedName)).ToList();

            switch (data.Sort)
            {
                case "FromAToZ":        data.Products = data.Products.OrderBy(x => x.Name).ToList();                      break;
                case "FromZToA":        data.Products = data.Products.OrderByDescending(x => x.Name).ToList();            break;
                case "HightToLowPrice": data.Products = data.Products.OrderBy(x => x.Price).ToList();                     break;
                case "LowToHightPrice": data.Products = data.Products.OrderByDescending(x => x.Price).ToList();           break;
                case "HightToLowRate":  data.Products = data.Products.OrderBy(x => x.AvarageRating).ToList();             break;
                case "LowToHightRate":  data.Products = data.Products.OrderByDescending(x => x.AvarageRating).ToList();   break;
                default:                data.Products = data.Products.OrderBy(x => x.Name).ToList();                      break;
            }

            return View(data);
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


    }
}