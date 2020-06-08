using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Data
{
    
    public class BookService
    {
        private AppDataContext appDataContext;

        public BookService(AppDataContext appDataContext)
        {
            this.appDataContext = appDataContext;
        }
        /// <summary>
        /// lấy ra all danh sách Book
        /// </summary>
        /// <returns></returns>
        public ICollection<Book> ListAllBooks()
        {
            return appDataContext.Book.ToList();
        }

        public List<Category> GetAllCategories()
        {
            return appDataContext.Category.ToList();
        }

        public List<Book> GetAllBookOfCategory(int categoryId)
        {
            var query = from b in appDataContext.Book
                        join cb in appDataContext.Categorybook on b.Id equals cb.BookId
                        join c in appDataContext.Category on cb.CategoryId equals c.Id
                        where cb.CategoryId == categoryId
                        select b;
            //var query = from b in appDataContext.Book select b;
            return query.ToList();
        }
        //public IEnumerable<Book> GetPageBookOfCategory(int page,int pageSize)
        //{
        //    //var query = appDataContext.Book.ToPagedList(page,pageSize);
        //    var query = (from b in appDataContext.Book select b).OrderBy(x => x.Id).ToPagedList(page, pageSize);
        //    return query;
        //}
        /// <summary>
        /// Lấy ra thông tin của 1 book 
        /// </summary>
        /// <param name="id">mã book</param>
        /// <returns>Book</returns>
        public Book GetById(int id)
        {
            return appDataContext.Book.Find(id);
        }
        /// <summary>
        /// Lấy ra danh sách thông tin của book và số lượng đặt
        /// </summary>
        /// <param name="bookIds">Dictionary chưa key là mã book value là số lượng đặt</param>
        /// <returns>Dictionary<Book, int></returns>
        public Dictionary<Book, int> FindAll(Dictionary<int, int> bookIds)
        {
            var query = from kv in bookIds
                        select new KeyValuePair<Book, int>(appDataContext.Book.Find(kv.Key), kv.Value);
            return query.ToDictionary(v => v.Key, v => v.Value);
        }
        /// <summary>
        /// Lấy danh dánh category và số lượng sách của nó
        /// </summary>
        /// <returns>Dictionary<Category, int></returns>
        public Dictionary<Category, int> GetAllCategoryCount()
        {
            //var categorys = from c in appDataContext.Category select c;
            //Dictionary<Category, int> ds = new Dictionary<Category, int>();
            var query = from c in appDataContext.Category
                    select new KeyValuePair<Category,int>(c, (from cb in appDataContext.Categorybook where cb.CategoryId == c.Id 
                                                             select cb).Count());

            return query.ToDictionary(v => v.Key, v => v.Value);
        }
        /// <summary>
        /// Phân trang
        /// </summary>
        /// <param name="categoryId">Mã Category</param>
        /// <param name="tolalRecord">tổng số record của all list book</param>
        /// <param name="pageIndex">trang gửi lên</param>
        /// <param name="pageSize">số lượng book trong 1 trang</param>
        /// <returns>danh sách book</returns>
        public List<Book> GetAllBookOfCategory(int categoryId,ref int tolalRecord,int pageIndex =1,int pageSize=6)
        {
            tolalRecord = (from b in appDataContext.Book
                           join cb in appDataContext.Categorybook on b.Id equals cb.BookId
                           join c in appDataContext.Category on cb.CategoryId equals c.Id
                           where cb.CategoryId == categoryId
                           select b).Count();
            var query = (from b in appDataContext.Book
                        join cb in appDataContext.Categorybook on b.Id equals cb.BookId
                        join c in appDataContext.Category on cb.CategoryId equals c.Id
                        where cb.CategoryId == categoryId
                        select b).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            //var query = from b in appDataContext.Book select b;
            return query.ToList();
        }
    }
}
