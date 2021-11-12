using Data.DataDb;
using Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Stock.Controllers
{
    public class LoginController : Controller
    {
        StockTrackingDbContext c = new StockTrackingDbContext();
        [HttpGet]
        public IActionResult Index()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(User u)
        {
            var user = c.Users.FirstOrDefault(x => x.UserName==u.UserName && x.Password==u.Password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,u.UserName)
                };
                var identity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
                HttpContext.Session.SetInt32("id", user.UserId);
                HttpContext.Session.SetString("username", user.UserName+ "");
                return RedirectToAction("Index","Product");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            
            return RedirectToAction("Index");
        }
        public IActionResult SignUp(User user)
        {
            if (HttpContext.Session.GetInt32("id").HasValue)
            {
                return RedirectToAction("/Home/Index/");
            }
            return View();
        }
        public async Task<IActionResult> Register(User user)
        {
            await c.AddAsync(user);
            await c.SaveChangesAsync();
            return RedirectToAction("Index");
        }
      
    }
}
