using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.User;
using eUseControl.Web.Models;
using eUseControl.Web.Models.Authorization;


namespace eUseControl.Web.Controllers
{
    public class LoginController : Controller
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
            return View(new LoginData());
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
        public ActionResult Login(LoginData data)
        {
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
            }
            return null;
        }

        [HttpPost]
        public ActionResult Register(UActionRegister data) 
        {
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

            return RedirectToAction("Index", "Home");
        }
    }
}