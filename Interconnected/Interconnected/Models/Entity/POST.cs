namespace Interconnected.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("POST")]
    public partial class POST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public POST()
        {
            COMMENTS = new HashSet<COMMENT>();
            VOTEs = new HashSet<VOTE>();
        }

        public int ID { get; set; }

        [StringLength(255)]
        public string TITLE { get; set; }

        [StringLength(255)]
        public string PICTURE { get; set; }

        [StringLength(500)]
        public string DESCRIPTION { get; set; }

        [StringLength(4000)]
        public string DETAIL { get; set; }

        public int ID_CATEGORY { get; set; }

        public DateTime? DATE_CREATED { get; set; }

        public DateTime? DATE_EDIT { get; set; }

        public int ID_USER { get; set; }

        public bool ACTIVE { get; set; }

        [StringLength(255)]
        public string URL_SLUG { get; set; }

        public virtual CATEGORy CATEGORy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENT> COMMENTS { get; set; }

        public virtual USER USER { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VOTE> VOTEs { get; set; }
    }
}
