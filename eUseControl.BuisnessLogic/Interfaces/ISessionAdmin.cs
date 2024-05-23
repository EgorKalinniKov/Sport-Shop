﻿using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BuisnessLogic.Interfaces
{
    public interface ISessionAdmin
    {
        BaseResponces RegisterProductActionFlow(PRegisterData uData);
        BaseResponces DeleteProductActionFlow(string pData);
        BaseResponces DeleteReviewActionFlow(string Msg);
    }
}
