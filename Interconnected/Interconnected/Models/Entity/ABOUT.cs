namespace Interconnected.Models.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ABOUTS")]
    public partial class ABOUT
    {
        public int ID { get; set; }

        [StringLength(4000)]
        public string CONTENTS { get; set; }
    }
}
