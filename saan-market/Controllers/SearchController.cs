using saan_market.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace saan_market.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
/*        public ActionResult ResultProduct()
        {
            return View();
        }*/
        public ActionResult ResultProduct(string searchString)
        {

                using (DatabaseEntities context = new DatabaseEntities())
                {
                    var productList = from s in context.Products
                                   select s;

                    if (!String.IsNullOrEmpty(searchString))
                    {
                        productList = productList.Where(s => s.name.Contains(searchString));
                    }

                return View(productList.ToList());
            }
        }
    }
}