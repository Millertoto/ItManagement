namespace SkoleDBWebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Equipment")]
    public partial class Equipment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Equipment()
        {
            Errors = new HashSet<Errors>();
        }

        [Key]
        public int Uid { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        public bool IsWorking { get; set; }

        public virtual Computer Computer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Errors> Errors { get; set; }

        public virtual SmartBoard SmartBoard { get; set; }

        public virtual SmartPhone SmartPhone { get; set; }

        public virtual Tablet Tablet { get; set; }
    }
}
