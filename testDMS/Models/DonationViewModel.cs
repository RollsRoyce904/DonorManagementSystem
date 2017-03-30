using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testDMS.Models
{
    public class DonationViewModel
    {
        public PagedList.IPagedList<DONATION> Donations { get; set; }
        public string searchString { get; set; }
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
    }
}