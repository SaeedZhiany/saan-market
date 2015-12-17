using saan_market.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace saan_market.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DatabaseEntities context = new DatabaseEntities();

            var productList = from s in context.Products
                              select s;
            return View(productList.ToList());
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}