using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.User;
using eUseControl.Web.Attribute;
using eUseControl.Web.Models.Product;
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
            return View();
        }
        public ActionResult Products()
        {
            ViewBag.Products = _product.GetAllProductsActionFlow();
            return View();
        }
        public ActionResult DeleteProduct()
        {
            ViewBag.Products = _product.GetAllProductsActionFlow();
            return View();
        }
        public ActionResult Users()
        {
            return View();
        }
        public ActionResult BanUser()
        {
            return View();
        }
        public ActionResult Comments()
        {
            ViewBag.ProdReview = _product.GetAllCommentsActionFlow();
            return View();
        }
        public ActionResult Product(string Art)
        {
            ViewBag.Product = _product.GetProductByArticleActionFlow(Art);
            ViewBag.ProductImgs = _product.GetProductImgsActionFlow(Art);
            ViewBag.ProdReview = _product.GetProductReviewsActionFlow(Art);
            return View();
        }

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
            if(Art!=null)
            return RedirectToAction("Product", "Admin", new { Art = Art });
            else
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