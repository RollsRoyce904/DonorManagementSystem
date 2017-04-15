using System;
using System.Collections.Generic;

namespace testDMS.Models
{
    public class ChartDispalyViewModel
    {
        public PagedList.IPagedList<DONOR> Donors { get; set; }

        public PagedList.IPagedList<DONATION> Donations { get; set; }

        public string searchString { get; set; }

        public int? amount { get; set; }

        public DateTime? date1 { get; set; }

        public DateTime? date2 { get; set; }

        public string department { get; set; }

        public string gl { get; set; }

    }
}