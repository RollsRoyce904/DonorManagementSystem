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
    
    public partial class CODE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CODE()
        {
            this.DONATIONs = new HashSet<DONATION>();
        }
    
        public int CodeId { get; set; }

        [Display(Name = "Fund")]
        public string Fund { get; set; }

        [Display(Name = "GL")]
        public string GL { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Display(Name = "Program")]
        public string Program { get; set; }

        [Display(Name = "Grant")]
        public string GrantS { get; set; }

        [Display(Name = "Appeal")]
        public string Appeal { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONATION> DONATIONs { get; set; }
    }
}
