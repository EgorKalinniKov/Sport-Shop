using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BuisnessLogic.Core;
using eUseControl.BuisnessLogic.Core.Levels;
using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.Domain.Entities.Auth;
using eUseControl.Domain.Entities.GeneralResponce;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Entities.User.DBModel;

namespace eUseControl.BuisnessLogic.MainBL
{
    public class SessionBL : UserApi, ISession
    {
        public GeneralResponce UserLoginAction(ULoginData data)
        {
            return ULASessionCheck(data);
        }
        public UCoockieData GenCoockieAlgo(User dataUser)
        {
            return UserCoockieGenerationAlg(dataUser);
        }
    }
}
