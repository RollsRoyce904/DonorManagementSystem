using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace testDMS.Models
{
    
        public class DonorMetadata
        {
            public int DonorId;
            public string FName;
            public string Init;
            public string LName;
            public string Title;
            public string Suffix;
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email;
            [RegularExpression(@"\d{10,11}$", ErrorMessage = "Invalid Phone Number. Please enter a valid 10 or 11 digit phone number without spaces, parentheses, or dashes.")]
            public string Cell;
            public Nullable<System.DateTime> Birthday;
            public string Gender { get; set; }
            public Nullable<int> MarkerId;
            public Nullable<int> ContactId;
            public string CompanyName;
            public string Address;
            public string City;
            [RegularExpression(@"\d{5}$", ErrorMessage = "Invalid Zip Code")]
            public string Zipcode;
            [RegularExpression(@"\d{10,11}$", ErrorMessage = "Invalid Phone Number. Please enter a valid 10 or 11 digit phone number without spaces, parentheses, or dashes.")]
            public string Phone;
            public string State;
        }
    
}