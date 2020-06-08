using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Components
{
    public class CartComponent : ViewComponent
    {
        //private  CardHelper _cardhelper;
        private BookService _bookService;
        public CartComponent( BookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //CardHelper.Update(HttpContext,)
            Dictionary<int, int> card = CardHelper.GetAllProducts(HttpContext);
            Dictionary<Book, int> books = _bookService.FindAll(card);
            return View(books);
        }
    }
}
