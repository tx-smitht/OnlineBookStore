using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineBookStore.Models;
using OnlineBookStore.Infrastructure;

namespace OnlineBookStore.Pages
{
    public class ShoppingCartModel : PageModel
    {
        // Bring in the data side of things with the repo.
        // Need this to get the book ID
        private IBookstoreRepository repo { get; set; }
        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }

        // Creating instance of the repo when the object is created.
        public ShoppingCartModel(IBookstoreRepository temp, Cart c)
        {
            repo = temp;
            cart = c;

        }

        // Lines that have been commented out have to do with session.
        // We used to be handling that here, but we have since moved it.

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            //cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(int bookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == bookId);

            // This says if the cart exists, then set cart = to that.
            // If not, create a new thing.
            //cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            cart.AddItem(b, 1);

            // Setting the "cart" variable within session
            //HttpContext.Session.SetJson("cart", cart);

            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

        // This handles the removal of books from cart
        public IActionResult OnPostRemove(int bookId, string returnUrl)
        {
            cart.RemoveItem(cart.BookList.First(x => x.Book.BookId == bookId).Book);
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }

    }
}
