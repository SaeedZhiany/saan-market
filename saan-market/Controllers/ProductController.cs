using saan_market.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace saan_market.Controllers
{
    public class ProductController : Controller
    {

        public ActionResult DefineProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DefineProduct(Models.ProductModel product, IEnumerable<HttpPostedFileBase> images)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (images.Count() == 0)
                    {
                        ModelState.AddModelError("", "عکسی برای محصول انتخاب نمایید.");
                        return RedirectToAction("DefineProduct", "Product");
                    }
                    product.paths = new List<string>();
                    foreach (HttpPostedFileBase image in images)
                    {
                        if (image != null && image.ContentLength > 0)
                        {
                            string path = Path.Combine(Server.MapPath(@"~/Content/images/productImages/"), image.FileName);
                            image.SaveAs(path);
                            product.paths.Add(path);
                        }

                    }

                    if (product.addProduct())
                    {
                        return RedirectToAction("DefineProduct", "Product");
                    }
                }
                catch (Exception e)
                {
                    if (e.Message.Equals(""))
                        ModelState.AddModelError("", "خطا در ذخیره سازی محصول در پایگاه داده، لطفا دوباره امتحان کنید.");
                    else
                        ModelState.AddModelError("", e.Message.ToString());
                    return View(product);
                }
            }
            ModelState.AddModelError("", "لطفا فیلدهای لازم را به درستی وارد کنید.");
            return View(product);

        }

        public ActionResult DisplayProduct(int productId)
        {
            DatabaseEntities context = new DatabaseEntities();
            
                List<Product> products = new List<Product>();
                Product selectedProduct = (from pro in context.Products
                                       where pro.id == productId
                                       select pro).First();
                products.Add(selectedProduct);

                var relativeProducts = (from pro in context.Products
                                        where (pro.kind == selectedProduct.kind) && (pro.id != selectedProduct.id)
                                        select pro);
                foreach (Product p in relativeProducts)
                    products.Add(p);
                //context.Products.Where(s => s.id == productId).First();

                return View(products);
                
            //    return View(context.Products.ToList());

            
        }
        public ActionResult ShopView()
        {
            DatabaseEntities context = new DatabaseEntities();

            var productList = from s in context.Products
                              select s;
            return View(productList.ToList());

        }
    }
}