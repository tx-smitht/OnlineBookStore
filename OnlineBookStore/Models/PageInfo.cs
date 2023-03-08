using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookStore.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalBookCount { get; set; }
        public int ResultsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalBookCount / ResultsPerPage);
    }
}
