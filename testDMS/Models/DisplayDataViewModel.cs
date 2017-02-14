using System.Collections.Generic;

namespace testDMS.Models
{
    public class DisplayDataViewModel
    {
        public DONOR Donors { get; set; }
        
        public IEnumerable<DONATION> Donations { get; set; }

        public IEnumerable<NOTE> Notes { get; set; }
    }
}