using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.BuisnessLogic.Core;
using eUseControl.BuisnessLogic.Core.Levels;
using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.Domain.Entities.Product;

namespace eUseControl.BuisnessLogic.MainBL
{
    public class ProductBL : UserApi, IProduct
    {
        public ProductDataModel GetProductsToList()
        {
            return ProductActionGetToList();
        }

        public ProductDataModel GetSingleProduct(int id)
        {
            return ProductGetSingleAction(id);
        }
    }
}
