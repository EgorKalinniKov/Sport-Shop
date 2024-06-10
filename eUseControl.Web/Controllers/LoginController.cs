using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;
using eUseControl.Web.Extension;
using eUseControl.Web.Models.Authorization;
using System;
using System.Web;
using System.Web.Mvc;


namespace eUseControl.Web.Controllers
{
    public class LoginController : BaseController
    {
        private readonly ISession _session;
        public LoginController()
        {
            var bl = new BuisnessLogic.BuisnessLogic();
            _session = bl.GetSessionBL();
        }
        // GET: Login
        public ActionResult Index()
        {
            SessionStatus();
            if ((string)System.Web.HttpContext.Current.Session["LoginStatus"] != "login")
                return View(new LoginData());

            if (System.Web.HttpContext.Current.GetMySessionObject().Level == URole.Admin)
                return RedirectToAction("Index", "Admin");
            
            return RedirectToAction("Index", "Profile");

        }
        public ActionResult Registration()
        {
            return View(new UActionRegister());
        }
        public ActionResult ForgotPassword1()
        {
            return View();
        }
        public ActionResult ForgotPassword2()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginData data)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Login");

            var adress = base.Request.UserHostAddress;
            var ulData = new ULoginData
            {
                Credential = data.Credential,
                Password = data.Password,
                LastIp = adress,
                LastLogin = DateTime.Now
            };

            BaseResponces resp = _session.ValidateUserCredentialAction(ulData);

            if (resp.Status)
            {
                BaseResponces auth = _session.GenerateUserSessionActionFlow(ulData);
                if (auth.Status)
                {
                    HttpCookie cookie = _session.GenCookie(data.Credential);
                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", auth.StatusMessage);
                    return RedirectToAction("Index", "Login");
                }
            }
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Register(UActionRegister data)
        {
            if (!ModelState.IsValid) return View();

            var adress = base.Request.UserHostAddress;
            var uData = new URegisterData
            {
                Username = data.Username,
                Password = data.Password,
                Email = data.Email,
                LastIp = adress,
                LastLogin = DateTime.Now,
            };

            BaseResponces resp = _session.RegisterUserActionFlow(uData);

            if(resp.Status)
                return RedirectToAction("Index", "Home");
            else
            {
                ModelState.AddModelError("", resp.StatusMessage);
                return RedirectToAction("Registration", "Login");
            }

        }
    }
}