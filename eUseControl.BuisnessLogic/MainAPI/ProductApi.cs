﻿using eUseControl.BuisnessLogic.DbModel;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Review;
using System.Collections.Generic;
using System.Linq;

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
        public List<ReviewData> GetProductReviews(string art)
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
        public List<string> GetProductImgs(string art)
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
    }
}