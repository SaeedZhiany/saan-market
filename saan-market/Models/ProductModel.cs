using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace saan_market.Models
{
    public class ProductModel
    {
        [Required(ErrorMessage = "نام محصول نیاز است")]
        public string name { get; set; }

        [Required(ErrorMessage = "قیمت محصول نیاز است")]
        public long price { get; set; }

        [Required(ErrorMessage = "خلاصه شرح محصول نیاز است")]
        public string descriptionSummury { get; set; }

        [Required]
        public int available { get; set; }

        [Required(ErrorMessage = "رنگ محصول را انتخاب کنید")]
        public string color { get; set; }

        public string technicalDescription { get; set; }

        [Required]
        [Display(GroupName = "kind")]
        public int kind { get; set; } // 1 = mobile, 2 = tablet

        public const int MOBILE = 1;
        public const int TABLET = 2;
        
        
        public List<string> paths { get; set; }

        public bool addProduct()
        {
            try
            { 
                using(DatabaseEntities context = new DatabaseEntities())
                {
                    Product product = new Product();
                    product.name = name;
                    product.price = price;
                    product.descriptionSummury = descriptionSummury;
                    product.available = available;
                    product.color = color;
                    product.technicalDescription = technicalDescription;
                    product.kind = kind;
                    foreach (string path in paths)
                    {
                        Picture pic = new Picture();
                        pic.path = path;
                        pic.Product = product;
                        product.Pictures.Add(pic);
                    }
                    context.Products.Add(product);
                    int curId = context.SaveChanges();

                    return true;
                }

            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}