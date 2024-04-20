using eUseControl.Domain.Entities.Auth;
using eUseControl.Domain.Entities.Product.DBModel;
using eUseControl.Domain.Entities.Product;
using eUseControl.Domain.Entities.User.DBModel;
using eUseControl.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eUseControl.Domain.Entities.GeneralResponce;

namespace eUseControl.BuisnessLogic.Core
{
    public class UserApi
    {

        /// <summary>
        /// AUTH
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal GeneralResponce ULASessionCheck(ULoginData data)
        {

            //db connection
            //SELECT USER FROM DB>User WHERE Username=data.Credential and Password = data.Password

            //if SELECT VALID OR TRUE

            //RETURN STATUS = true

            return new GeneralResponce
            {
                Status = false,
                CurrentUser = new User
                {
                    UserName = "Vasilica"
                }
            };
        }
        internal UCoockieData UserCoockieGenerationAlg(User user)
        {

            //HERE WILL BE THE LOGIC TO COOCKIE GENERATION PROCESS

            return new UCoockieData
            {
                MaxAge = 1709044385,
                Coockie = "MY UNIQUE ID FOR THIS SESSION"
            };
        }
        //-----------------------------PRODUCT-------------------------------------
        internal ProductDataModel ProductActionGetToList()
        {


            //SELECT FRON DB db.Product -> Products;

            var products = new List<Product>();


            return new ProductDataModel { Products = products };
        }
        internal ProductDataModel ProductGetSingleAction(int id)
        {

            //SELECT FRON db.Product WHERE ID = id

            var product = new Product();

            return new ProductDataModel { SingleProduct = product };

        }

    }
}
