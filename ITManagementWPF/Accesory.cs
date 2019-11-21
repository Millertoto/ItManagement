namespace ITManagementWPF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Accesory
    {
        [Key]
        public int Tid { get; set; }

        public bool IsBorrowed { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }
    }
}
