using eUseControl.BuisnessLogic.DbModel;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Review;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls.WebParts;

namespace eUseControl.BuisnessLogic.MainAPI
{
    public class ProductApi
    {
        internal List<ProdMin> GetAllProducts()
        {
            List<ProdMin> ListP = new List<ProdMin>();
            using (var db = new ProductContext())
            {
                var products = db.Products;
                foreach (var p in products)
                {
                    var prodMin = new ProdMin()
                    {
                        Id = p.Id,
                        Article = p.Article,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        AvarageRating = p.AvarageRating,
                        TotalRatings = p.TotalRatings,
                        Discount = p.Discount,
                        Directory = p.Directory,
                        AvailableStatus = p.AvailableStatus,
                        Brend = p.Brend,
                        Category = p.Category,
                        Tag = p.Tag,
                        Image = p.Image,
                    };
                    ListP.Add(prodMin);
                }
            }
            return ListP;
        }
        internal ProdMin GetProduct(string Cred)
        {
            PDbTable product = null;
            using (var db = new ProductContext())
            {
                product = db.Products.FirstOrDefault(x => x.Article == Cred);
            }
            var prodMin = new ProdMin()
            {
                Id = product.Id,
                Article = product.Article,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                TotalRatings= product.TotalRatings,
                AvarageRating= product.AvarageRating,
                Directory = product.Directory,
                AvailableStatus = product.AvailableStatus,
                Brend = product.Brend,
                Category = product.Category,
                Tag = product.Tag,
                Image = product.Image,
            };
            prodMin.Imgs = new List<string>();
            using (var db = new ImgContext())
            {
                var imgs = db.Imgs.Where(x => x.ProdArticle == prodMin.Article).ToList();
                foreach (var i in imgs)
                {
                    prodMin.Imgs.Add(i.Img);
                }
            }
            return prodMin;
        }
        internal List<ProdMin> GetNewProducts()
        {
            List<ProdMin> ListP = new List<ProdMin>();
            using (var db = new ProductContext())
            {
                var products = db.Products.OrderByDescending(x => x.Id);
                int k = 0;
                foreach (var p in products)
                {
                    if (k < 10)
                    {
                        var prodMin = new ProdMin()
                        {
                            Id = p.Id,
                            Article = p.Article,
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Price,
                            Discount = p.Discount,
                            AvarageRating = p.AvarageRating,
                            TotalRatings = p.TotalRatings,
                            Directory = p.Directory,
                            AvailableStatus = p.AvailableStatus,
                            Brend = p.Brend,
                            Category = p.Category,
                            Tag = p.Tag,
                            Image = p.Image,
                        };
                        ListP.Add(prodMin);
                    }
                    else break;
                }
            }
            return ListP;
        }
        internal List<ProdMin> GetTopProducts()
        {
            List<ProdMin> ListP = new List<ProdMin>();
            using (var db = new ProductContext())
            {
                var products = db.Products.OrderByDescending(x => x.AvarageRating);
                int k = 0;
                foreach (var p in products)
                {
                    if (k < 10)
                    {
                        var prodMin = new ProdMin()
                        {
                            Id = p.Id,
                            Article = p.Article,
                            Name = p.Name,
                            Description = p.Description,
                            Price = p.Price,
                            TotalRatings = p.TotalRatings,
                            AvarageRating = p.AvarageRating,
                            Discount = p.Discount,
                            Directory = p.Directory,
                            AvailableStatus = p.AvailableStatus,
                            Brend = p.Brend,
                            Category = p.Category,
                            Tag = p.Tag,
                            Image = p.Image,
                        };
                        ListP.Add(prodMin);
                    }
                    else break;
                }
            }
            return ListP;
        }
        internal ProdMin GetProductByArticle(string data)
        {
            ProdMin Prod = null;
            PDbTable local = null;
            using (var db = new ProductContext())
            {
                local = db.Products.FirstOrDefault(x => x.Article.Contains(data));
            }
            if(local != null)
            {
                Prod = new ProdMin
                {
                    Id = local.Id,
                    Article = local.Article,
                    Name = local.Name,
                    Description = local.Description,
                    Price = local.Price,
                    Discount = local.Discount,
                    AvarageRating = local.AvarageRating,
                    TotalRatings = local.TotalRatings,
                    Directory = local.Directory,
                    AvailableStatus = local.AvailableStatus,
                    Brend = local.Brend,
                    Category = local.Category,
                    Tag = local.Tag,
                    Image = local.Image,
                };
            }
            return Prod;
        }
        internal List<ReviewData> GetProductReviews(string art)
        {
            List<ReviewData> ListR = new List<ReviewData>();
            using (var db = new ReviewContext())
            {
                var reviews = db.Reviews.Where(x => x.Article == art).ToList();
                if (reviews != null)
                    foreach (var r in reviews)
                    {
                        var Rev = new ReviewData
                        {
                            Id = r.ReviewId,
                            UserId = r.UserId,
                            Username = r.Username,
                            Message = r.Message,
                            Rate = r.Rate,
                            DateEdited = r.DateEdited,
                        };
                        ListR.Add(Rev);
                    }
            }
            return ListR;
        }
        internal List<ReviewData> GetAllComments()
        {
            List<ReviewData> ListR = new List<ReviewData>();
            using (var db = new ReviewContext())
            {
                var reviews = db.Reviews;
                if (reviews != null)
                    foreach (var r in reviews)
                    {
                        var Rev = new ReviewData
                        {
                            Id = r.ReviewId,
                            UserId = r.UserId,
                            Username = r.Username,
                            Message = r.Message,
                            Rate = r.Rate,
                            DateEdited = r.DateEdited,
                        };
                        ListR.Add(Rev);
                    }
            }
            return ListR;
        }
        internal List<string> GetProductImgs(string art)
        {
            List<string> ListI = new List<string>();
            using (var db = new ImgContext())
            {
                var imgs = db.Imgs.Where(x => x.ProdArticle == art).ToList();
                if (imgs != null)
                    foreach (var i in imgs)
                    {
                        ListI.Add(i.Img);
                    }
            }
            return ListI;
        }
        internal List<string> GetProdImgs(string Article)
        {
            List<PImgTable> local = null;
            using (var db = new ImgContext())
            {
                local = db.Imgs.Where(x => x.ProdArticle == Article).ToList();
            }

            List<string> imgs = new List<string>();
            foreach (var p in local)
            {
                imgs.Add(p.Img);
            }
            return imgs;
        }
        internal List<ProdMin> SortProducts(ProdSort list)
        {
            switch (list.SortOrder)
            {
                case "FromAToZ": list.ListP.Sort(delegate (ProdMin x, ProdMin y) { return x.Name.CompareTo(y.Name); }); break;
                case "FromZToA": list.ListP.Sort(delegate (ProdMin x, ProdMin y) { return y.Name.CompareTo(x.Name); }); break;
                case "HightToLowPrice": list.ListP.Sort(delegate (ProdMin x, ProdMin y) { return x.Price.CompareTo(y.Price); }); break;
                case "LowToHightPrice": list.ListP.Sort(delegate (ProdMin x, ProdMin y) { return y.Price.CompareTo(x.Price); }); break;
                case "HightToLowRate": list.ListP.Sort(delegate (ProdMin x, ProdMin y) { return x.AvarageRating.CompareTo(y.AvarageRating); }); break;
                case "LowToHightRate": list.ListP.Sort(delegate (ProdMin x, ProdMin y) { return y.AvarageRating.CompareTo(x.AvarageRating); }); break;
            }
            return list.ListP;
        }
        internal List<ProdMin> FilterProductsBy(ProdFilter list)
        {
            List<ProdMin> ListP = new List<ProdMin>();
            using (var db = new ProductContext())
            {
                var products = db.Products.ToList();

                if (list.Brend != null)
                    products = products.Where(x => x.Brend == list.Brend).ToList();
                if (list.Tag != null)
                    products = products.Where(x => x.Tag == list.Tag).ToList();

                products = products.Where(x => x.Price - x.Price/100 * x.Discount < list.HighPrice &&
                                                x.Price - x.Price / 100 * x.Discount > list.LowPrice).ToList();
                foreach (var p in products)
                {
                    var prodMin = new ProdMin()
                    {
                        Id = p.Id,
                        Article = p.Article,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        AvarageRating = p.AvarageRating,
                        TotalRatings = p.TotalRatings,
                        Discount = p.Discount,
                        Directory = p.Directory,
                        AvailableStatus = p.AvailableStatus,
                        Brend = p.Brend,
                        Category = p.Category,
                        Tag = p.Tag,
                        Image = p.Image,
                    };
                    ListP.Add(prodMin);
                }
            }
            return ListP;
        }
        internal List<ProdMin> SearchProducts(PSearch data)
        {
            List<ProdMin> ListP = new List<ProdMin>();
            using (var db = new ProductContext())
            {
                var products = db.Products.Where(x => x.Name.Contains(data.Credential) || x.Article.Contains(data.Credential)).ToList();
                if(data.Category != "All")
                    products = products.Where(x => x.Category == data.Category).ToList();    
                foreach (var p in products)
                {
                    var prodMin = new ProdMin()
                    {
                        Id = p.Id,
                        Article = p.Article,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        AvarageRating = p.AvarageRating,
                        TotalRatings = p.TotalRatings,
                        Discount = p.Discount,
                        Directory = p.Directory,
                        AvailableStatus = p.AvailableStatus,
                        Brend = p.Brend,
                        Category = p.Category,
                        Tag = p.Tag,
                        Image = p.Image,
                    };
                    ListP.Add(prodMin);
                }
            }
            return ListP;
        }
    }
}
