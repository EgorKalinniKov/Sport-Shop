using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.User;
using eUseControl.Web.Attribute;
using eUseControl.Web.Models.Product;
using eUseControl.Web.Models.User;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ISessionAdmin _admin;
        private readonly IProduct _product;
        public AdminController()
        {
            var bl = new BuisnessLogic.BuisnessLogic();
            _admin = bl.GetSessionAdminBL();
            _product = bl.GetProductBL();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NewProduct()
        {
            return View(new ProductRegistration());
        }
        public ActionResult Products()
        {
            var data = new ProdSearch();
            data.Products = _product.GetAllProductsActionFlow();
            return View(data);
        }
        public ActionResult DeleteProduct()
        {
            var data = new ProdSearch();
            data.Products = _product.GetAllProductsActionFlow();
            return View(data);
        }
        public ActionResult Users()
        {
            var data = new UserSearch();
            //data.Users = _product.GetAllProductsActionFlow();
            return View(data);
        }
        public ActionResult BanUser()
        {
            var data = new UserSearch();
            //data.Users = _product.GetAllProductsActionFlow();
            return View(data);
        }
        public ActionResult Comments()
        {
            var data = new ReviewSearch();
            //data.Reviews = _product.GetAllProductsActionFlow();
            return View(data);
        }
        public ActionResult Product(string Art)
        {
            ViewBag.Product = _product.GetProductByArticleActionFlow(Art);
            ViewBag.ProductImgs = _product.GetProductImgsActionFlow(Art);
            ViewBag.ProdReview = _product.GetProductReviewsActionFlow(Art);
            return View();
        }

        [HttpPost]
        public ActionResult DeleteProduct(ProdSearch data)
        {
            data.Products = _product.GetAllProductsActionFlow();

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
        [HttpPost]
        public ActionResult Products(ProdSearch data)
        {
            data.Products = _product.GetAllProductsActionFlow();

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
        /*[HttpPost]
        public ActionResult Users(UserSearch data)
        {
            data.Users = _product.GetAllProductsActionFlow();

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
        [HttpPost]
        public ActionResult BanUser(UserSearch data)
        {
            data.Products = _product.GetAllProductsActionFlow();

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
        [HttpPost]
        public ActionResult Comments(ReviewSearch data)
        {
            data.Products = _product.GetAllProductsActionFlow();

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
        }*/
        [AdminMod]
        [HttpPost]
        public ActionResult ProductRegistration(ProductRegistration data)
        {
            List<string> Imgs = new List<string>();
            string directory = "";
            foreach (var img in data.Imgs)
            {
                if (img != null)
                {
                    string ImageName = System.IO.Path.GetFileName(img.FileName);
                    string path = Server.MapPath("~/Content/Images/" + data.Article);
                    // save image in folder
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                        directory = path;
                    }
                    img.SaveAs(Path.Combine(@path, ImageName));
                    Imgs.Add("/Content/Images/" + data.Article + '/' + ImageName);
                }
            }

            var pData = new PRegisterData
            {
                Name = data.Name,
                Article = data.Article,
                Description = data.Description,
                Price = data.Price,
                Brend = data.Brend,
                Category = data.Category,
                Tag = data.Tag,
                Images = Imgs,
                Directory = directory,
                AvailableStatus = data.AvailableStatus,
            };

            BaseResponces resp = _admin.RegisterProductActionFlow(pData);
            Imgs = null;
            return RedirectToAction("NewProduct", "Admin");
        }

        [AdminMod]
        [HttpPost]
        public ActionResult ProductEdit(ProductRegistration data)
        {
            var pData = new PRegisterData
            {
                Name = data.Name,
                Article = data.Article,
                Description = data.Description,
                Discount = data.Discount,
                Price = data.Price,
                Brend = data.Brend,
                Category = data.Category,
                Tag = data.Tag,
                AvailableStatus = data.AvailableStatus,
            };
            BaseResponces resp = _admin.EditProductActionFlow(pData);
            return RedirectToAction("Product", "Admin", new {Art = data.Article});
        }

        [AdminMod]
        [HttpPost]
        public ActionResult ProductDelete(string Art)
        {
            BaseResponces resp = _admin.DeleteProductActionFlow(Art);
            return RedirectToAction("DeleteProduct", "Admin");
        }

        [AdminMod]
        [HttpPost]
        public ActionResult DeleteReview(int? id, string Art)
        {
            BaseResponces resp = _admin.DeleteReviewActionFlow(id);
            if(!String.IsNullOrEmpty(Art))
            return RedirectToAction("Product", "Admin", new { Art = Art });
            return RedirectToAction("Comments", "Admin");
        }

        [AdminMod]
        [HttpPost]
        public ActionResult UserBan()
        {
            return null;
        }

        [AdminMod]
        [HttpPost]
        public ActionResult UserEdit()
        {
            return null;
        }
    }
}