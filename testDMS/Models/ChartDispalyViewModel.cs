using System.Collections.Generic;

namespace testDMS.Models
{
    public class ChartDispalyViewModel
    {
        public IEnumerable<DONOR> Donors { get; set; }

        public IEnumerable<DONATION> Donations { get; set; }
    }
}