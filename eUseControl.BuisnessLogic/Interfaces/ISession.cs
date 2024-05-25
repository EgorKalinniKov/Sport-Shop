using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.Review;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace eUseControl.BuisnessLogic.Interfaces
{
    public interface ISession
    {
        BaseResponces ValidateUserCredentialAction(ULoginData ulData);
        BaseResponces RegisterUserActionFlow(URegisterData uData);
        BaseResponces GenerateUserSessionActionFlow(ULoginData ulData);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string apiCookieValue);
    }
}
