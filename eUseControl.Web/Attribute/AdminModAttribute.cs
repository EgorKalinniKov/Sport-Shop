using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.BuisnessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using eUseControl.Web.Extension;

namespace eUseControl.Web.Attribute
{
    public class AdminModAttribute:ActionFilterAttribute
    {
        private readonly ISession _session;
        public AdminModAttribute()
        {
            var bl = new BuisnessLogic.BuisnessLogic();
            _session = bl.GetSessionBL();
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var apiCookie = HttpContext.Current.Request.Cookies["X-KEY"];
            if (apiCookie != null)
            {
                var profile = _session.GetUserByCookie(apiCookie.Value);
                if (profile != null && profile.Level == Domain.Enums.URole.Admin)
                {
                    HttpContext.Current.SetMySessionObject(profile);
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Error", action = "Error404" }));
                }
            }
        }

    }
}