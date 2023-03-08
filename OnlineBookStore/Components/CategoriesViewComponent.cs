using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookStore.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        private IBookstoreRepository repo { get; set; }

        public CategoriesViewComponent(IBookstoreRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            // Setting the ViewBag variable of "SelectedCategory" to the current selected category
            // (if there is one)
            ViewBag.SelectedCategory = RouteData?.Values["category"];

            // The list that we are builing here will come into
            // the "Default" component view as an IEnumerable.
            // We use var here, which allows ASP to detect that down the 
            // road it will be an IEnnumerable.
            var categories = repo.Books
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return View(categories);
        }
    }
}
