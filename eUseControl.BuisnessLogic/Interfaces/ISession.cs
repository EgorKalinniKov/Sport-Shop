using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.Auth;
using eUseControl.Domain.Entities.User.DBModel;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Entities.GeneralResponce;

namespace eUseControl.BuisnessLogic.Interfaces
{
    public interface ISession
    {
        GeneralResponce UserLoginAction(ULoginData data);
        UCoockieData GenCoockieAlgo(User dataUser);
    }
}
