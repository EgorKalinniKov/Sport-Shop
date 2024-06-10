using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.Review;
using eUseControl.Domain.Entities.User;
using eUseControl.Web.Extension;
using eUseControl.Web.Models.Product;
using eUseControl.Web.Models.User;
using System.Web.Mvc;

namespace eUseControl.Web.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly ISession _session;
        public ProfileController()
        {
            var bl = new BuisnessLogic.BuisnessLogic();
            _session = bl.GetSessionBL();
        }
        // GET: Profile
        public ActionResult Index()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Id = System.Web.HttpContext.Current.GetMySessionObject().Id;
            ViewBag.Name = System.Web.HttpContext.Current.GetMySessionObject().Username;
            ViewBag.Email = System.Web.HttpContext.Current.GetMySessionObject().Email;
            return View();
        }
        public ActionResult Checkout()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
        public ActionResult Cart()
        {
            SessionStatus();
            ViewBag.CurrentPage = "Cart";
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }
            int id = System.Web.HttpContext.Current.GetMySessionObject().Id;
            ViewBag.Cart = _session.GetUserCartActionFlow(id);
            return View();
        }
        public ActionResult Favourites()
        {
            SessionStatus();
            ViewBag.CurrentPage = "Favourites";
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }
            int id = System.Web.HttpContext.Current.GetMySessionObject().Id;
            ViewBag.Fav = _session.GetUserFavActionFlow(id);
            return View();
        }
        public ActionResult Settings()
        {
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("Index", "Login");
            }
            return View(new UserEditM());
        }
        public ActionResult Logout()
        {
            CloseSession();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult AddToCart(string Art, string page)
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("index", "Login");
            }
            int UserId = System.Web.HttpContext.Current.GetMySessionObject().Id;
            BaseResponces resp = _session.AddItemToCartActionFlow(Art, UserId);
            if (!resp.Status) ModelState.AddModelError("", resp.StatusMessage);
            switch (page)
            {
                case "index": return RedirectToAction(page, "Home");
                case "Cart": return RedirectToAction(page, "Profile");
                case "Favourites": return RedirectToAction(page, "Profile");
            }
            return RedirectToAction(page, "Home", new { Art = Art });
        }
        [HttpPost]
        public ActionResult AddToFav(string Art, string page)
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("index", "Login");
            }
            int UserId = System.Web.HttpContext.Current.GetMySessionObject().Id;
            BaseResponces resp = _session.AddItemToFavActionFlow(Art, UserId);
            if (!resp.Status) ModelState.AddModelError("", resp.StatusMessage);
            switch(page)
            {
                case "index":   return RedirectToAction(page, "Home");
                case "Cart":    return RedirectToAction(page, "Profile");
                case "Favourites":     return RedirectToAction(page, "Profile");
            }
            return RedirectToAction(page, "Home", new { Art = Art });
        }
        [HttpPost]
        public ActionResult DeleteFromCart(string Art, string page)
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("index", "Login");
            }
            int UserId = System.Web.HttpContext.Current.GetMySessionObject().Id;
            BaseResponces resp = _session.RemoveItemFromCartActionFlow(Art, UserId);
            if (!resp.Status) ModelState.AddModelError("", resp.StatusMessage);
            switch (page)
            {
                case "index": return RedirectToAction(page, "Home");
                case "Cart": return RedirectToAction(page, "Profile");
                case "Favourites": return RedirectToAction(page, "Profile");
            }
            return RedirectToAction(page, "Home", new { Art = Art });
        }
        [HttpPost]
        public ActionResult DeleteFromFav(string Art, string page)
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("index", "Login");
            }
            int UserId = System.Web.HttpContext.Current.GetMySessionObject().Id;
            BaseResponces resp = _session.RemoveItemFromFavActionFlow(Art, UserId);
            if (!resp.Status) ModelState.AddModelError("", resp.StatusMessage);
            switch (page)
            {
                case "index": return RedirectToAction(page, "Home");
                case "Cart": return RedirectToAction(page, "Profile");
                case "Favourites": return RedirectToAction(page, "Profile");
            }
            return RedirectToAction(page, "Home", new { Art = Art });
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddReview(ProdStore data)
        {
            if (ModelState.IsValid)
            {
                SessionStatus();
                if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
                {
                    return RedirectToAction("index", "Login");
                }
                var user = new UserMinimal
                {
                    Level = System.Web.HttpContext.Current.GetMySessionObject().Level,
                    Email = System.Web.HttpContext.Current.GetMySessionObject().Email,
                    BanTime = System.Web.HttpContext.Current.GetMySessionObject().BanTime,
                };

                BaseResponces check = _session.CheckIfUserBannedActionFlow(user);

                if (check.Status)
                {
                    var rData = new RRegisterData
                    {
                        UserId = System.Web.HttpContext.Current.GetMySessionObject().Id,
                        Username = System.Web.HttpContext.Current.GetMySessionObject().Username,
                        Article = data.ReviewReg.Article,
                        Message = data.ReviewReg.Message,
                        Rate = data.ReviewReg.Rate,
                    };

                    BaseResponces resp = _session.RegisterUReviewActionFlow(rData);
                    if (resp.Status) ModelState.AddModelError("", check.StatusMessage);
                }
                else ModelState.AddModelError("", check.StatusMessage);
            }
            return RedirectToAction("Product", "Home", new { Art = data.ReviewReg.Article });
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteReview(int id, string Article)
        {
            if (ModelState.IsValid)
            {
                SessionStatus();
                if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
                {
                    return RedirectToAction("index", "Login");
                }
                var user = new UserMinimal
                {
                    Level = System.Web.HttpContext.Current.GetMySessionObject().Level,
                    Email = System.Web.HttpContext.Current.GetMySessionObject().Email,
                    BanTime = System.Web.HttpContext.Current.GetMySessionObject().BanTime,
                };

                BaseResponces check = _session.CheckIfUserBannedActionFlow(user);

                if (check.Status)
                {
                    BaseResponces resp = _session.DeleteReviewActionFlow(id);
                    if (!resp.Status) ModelState.AddModelError("", check.StatusMessage);
                }
                else ModelState.AddModelError("", check.StatusMessage);
            }
            return RedirectToAction("Product", "Home", new { Art = Article });
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Settings(UserEditM data)
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("index", "Login");
            }

            string cred = "";
            switch(data.FormName)
            {
                case "Email":       cred = data.Email; break;
                case "Username":    cred = data.Username; break;
                case "Password":    cred = data.Password; break;
                default: return RedirectToAction("Index", "Profile");
            }

            var user = new UserEdit
            {
                Id = System.Web.HttpContext.Current.GetMySessionObject().Id,
                Form = data.FormName,
                Credential = cred,
            };

            BaseResponces resp = _session.EditUserActionFlow(user);
            return RedirectToAction("Index", "Profile");

        }
        [HttpPost]
        public ActionResult Delete()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
            {
                return RedirectToAction("index", "Login");
            }
            _session.DeleteUserActionFlow(System.Web.HttpContext.Current.GetMySessionObject().Id);
            return RedirectToAction("Index", "Login");
        }
    }
}