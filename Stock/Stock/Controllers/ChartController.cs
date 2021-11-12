using Data.Data;
using Data.DataDb;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Controllers
{
    
    public class ChartController : Controller
    {
        StockTrackingDbContext c = new StockTrackingDbContext();
        public IActionResult Index()
        {
          
            var count = c.Products.Sum(x => x.ProductStock);
            ViewBag.T2 = count;
            return View();
        }       
        public IActionResult Index2()
        {

            var count = c.Products.Sum(x => x.ProductStock);
            ViewBag.T2 = count;
            return View();

        }
        public IActionResult VisualizeProductResult()
        {
            return Json(ProductList());
        }
        public List<Chart> ProductList()
        {
            List<Chart> ct = new List<Chart>();
            using (var c = new StockTrackingDbContext())
            {
                ct = c.Products.Select(x => new Chart
                {
                    productname = x.ProductName,
                    stock = x.ProductStock
                }).ToList();
            }
            return ct;
        }
    }
}
