using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        private BookService _bookService;

        public CartController(BookService bookService)
        {
            _bookService = bookService;
        }
        public IActionResult Index()
        {
            Dictionary<int, int> bookIds = CardHelper.GetAllProducts(HttpContext);
            Dictionary<Book, int> books = _bookService.FindAll(bookIds);
            string referer = Request.Headers["Referer"].ToString();
            ViewData["returnurl"] = referer;
            return View(books);
        }
        public IActionResult AddProduct(int id)
        {
            Book book = _bookService.GetById(id);
            if (book == null) return NotFound();
            CardHelper.AddProduct(HttpContext, book);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveProduct(int id)
        {
            CardHelper.RemoveProduct(HttpContext, id);
            return RedirectToAction("Index");
        }

        public IActionResult Update([FromForm] Dictionary<int, int> books)
        {
            CardHelper.Update(HttpContext, books);
            return RedirectToAction("Index");
        }
    }
}