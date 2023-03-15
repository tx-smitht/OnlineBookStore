using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookStore.Models
{
    public class Purchase
    {
        [Key]
        [BindNever] // We use bindnever to not bind it to the form. It is stuff that's not passed
                    // with the form. We want this to be secure. 
        public int PurchaseId { get; set; }

        [BindNever]
        public ICollection<CartLineItem> Lines { get; set; }

        [Required(ErrorMessage = "Please Enter a First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter a Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter the first address line")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }

        [Required(ErrorMessage = "Please Enter a City Name")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please Enter a State")]
        public string State { get; set; }

        public string Zip { get; set; }

        [Required(ErrorMessage = "Please Enter a Country")]
        public string Country { get; set; }
    }
}
