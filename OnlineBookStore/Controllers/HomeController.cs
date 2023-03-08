using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Models;
using OnlineBookStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookStore.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo;

        public HomeController(IBookstoreRepository bookstorerepository) => repo = bookstorerepository;
        public IActionResult Index(string category, int page_num = 1)
        {
            int results_per_page = 10;


            var x = new BooksViewModel
            {
                Books = repo.Books
                .Where(b => b.Category == category || category == null)
                .OrderBy(b => b.Title)
                .Skip((page_num - 1) * results_per_page)
                .Take(results_per_page),

                PageInfo = new PageInfo
                {
                    TotalBookCount = 
                        (category == null 
                        ? repo.Books.Count() 
                        : repo.Books.Where(b => b.Category == category).Count()),
                    ResultsPerPage = results_per_page,
                    CurrentPage = page_num
                }
            };
            return View(x);
        }
    }
}
