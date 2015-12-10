using System;
using System.ComponentModel.DataAnnotations;

namespace saan_market.Models
{
    public class ProductModel
    {
        [Required]
        public String name { get; set; }

        [Required]
        public long price { get; set; }

        [Required]
        public String descriptionSummury { get; set; }

        [Required]
        public int available { get; set; }

        [Required]
        public String color { get; set; }

        [Required]
        public String technicalDescription { get; set; }

        [Required]
        public int kind { get; set; } // 1 = mobile, 2 = tablet

        public const int MOBILE = 1;
        public const int TABLET = 2;

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
                    context.Products.Add(product);
                    context.SaveChanges();
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