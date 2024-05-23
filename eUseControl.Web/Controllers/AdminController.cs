using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.User;
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
        public AdminController()
        {
            var bl = new BuisnessLogic.BuisnessLogic();
            _admin = bl.GetProductBL();
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
            return View();
        }
        public ActionResult DeleteProduct()
        {
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
            return View();
        }
        public ActionResult Product()
        {
            return View();
        }
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
                    string path = Server.MapPath("~/Content/Images/" + data.Name);
                    // save image in folder
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                        directory = path;
                    }
                    img.SaveAs(Path.Combine(@path, ImageName));
                    Imgs.Add(Path.Combine(@path, ImageName));
                }
            }
            var uData = new PRegisterData
            {
                Name = data.Name,
                Article = data.Article,
                Description = data.Description,
                Cost = data.Cost,
                Brend = data.Brend,
                Category = data.Category,
                Tag = data.Tag,
                Images = Imgs,
                Directory = directory,
                AvailableStatus = data.AvailableStatus,
            };

            BaseResponces resp = _admin.RegisterProductActionFlow(uData);
            Imgs = null;
            return null;
        }
    }
}