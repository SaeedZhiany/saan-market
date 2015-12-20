using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace saan_market.Models
{
    public class CartProductsModel
    {
        [HiddenInput]
        public int id { get; set; }

        public string name { get; set; }

        public string picturePath { get; set; }

        public int count { get; set; }

        public int baseValue { get; set; }
    }
}