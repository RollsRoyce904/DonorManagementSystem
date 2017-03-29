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

    public class JsonData
    {
        public string FName { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.DateTime> DateRecieved { get; set; }
        public string Department { get; set; }
        public string GL { get; set; }
    }

    public class JsonDataDisplay
    {
        public IEnumerable<JsonData> DataList { get; set; }
    }

    public class Searches
    {
        public string typeOne { get; set; }
        public string typeTwo { get; set; }
        public string typeThree { get; set; }
        public string typeFour { get; set; }
        public string typeFive { get; set; }
    }
}