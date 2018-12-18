namespace Interconnected.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VOTE")]
    public partial class VOTE
    {
        public int ID { get; set; }

        public int ID_POST { get; set; }

        public int ID_USER { get; set; }

        public virtual POST POST { get; set; }

        public virtual USER USER { get; set; }
    }
}
