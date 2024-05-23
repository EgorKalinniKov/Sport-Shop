using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.BuisnessLogic.MainAPI;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace eUseControl.BuisnessLogic.BaseBL
{
    public class SessionBL: UserAPI, ISession
    {
        public BaseResponces ValidateUserCredentialAction(ULoginData ulData)
        {
            return CheckUserCredintial(ulData);
        }
        public BaseResponces RegisterUserActionFlow(URegisterData uData)
        {
            return RegisterUserAction(uData);
        }
        public BaseResponces GenerateUserSessionActionFlow(ULoginData ulData)
        {
            return GenerateUserSession(ulData);
        }
        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }
        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }
    }
}
