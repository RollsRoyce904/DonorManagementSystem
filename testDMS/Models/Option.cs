using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testDMS.Models
{
    public class Option
    {
        public int OptionId { get; set; }
        public string Value { get; set; }
    }

    public class JsonDonationList
    {
        public IEnumerable<DONATION> list { get; set; }
    }
}