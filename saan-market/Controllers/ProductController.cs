using System;
using System.Collections.Generic;
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
        public ActionResult DefineProduct(Models.ProductModel product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (product.addProduct())
                    {
                        return RedirectToAction("DefineProduct", "Product");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "خطا در ذخیره سازی محصول در پایگاه داده، لطفا دوباره امتحان کنید.");
                }
            }
            ModelState.AddModelError("", "لطفا فیلدهای لازم را به درستی وارد کنید.");
            return View(product);

        }
    }
}