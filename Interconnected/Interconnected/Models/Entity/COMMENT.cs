namespace Interconnected.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COMMENTS")]
    public partial class COMMENT
    {
        public int ID { get; set; }

        [Required]
        public string DETAIL { get; set; }

        public int ID_USER { get; set; }

        public int ID_POST { get; set; }

        public int? ID_PARENT { get; set; }

        public DateTime? DATE_CREATED { get; set; }

        public DateTime? DATE_EDIT { get; set; }

        public virtual POST POST { get; set; }

        public virtual USER USER { get; set; }
    }
}
