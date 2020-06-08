using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;


namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        // GET: /<controller>/
        private BookService _bookService;
       
        public BookController(BookService bookService)
        {
            _bookService = bookService;
            
        }
        public IActionResult Detail(int id)
        {
            Book book = _bookService.GetById(id);
            return View(book);
        }
        public IActionResult RemoveProduct(int id)
        {
            CardHelper.RemoveProduct(HttpContext, id);
            string referer = Request.Headers["Referer"].ToString();
            return Redirect(referer);
        }
    }
}
