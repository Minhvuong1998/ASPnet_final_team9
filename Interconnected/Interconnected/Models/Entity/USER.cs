namespace Interconnected.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USERS")]
    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            COMMENTS = new HashSet<COMMENT>();
            POSTs = new HashSet<POST>();
            VOTEs = new HashSet<VOTE>();
        }

        public int ID { get; set; }

        [StringLength(255)]
        public string EMAIL { get; set; }

        [StringLength(255)]
        public string FULLNAME { get; set; }

        [StringLength(255)]
        public string PICTURE { get; set; }

        [StringLength(20)]
        public string PHONE { get; set; }

        [StringLength(20)]
        public string ADDRESS { get; set; }

        public bool ACTIVE { get; set; }

        [StringLength(255)]
        public string PASSWORD { get; set; }

        public int ID_ROLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENT> COMMENTS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<POST> POSTs { get; set; }

        public virtual ROLE ROLE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VOTE> VOTEs { get; set; }
    }
}
