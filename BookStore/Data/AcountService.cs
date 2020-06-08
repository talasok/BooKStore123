using BookStore.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Data
{
    public class AcountService
    {
        private AppDataContext appDataContext;

        public AcountService(AppDataContext appDataContext)
        {
            this.appDataContext = appDataContext;
        }
        public Account GetAccount(Account U)
        {
            Account user = appDataContext.Account.Where(x => x.UserName == U.UserName && x.Password == U.Password).FirstOrDefault();
           
            return user;
        }
        public void InsertAccount(Account U)
        {
           
            appDataContext.Account.Add(U);
            appDataContext.SaveChanges();
        }
        public bool CheckAccount(string user)
        {
            var query = appDataContext.Account.Find(user);
            if (query != null) return true;
            return false;
        }
    }
}
