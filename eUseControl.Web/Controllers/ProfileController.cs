using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.Review;
using eUseControl.Domain.Entities.User;
using eUseControl.Web.Extension;
using eUseControl.Web.Models.Product;
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
            return View();
        }
        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult Cart()
        {
            SessionStatus();
            int id = System.Web.HttpContext.Current.GetMySessionObject().Id;
            ViewBag.Fav = _session.GetUserFavActionFlow(id);
            return View();
        }
        public ActionResult Favourites()
        {
            SessionStatus();
            int id = System.Web.HttpContext.Current.GetMySessionObject().Id;
            ViewBag.Fav = _session.GetUserFavActionFlow(id);
            return View();
        }
        public ActionResult Settings()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddToCart(string Art, string page)
        {
            SessionStatus();
            int UserId = System.Web.HttpContext.Current.GetMySessionObject().Id;
            BaseResponces resp = _session.AddItemToCartActionFlow(Art, UserId);
            if (!resp.Status) ModelState.AddModelError("", resp.StatusMessage);
            if (page == "Product")
                return RedirectToAction(page, "Home", new { Art = Art });
            return RedirectToAction(page, "Home");
        }
        [HttpPost]
        public ActionResult AddToFav(string Art, string page)
        {
            SessionStatus();
            int UserId = System.Web.HttpContext.Current.GetMySessionObject().Id;
            BaseResponces resp = _session.AddItemToFavActionFlow(Art, UserId);
            if (!resp.Status) ModelState.AddModelError("", resp.StatusMessage);
            if (page == "Product")
                return RedirectToAction(page, "Home", new { Art = Art });
            return RedirectToAction(page, "Home");
        }
        [HttpPost]
        public ActionResult DeleteFromCart(string Art, string page)
        {
            SessionStatus();
            int UserId = System.Web.HttpContext.Current.GetMySessionObject().Id;
            BaseResponces resp = _session.RemoveItemFromCartActionFlow(Art, UserId);
            if (!resp.Status) ModelState.AddModelError("", resp.StatusMessage);
            if (page == "Product")
                return RedirectToAction(page, "Home", new { Art = Art });
            return RedirectToAction(page, "Home");
        }
        [HttpPost]
        public ActionResult DeleteFromFav(string Art, string page)
        {
            SessionStatus();
            int UserId = System.Web.HttpContext.Current.GetMySessionObject().Id;
            BaseResponces resp = _session.RemoveItemFromFavActionFlow(Art, UserId);
            if (!resp.Status) ModelState.AddModelError("", resp.StatusMessage);
            if (page == "Product")
                return RedirectToAction(page, "Home", new { Art = Art });
            return RedirectToAction(page, "Home");
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AddReview(ReviewReg data)
        {
            if (ModelState.IsValid)
            {
                SessionStatus();
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
                        Article = data.Article,
                        Message = data.Message,
                        Rate = data.Rate,
                    };

                    BaseResponces resp = _session.RegisterUReviewActionFlow(rData);
                    if (resp.Status) ModelState.AddModelError("", check.StatusMessage);
                }
                else ModelState.AddModelError("", check.StatusMessage);
            }
            return RedirectToAction("Product", "Home", new { Art = data.Article });
        }
    }
}