using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace testDMS.Models
{
   
        [MetadataType(typeof(DonorMetadata))]
        public partial class DONOR
        {
        }
    
    [MetadataType(typeof(DonationMetadata))]
    public partial class DONATION
    { }
}