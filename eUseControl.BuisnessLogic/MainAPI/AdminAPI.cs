using eUseControl.BuisnessLogic.DbModel;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.Responces;
using eUseControl.Domain.Entities.Review;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace eUseControl.BuisnessLogic.MainAPI
{
    public class AdminAPI
    {
        internal BaseResponces RegisterProdAction(PRegisterData data)
        {
            PDbTable local;

            using (var db = new ProductContext())//Поиск на уже существующего товара
            {
                local = db.Products.FirstOrDefault(x => x.Article == data.Article);
            }

            if (local != null) { return new BaseResponces { Status = false, StatusMessage = "Article already registered" }; }

            var product = new PDbTable
            {
                Name = data.Name,
                Article = data.Article,
                Description = data.Description,
                Price = data.Price,
                Discount = 0,
                TotalRatings = 0,
                AvarageRating = 0,
                Brend = data.Brend,
                Category = data.Category,
                Tag = data.Tag,
                Directory = data.Directory,
                DateCreated = DateTime.Now,
                DateChanged = DateTime.Now,
                AvailableStatus = data.AvailableStatus,
                Image = ""
            };

            if (data.Images != null)
                product.Image = data.Images.First();

            //Занесение товара в таблицу
            using (var db = new ProductContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }

            if (data.Images != null)
            {
                using (var db = new ImgContext())
                {
                    foreach (var img in data.Images)
                    {
                        var strImg = new PImgTable
                        {
                            ProdArticle = data.Article,
                            Img = img,
                        };

                        db.Imgs.Add(strImg);
                    }
                    db.SaveChanges();
                }
            }
            return new BaseResponces { Status = true };
        }
        internal BaseResponces DeleteProdAction(string pCred)
        {
            PDbTable deleteProd = null;
            using (var db = new ProductContext())
            {
                deleteProd = db.Products.FirstOrDefault(x => x.Article == pCred);
            }
            if (deleteProd == null) { return new BaseResponces { Status = false, StatusMessage = "Product doesn't exist" }; }
            using (var db = new ProductContext())
            {
                List<PImgTable> local = new List<PImgTable>();
                using (var db1 = new ImgContext())
                {
                    local = db1.Imgs.Where(x => x.ProdArticle == pCred).ToList();
                    if (local != null)
                        foreach (var item in local)
                        {
                            db1.Imgs.Remove(item);
                        }
                    db1.SaveChanges();
                }

                Directory.Delete(deleteProd.Directory, true);

                db.Products.Remove(deleteProd);
                db.SaveChanges();

            }

            return new BaseResponces { Status = true };
        }
        internal BaseResponces DeleteReviewAction(string message)
        {
            RDbTable deleteReview = null;
            using (var db = new ReviewContext())
            {
                deleteReview = db.Reviews.FirstOrDefault(x => x.Message == message);
            }
            if (deleteReview == null) { return new BaseResponces { Status = false, StatusMessage = "Review doesn't exist" }; }

            PDbTable local = null;
            using (var db = new ProductContext())
            {
                local = db.Products.FirstOrDefault(x => x.Article == deleteReview.Article);
            }
            if (local == null) { return new BaseResponces { Status = false, StatusMessage = "Product doesn't exist" }; }

            using (var db = new ProductContext())
            {
                local.AvarageRating = local.AvarageRating - deleteReview.Rate / local.TotalRatings;
                local.TotalRatings--;
                db.Entry(local).State = EntityState.Modified;
                db.SaveChanges();
            }

            using (var db = new ReviewContext())
            {
                db.Reviews.Remove(deleteReview);
                db.SaveChanges();
            }
            return new BaseResponces { Status = true };
        }
        internal BaseResponces BanUserAction(string data)
        {
            UDbTable BanUser = null;
            using (var db = new UserContext())
            {
                BanUser = db.Users.FirstOrDefault(x => x.Email == data);
            }

            if (BanUser != null)
            {
                BanUser.Level = Domain.Enums.URole.Banned;
            }
            return new BaseResponces { Status = true };
        }
        //internal BaseResponces EditUserAction(... data)
        //internal BaseResponces EditProdAction(... data)
    }

}
