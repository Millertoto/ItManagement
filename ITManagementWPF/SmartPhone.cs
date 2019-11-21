namespace ITManagementWPF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SmartPhone")]
    public partial class SmartPhone
    {
        [Key]
        public int Uid { get; set; }

        public bool IsBorrowed { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}
