using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acme_Database_Utility;

namespace Acme_Ecommerce.Models
{
    public class UserModel: UserSchema
    {
        public bool isLoggedIn { get; set; }

        private List<ProductModel> UserCart = new List<ProductModel>();

        public void SetCart(ProductModel product)
        {
            this.UserCart.Add(product);
        }

        public List<ProductModel> GetCart()
        {
            return this.UserCart;
        }
    }
}