using Data.DataDb;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stock.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Controllers
{
    public class UserController : Controller
    {
        UserRepository userRepository = new UserRepository();
        [Authorize]
        public IActionResult Index(string s)
        {
            if (!String.IsNullOrEmpty(s))
            {
                return View(userRepository.List(x => x.UserName.Contains(s)));
            }
            return View(userRepository.TList());
        }
        [HttpGet]
        public IActionResult UserAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserAdd(User user)
        {
            userRepository.TAdd(user);
            return RedirectToAction("Index"); 

        }
        public IActionResult UserGet(int id)
        {
            var x = userRepository.TGet(id);
            User user = new User()
            {
                UserName = x.UserName,
                Password = x.Password,
                UserId = x.UserId

            };
            return View(user);
        }
        [HttpPost]
        public IActionResult UserUpdate(User user)
        {
            var x = userRepository.TGet(user.UserId);
            x.UserName = user.UserName;
            x.Password = user.Password;
            userRepository.TUpdate(x);
            return RedirectToAction("Index");       
        }

        public IActionResult UserDelete (int id)
        {
            userRepository.TDelete(new User { UserId = id });
            return RedirectToAction("Index");
        }
        
        }
    }

