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
        public IActionResult Index(int page_num = 1)
        {
            int results_per_page = 5;


            var x = new BooksViewModel
            {
                Books = repo.Books
                .OrderBy(b => b.Title)
                .Skip((page_num - 1) * results_per_page)
                .Take(10),

                PageInfo = new PageInfo
                {
                    TotalBookCount = repo.Books.Count(),
                    ResultsPerPage = results_per_page,
                    CurrentPage = page_num
                }
            };
            return View(x);
        }
    }
}
