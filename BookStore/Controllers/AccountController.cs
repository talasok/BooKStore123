using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookStore.Data;
using Microsoft.AspNetCore.Http;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private AcountService _acountService;

        public AccountController(AcountService acountService)
        {
            _acountService = acountService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (!(string.IsNullOrEmpty(HttpContext.Session.GetString("User"))))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(Account U)
        {
            var user = _acountService.GetAccount(U);
            if (user != null)
            {
                HttpContext.Session.SetString("User", user.UserName);
                HttpContext.Session.SetString("Role", user.FullName);   

                return RedirectToAction("Index","Home");
            }
            ViewBag.Message = "Mật khẩu hoặc tài khoản không đúng";
            return View();
        }
        public IActionResult Logout()
        {
            //HttpContext.Session.Clear();
            HttpContext.Session.Remove("User");
            HttpContext.Session.Remove("Role");
            
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
            if (!(string.IsNullOrEmpty(HttpContext.Session.GetString("User"))))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Register(Account U,string RepeatPassword)
        {
            Console.WriteLine("check1: " + RepeatPassword);
            Console.WriteLine("check2: " + U.FullName+ ";"+U.Password +":"+U.UserName);
            if (U != null)
            {
                if (U.FullName != null && U.UserName != null && U.Password != null && RepeatPassword != null)
                {
                    if (U.Password.Trim() != RepeatPassword.Trim())
                    {
                        ViewBag.ErrorMessage = "Mật Khẩu không trùng khớp";
                        return View();
                    }

                    if (_acountService.CheckAccount(U.UserName))
                    {
                        ViewBag.ErrorMessage = "Tài Khoản đã được đăng ký từ trước";
                        return View();
                    }

                    _acountService.InsertAccount(U);
                    ViewBag.ErrorMessage = "Đăng ký thành công";
                    return View();

                }
            }
            ViewBag.ErrorMessage = "Làm ơn nhập thông tin";
            return View();
        }
    }
}