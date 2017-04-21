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
            [DataType(DataType.EmailAddress)]
            public string Email;
            [RegularExpression(@"\d{10,11}$", ErrorMessage = "Invalid Phone Number. Please enter a valid 10 or 11 digit phone number without spaces, parentheses, or dashes.")]
            public string Cell;
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> Birthday;
            public string Gender { get; set; }
            public Nullable<int> MarkerId;
            public Nullable<int> ContactId;
            public string CompanyName;
            public string Address;
            public string City;
            [RegularExpression(@"\d{5}$", ErrorMessage = "Invalid Zip Code")]
            public string Zipcode;
            [DataType(DataType.PhoneNumber)]
            [RegularExpression(@"\d{10,11}$", ErrorMessage = "Invalid Phone Number. Please enter a valid 10 or 11 digit phone number without spaces, parentheses, or dashes.")]
            public string Phone;
            public string State;
        }

    public class DonationMetadata
    {
        public int DonationId { get; set; }
        public int DonorId { get; set; }
        [Required]
        //[RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Price can't have more than 2 decimal places")]
        public Nullable<decimal> Amount { get; set; }
        public string TypeOf { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateRecieved { get; set; }
        public string GiftMethod { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateGiftMade { get; set; }

        public string GiftRestrictions { get; set; }
        public string Notes { get; set; }
        public string Fund { get; set; }
        public string GL { get; set; }
        public string Department { get; set; }
        public string Program { get; set; }
        public string GrantS { get; set; }
        public string Appeal { get; set; }
    }
   
}