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

        public bool addProduct()
        {
            try
            {
                using(SaanMarketDBEntities context = new SaanMarketDBEntities())
                {
                    Product product = new Product();
                    product.name = name;
                    product.price = price;
                    product.descriptionSummury = descriptionSummury;
                    product.available = available;
                    if(!color.Equals("") || !technicalDescription.Equals(""))
                    {
                        MobileProduct mProduct = new MobileProduct();
                        mProduct.Product = product;
                        mProduct.color = color;
                        
                        mProduct.technicalDescription = technicalDescription;
                        context.MobileProducts.Add(mProduct);
                        context.SaveChanges();
                        return true;
                    }
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