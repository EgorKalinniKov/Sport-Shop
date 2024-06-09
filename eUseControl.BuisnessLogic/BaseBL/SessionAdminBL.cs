using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.BuisnessLogic.MainAPI;
using eUseControl.Domain.Entities.Administration;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BuisnessLogic.BaseBL
{
    public class SessionAdminBL : AdminAPI, ISessionAdmin
    {
        public BaseResponces RegisterProductActionFlow(PRegisterData pData)
        {
            return RegisterProdAction(pData);
        }
        public BaseResponces EditProductActionFlow(PRegisterData pData)
        {
            return EditProdAction(pData);
        }
        public BaseResponces DeleteProductActionFlow(string pData)
        {
            return DeleteProdAction(pData);
        }
        public BaseResponces BanUserActionFlow(BanedUser data)
        {
            return BanUserAction(data);
        }
        public BaseResponces EditUserActionFlow(UserEdit data)
        {
            return EditUserAction(data);
        }
    }
}
