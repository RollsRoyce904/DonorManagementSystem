using System.Collections.Generic;

namespace testDMS.Models
{
    public class ChartDispalyViewModel
    {
        public IEnumerable<DONOR> Donors { get; set; }

        public IEnumerable<DONATION> Donations { get; set; }

        public IEnumerable<JsonData> DataList { get; set; }

        public JsonData DataInstance { get; set; }

        public Option JsonOption { get; set; }

    }
}