using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.Review;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;
using eUseControl.Web.Attribute;
using eUseControl.Web.Extension;
using eUseControl.Web.Models;
using eUseControl.Web.Models.Product;
using eUseControl.Web.Models.User;

namespace eUseControl.Web.Controllers
{
    public class HomeController : BaseController
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
            SessionStatus();
            ViewBag.CurrentPage = "Index";
            ViewBag.NewProducts = _product.GetNewProductsActionFlow();
            ViewBag.TopProducts = _product.GetTopProductsActionFlow();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return View();
            }

            int UserId = System.Web.HttpContext.Current.GetMySessionObject().Id;
            ViewBag.Cart = _session.GetUserCartActionFlow(UserId);
            ViewBag.CartCount = ViewBag.Cart.Count;
            ViewBag.Fav = _session.GetUserFavActionFlow(UserId);
            return View();
        }

        public ActionResult Product(string Art)
        {
            SessionStatus();
            ViewBag.CurrentPage = "Product";
            ViewBag.Product = _product.GetProductByArticleActionFlow(Art);
            ViewBag.ProductImgs = _product.GetProductImgsActionFlow(Art);
            ViewBag.ProdReview = _product.GetProductReviewsActionFlow(Art);
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return View();
            }

            ViewBag.UserId = System.Web.HttpContext.Current.GetMySessionObject().Id;
            ViewBag.Role = System.Web.HttpContext.Current.GetMySessionObject().Level;
            ViewBag.Cart = _session.GetUserCartActionFlow(ViewBag.UserId);
            ViewBag.Fav = _session.GetUserFavActionFlow(ViewBag.UserId);
            return View(new ProdStore());
        }

        public ActionResult Store(string category)
        {
            SessionStatus();
            ViewBag.CurrentPage = "Store";
            var data = new ProdStore();
            data.Products = _product.GetAllProductsActionFlow();
            data.SelectedCategory = category;
            if (!String.IsNullOrEmpty(data.SelectedCategory) && (data.SelectedCategory != "All"))
                data.Products = data.Products.Where(x => x.Category == data.SelectedCategory).ToList();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return View(data);
            }

            int UserId = System.Web.HttpContext.Current.GetMySessionObject().Id;
            ViewBag.Cart = _session.GetUserCartActionFlow(UserId);
            ViewBag.Fav = _session.GetUserFavActionFlow(UserId);
            return View(data);
        }

        [HttpPost]
        public ActionResult Store(ProdStore data)
        {
            data.Products = _product.GetAllProductsActionFlow();

            if (!String.IsNullOrEmpty(data.SelectedCategory) && (data.SelectedCategory != "All"))
                data.Products = data.Products.Where(x => x.Category == data.SelectedCategory).ToList();

            if (!String.IsNullOrEmpty(data.SelectedBrend))
                data.Products = data.Products.Where(x => x.Brend == data.SelectedBrend).ToList();

            if (!String.IsNullOrEmpty(data.SelectedTag))
                data.Products = data.Products.Where(x => x.Tag == data.SelectedTag).ToList();

            if (!String.IsNullOrEmpty(data.SelectedName))
                data.Products = data.Products.Where(x => x.Name.Contains(data.SelectedName) || x.Article.Contains(data.SelectedName)).ToList();

            switch (data.Sort)
            {
                case "FromAToZ": data.Products = data.Products.OrderBy(x => x.Name).ToList(); break;
                case "FromZToA": data.Products = data.Products.OrderByDescending(x => x.Name).ToList(); break;
                case "HightToLowPrice": data.Products = data.Products.OrderBy(x => x.Price).ToList(); break;
                case "LowToHightPrice": data.Products = data.Products.OrderByDescending(x => x.Price).ToList(); break;
                case "HightToLowRate": data.Products = data.Products.OrderBy(x => x.AvarageRating).ToList(); break;
                case "LowToHightRate": data.Products = data.Products.OrderByDescending(x => x.AvarageRating).ToList(); break;
                default: data.Products = data.Products.OrderBy(x => x.Name).ToList(); break;
            }

            return View(data);
        }
    }
}