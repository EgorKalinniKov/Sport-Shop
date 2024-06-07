using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.BuisnessLogic.MainAPI;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.Review;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
        public BaseResponces CheckIfUserBannedActionFlow(UserMinimal data)
        {
            return CheckIfUserBanned(data);
        }
        public BaseResponces GenerateUserSessionActionFlow(ULoginData ulData)
        {
            return GenerateUserSession(ulData);
        }
        public BaseResponces RegisterUReviewActionFlow(RRegisterData rData)
        {
            return RegisterUReviewAction(rData);
        }
        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }
        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }
        public List<ProdMin> GetUserCartActionFlow(int? id)
        {
            return GetUserCartAction(id);
        }
        public List<ProdMin> GetUserFavActionFlow(int? id)
        {
            return GetUserFavAction(id);
        }
        public BaseResponces AddItemToCartActionFlow (string Art, int? id)
        {
            return AddItemToCartActionFlow(Art, id);
        }
        public BaseResponces AddItemToFavActionFlow(string Art, int? id)
        {
            return AddItemToFavActionFlow(Art, id);
        }
        public BaseResponces RemoveItemFromCartActionFlow(string Art, int? id)
        {
            return RemoveItemFromCartAction(Art, id);
        }
        public BaseResponces RemoveItemFromFavActionFlow(string Art, int? id)
        {
            return RemoveItemFromFavActionFlow(Art, id);
        }
    }
}
