using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public static class CardHelper
    {
        //public static Dictionary<int, int> books = new Dictionary<int, int>();
        public static void AddProduct(HttpContext context, Book book)
        {
            string bookstr = context.Session.GetString("cart");
            Dictionary<int, int> books = new Dictionary<int, int>();
            if (!String.IsNullOrEmpty(bookstr))
                books = JsonConvert.DeserializeObject<Dictionary<int, int>>(bookstr);

            if (books.ContainsKey(book.Id))
            {
                books[book.Id]++;
            }
            else
            {
                books[book.Id] = 1;
            }

            bookstr = JsonConvert.SerializeObject(books);
            context.Session.SetString("cart", bookstr);
        }

        public static Dictionary<int, int> GetAllProducts(HttpContext context)
        {
            string bookstr = context.Session.GetString("cart");
            Dictionary<int, int> books = new Dictionary<int, int>();
            if (!String.IsNullOrEmpty(bookstr))
            {
                books = JsonConvert.DeserializeObject<Dictionary<int, int>>(bookstr);
                return books;
            }
            else
            {
                return new Dictionary<int, int>();
            }
        }

        public static void RemoveProduct(HttpContext context, int id)
        {
            string bookstr = context.Session.GetString("cart");
            Dictionary<int, int> books = new Dictionary<int, int>();
            if (!String.IsNullOrEmpty(bookstr))
                books = JsonConvert.DeserializeObject<Dictionary<int, int>>(bookstr);
            if (books.ContainsKey(id))
                books.Remove(id);
            bookstr = JsonConvert.SerializeObject(books);
            context.Session.SetString("cart", bookstr);
        }

        public static void Update(HttpContext context, Dictionary<int, int> books)
        {
            string bookstr = JsonConvert.SerializeObject(books);
            context.Session.SetString("cart", bookstr);
        }
    }
}
