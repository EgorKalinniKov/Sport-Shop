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
        List<UserMinimal> GetAllUsersActionFlow();
        BaseResponces ValidateUserCredentialAction(ULoginData ulData);
        BaseResponces RegisterUserActionFlow(URegisterData uData);
        BaseResponces CheckIfUserBannedActionFlow(UserMinimal data);
        BaseResponces GenerateUserSessionActionFlow(ULoginData ulData);
        void CloseUserSessionActionFlow(string cookie);
        BaseResponces RegisterUReviewActionFlow(RRegisterData rData);
        HttpCookie GenCookie(string loginCredential);
        UserMinimal GetUserByCookie(string apiCookieValue);
        List<ProdMin> GetUserCartActionFlow(int? id);
        List<ProdMin> GetUserFavActionFlow(int? id);
        BaseResponces AddItemToCartActionFlow(string Art, int? id);
        BaseResponces AddItemToFavActionFlow(string Art, int? id);
        BaseResponces RemoveItemFromCartActionFlow(string Art, int? id);
        BaseResponces RemoveItemFromFavActionFlow(string Art, int? id);
        BaseResponces EditUserActionFlow(UserEdit data);
        void DeleteUserActionFlow(int id);
    }
}
