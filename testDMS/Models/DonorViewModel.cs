using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testDMS.Models
{
    public class DonorViewModel
    {
        public PagedList.IPagedList<DONOR> Donors { get; set; }
        public string searchString { get; set; }
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int ItemNumber { get; set; }
    }
}