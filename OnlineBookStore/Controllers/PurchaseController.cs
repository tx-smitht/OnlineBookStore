using Microsoft.AspNetCore.Mvc;
using OnlineBookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookStore.Controllers
{
    public class PurchaseController : Controller
    {
        private IPurchaseRepository repo { get; set; }
        private Cart cart { get; set; }
        public PurchaseController(IPurchaseRepository temp, Cart c)
        {
            repo = temp;
            cart = c;
        }


        // We also need to know what is in their cart. That is why we pass in the session cart.

        [HttpGet]
        public IActionResult Checkout()
        {
            return View(new Purchase());
        }


        [HttpPost]
        public IActionResult Checkout(Purchase purchase) // Pass in a purchase so that it can be added to the DB
        {
            if (cart.BookList.Count == 0)
            {
                ModelState.AddModelError("", "Your Cart is Empty. Please Add Items.");
            }

            if (ModelState.IsValid)
            {
                purchase.Lines = cart.BookList.ToArray();
                repo.SavePurchase(purchase);
                cart.ClearCart();

                return RedirectToPage("/PurchaseComplete");
            }
            else
            {
                return View();
            }
        }
    }
}
