using Data.DataDb;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Stock.Controllers
{
    public class ProductController : Controller
    { StockTrackingDbContext c = new StockTrackingDbContext();
        ProductRepository productRepository = new ProductRepository();
        [Authorize]
        public IActionResult Index(string s)
        {
          
                       
            if (!String.IsNullOrEmpty(s))
            {    
                return View(productRepository.List(x => x.ProductName.Contains(s)));
            }
            
            return View(productRepository.TList());
        }
      
        [HttpGet]
        public IActionResult ProductAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ProductAdd(Product product)
        {

            productRepository.TAdd(product);
            return RedirectToAction("Index");
        }
        public IActionResult ProductGet(int id)
        {
            var x = productRepository.TGet(id);
            Product product = new Product()
            {
                ProductName = x.ProductName,
                ProductStock = x.ProductStock,
                ProductPrice = x.ProductPrice,
                ProductStatus = x.ProductStatus,
                Id = x.Id


            };
            return View(product);
        }
        [HttpPost]
        public IActionResult ProductUpdate(Product product)
        {
            var x = productRepository.TGet(product.Id);
            x.ProductName = product.ProductName;
            x.ProductPrice = product.ProductPrice;
            x.ProductStock = product.ProductStock;
            x.ProductStatus = true;
            productRepository.TUpdate(x);
            return RedirectToAction("Index");
        }
        public IActionResult ProductDelete(int id)
        {
            productRepository.TDelete(new Product { Id = id });
            return RedirectToAction("Index");
        }
        [HttpPost]
       public IActionResult Details(Product product)
        {
            var x = productRepository.TGet(product.Id);

            x.Details = product.Details;
                x.Id = product.Id;   
            
            return View(product.Details);
        }
        }
    }

    