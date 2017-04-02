//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace testDMS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class DONOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DONOR()
        {
            this.DONATION = new HashSet<DONATION>();
            this.NOTES = new HashSet<NOTES>();
            this.RELATIVES = new HashSet<RELATIVES>();
        }
    
        public int DonorId { get; set; }
        public string FName { get; set; }
        public string Init { get; set; }
        public string LName { get; set; }
        public string Title { get; set; }
        public string Suffix { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string Cell { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Gender { get; set; }
        public Nullable<int> MarkerId { get; set; }
        public Nullable<int> ContactId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter a valid zipcode.")]
        public string Zipcode { get; set; }
        [RegularExpression("^[0-9]{10,11}$", ErrorMessage = "Please enter a valid 10 digit phone number. Do not include dashes.")]
        public string Phone { get; set; }
        public string State { get; set; }
    
        public virtual CONTACT CONTACT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONATION> DONATION { get; set; }
        public virtual IDENTITYMARKER IDENTITYMARKER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOTES> NOTES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RELATIVES> RELATIVES { get; set; }
    }
}
