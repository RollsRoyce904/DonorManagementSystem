using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testDMS.Models
{
    public class Report
    {
        public String Criteria { get; set; }
        public String Type { get; set; }
        public String Params { get; set; }
        public Char Equivalance { get; set; }
    }
}