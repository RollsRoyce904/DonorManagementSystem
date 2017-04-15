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
        [Required]
        public string Email { get; set; }
        [RegularExpression(@"\d{10,11}$", ErrorMessage = "Invalid Phone Number. Please enter a valid 10 or 11 digit phone number without spaces, parentheses, or dashes.")]
        public string Cell { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Gender { get; set; }
        public Nullable<int> MarkerId { get; set; }
        public Nullable<int> ContactId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
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
