using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acme_Database_Utility;

namespace Acme_Ecommerce.Models
{
    public class ProductModel: ProductSchema
    {
        private string path;

        public void setPath()
        {

        }

        public string getPath()
        {
            if (this.type.ToLower().Equals("tech"))
            {
                this.path = "~/Assets/Product Images/phone.jpg";
            }
            else if ((this.type.ToLower().Equals("book")))
            {
                this.path = "~/Assets/Product Images/phone.jpg";
            }
            else if ((this.type.ToLower().Equals("cosmetic")))
            {
                this.path = "~/Assets/Product Images/phone.jpg";
            }
            else if ((this.type.ToLower().Equals("cosmetic")))
            {
                this.path = "~/Assets/Product Images/phone.jpg";
            }
            return this.path;
        }

    }
}