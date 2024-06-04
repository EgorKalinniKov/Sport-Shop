using eUseControl.BuisnessLogic.Interfaces;
using eUseControl.BuisnessLogic.MainAPI;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eUseControl.BuisnessLogic.BaseBL
{
    public class ProductBL : ProductApi, IProduct
    {
        public List<ProdMin> GetAllProductsActionFlow()
        {
            return GetAllProducts();
        }
        public List<ProdMin> GetNewProductsActionFlow()
        {
            return GetNewProducts();
        }
        public List<ProdMin> GetTopProductsActionFlow()
        {
            return GetTopProducts();
        }
        public ProdMin GetProductByArticleActionFlow(string Cred)
        {
            return GetProductByArticle(Cred);
        }
        public List<ReviewData> GetProductReviewsActionFlow(string art)
        {
            return GetProductReviews(art);
        }
        public List<ReviewData> GetAllCommentsActionFlow()
        {
            return GetAllComments();
        }
        public List<string> GetProductImgsActionFlow(string art)
        {
            return GetProductImgs(art);
        }
        public ProdMin GetProductActionFlow(string Cred)
        {
            return GetProduct(Cred);
        }
        public List<ProdMin> SortProductsAction(ProdSort list)
        {
            return SortProducts(list);
        }
        public List<ProdMin> FilterProductsByAction(ProdFilter list)
        {
            return FilterProductsBy(list);
        }
        public List<ProdMin> SearchProductsAction(PSearch Cred)
        {
            return SearchProducts(Cred);
        }
    }
}
