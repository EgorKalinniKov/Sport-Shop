using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BuisnessLogic.Interfaces
{
    public interface IProduct
    {
        List<ProdMin> GetAllProductsActionFlow();
        List<ProdMin> GetNewProductsActionFlow();
        List<ProdMin> GetTopProductsActionFlow();
        ProdMin GetProductByArticleActionFlow(string art);
        List<ReviewData> GetProductReviewsActionFlow(string art);
        List<string> GetProductImgsActionFlow(string art);
        ProdMin GetProductActionFlow(string Cred);
    }
}
