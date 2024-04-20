using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.Domain.Entities.Auth;
using eUseControl.Domain.Entities.GeneralResponce;
using eUseControl.Domain.Entities.User;
using eUseControl.Web.Models;


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
            return View();
        }
        public ActionResult Registration()
        {
            return View();
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
        [ValidateAntiForgeryToken]
        public ActionResult Index (LoginData login)
        {

            var uLoginData = new ULoginData
            {
                Credential = login.Username,
                Password = login.Password,
                FirstIp = "",
                FirstLoginTime = DateTime.Now
            };

            GeneralResponce responce = _session.UserLoginAction(uLoginData);
            if (responce != null && responce.Status)
            {
                //here will be the logic to Coockie Generation
                UCoockieData cData = _session.GenCoockieAlgo(responce.CurrentUser);

                if (cData != null)
                {
                    //SET COOCKKE TO USER BROWSER
                }
            }


            return View();
        }
    }
}