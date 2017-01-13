using System.Collections.Generic;

namespace testDMS.Models
{
    public class DisplayDataViewModel
    {
        public IEnumerable<DONOR> Donors { get; set; }
        public IEnumerable<DONATION> Donations { get; set; }
    }
}