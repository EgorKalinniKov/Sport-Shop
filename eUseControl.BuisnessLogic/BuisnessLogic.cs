using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.BuisnessLogic.MainBL;

namespace eUseControl.BuisnessLogic
{
    public class BuisnessLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }

        public IProduct GetProductBL()
        {
            return new ProductBL();
        }

        public IAdministration GetAdministrationBL()
        {
            return new AdministrationBL();
        }
    }
}
