using saan_market.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace saan_market.Controllers
{
    public class SearchController : Controller
    {
//        public string displayString { get; set; }
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
            DatabaseEntities context = new DatabaseEntities();
            
                var productList = from s in context.Products
                                select s;

                if (!String.IsNullOrEmpty(searchString))
                {
                    productList = productList.Where(s => s.name.Contains(searchString));
                    ViewData["searchString"] = searchString;
                }
                else
                {
                    ViewData["searchString"] = "Empty";
                }

                return View(productList.ToList());
            
        }
    }
}