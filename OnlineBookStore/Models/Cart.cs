using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookStore.Models
{
    public class Cart
    {
        // This is declaring the variable and instantiating it all in the same line
        // First part declares, second part instatiates. 
        public List<CartLineItem> BookList { get; set; } = new List<CartLineItem>();

        public void AddItem(Book book, int qty)
        {
            // Go search the current cart list and find the book associated with that book's ID
            CartLineItem line = BookList
                .Where(b => b.Book.BookId == book.BookId)
                .FirstOrDefault();

            // If there isn't any line item associated with the book, create one
            if (line == null)
            {
                // Using the default .Add operator to add to the list of type CartLineItem
                // The Book for the line item is the book passed in,
                // the quantity is the qty passed in
                BookList.Add(new CartLineItem
                {
                    Book = book,
                    Quantity = qty
                });
            }
            // Else, (if there already is a book found in cart list)  we are going to
            // increment it up by the quantity
            else
            {
                line.Quantity += qty;
            }
        }
        public double CalculateTotal()
        {
            double sum = 0;
            foreach (var b in BookList)
            {
                sum += (b.Book.Price * b.Quantity);
            }
            //double sum = BookList.Sum(x => x.Quantity * 25);

            return Math.Round(sum,2);

        }
    }


    public class CartLineItem
    {
        public int LineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }

    }
}
