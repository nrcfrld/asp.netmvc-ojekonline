//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _190442010026.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class pengemudi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pengemudi()
        {
            this.pendapatans = new HashSet<pendapatan>();
        }
    
        public int id { get; set; }
        public string nama_pengemudi { get; set; }
        public string plat_nomor { get; set; }
        public string nomor_sim { get; set; }
        public Nullable<decimal> gaji_perjam { get; set; }
        public Nullable<decimal> insentif_makan { get; set; }
        public Nullable<decimal> insentif_bensin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pendapatan> pendapatans { get; set; }
    }
}
