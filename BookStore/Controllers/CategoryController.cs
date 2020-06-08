using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Components;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BookService _bookService;
        private readonly MenuService _menuService;
        public CategoryController(BookService bookService, MenuService menuService)
        {
            _bookService = bookService;
            _menuService = menuService;
        }

        // GET: /<controller>/
        public IActionResult Detail(int id,int page =1,int pagesize = 4)
        {

            //List<Book> books = _bookService.GetAllBookOfCategory(id);
            int totalRecord = 0;
            List<Book> books = _bookService.GetAllBookOfCategory(id,ref totalRecord,page,pagesize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)totalRecord / pagesize);
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            ViewBag.ID = id;
            //List<Book> books = _bookService.GetPageBookOfCategory(page,pagesize);
            ViewData["category"] = _bookService.GetAllCategoryCount();
            //ViewData["category"] = _menuService.GetAllMenus();
            return View(books);
        }
        public IActionResult RemoveProduct(int id)
        {
            CardHelper.RemoveProduct(HttpContext, id);
            string referer = Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }
    }
}
