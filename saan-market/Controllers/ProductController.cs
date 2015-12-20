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

        public PartialViewResult AddToCart(int productId, int count)
        {
            ViewData["CartContent"] = TempData["CartContent"];
            List<CartProductsModel> list = (List<CartProductsModel>) ViewData["CartContent"];
            if (list == null)
                list = new List<CartProductsModel>();
            foreach(CartProductsModel m in list)
            {
                if(m.id == productId)
                {
                    m.count += count;
                    ViewData["CartContent"] = list;
                    TempData["CartContent"] = ViewData["CartContent"];
                    return PartialView("_cart");
                }
            }
            using(DatabaseEntities context = new DatabaseEntities())
            {
                Product product = context.Products.Where(p => p.id == productId).First();
                CartProductsModel addedProduct = new CartProductsModel();
                addedProduct.id = product.id;
                addedProduct.name = product.name;
                addedProduct.baseValue = (int) product.price;
                addedProduct.count = count;
                addedProduct.picturePath = product.Pictures.First().path;
                list.Add(addedProduct);
            }
            ViewData["CartContent"] = list;
            TempData["CartContent"] = ViewData["CartContent"];
            return PartialView("_Cart");
        }

        public PartialViewResult RemoveFromCart(int productId)
        {
            ViewData["CartContent"] = TempData["CartContent"];
            List<CartProductsModel> list = (List<CartProductsModel>)ViewData["CartContent"];
            foreach (CartProductsModel m in list)
            {
                if (m.id == productId)
                {
                    list.Remove(m);
                    ViewData["CartContent"] = list;
                    TempData["CartContent"] = ViewData["CartContent"];
                    break;
                }
            }
            return PartialView("_cart");
        }
    }
}