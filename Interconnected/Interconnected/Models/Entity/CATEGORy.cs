namespace Interconnected.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CATEGORIES")]
    public partial class CATEGORy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CATEGORy()
        {
            POSTs = new HashSet<POST>();
        }

        public int ID { get; set; }

        [StringLength(255)]
        public string NAME { get; set; }

        [StringLength(255)]
        public string DESCRIPTION { get; set; }

        public int? ID_PARENT { get; set; }

        [StringLength(255)]
        public string URL_SLUG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POST> POSTs { get; set; }
    }
}
