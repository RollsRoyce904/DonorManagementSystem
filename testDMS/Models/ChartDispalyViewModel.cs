using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace testDMS.Models
{
    public class ChartDispalyViewModel
    {
        public IEnumerable<DONOR> Donors { get; set; }

        public IEnumerable<DONATION> Donations { get; set; }

    }
}