namespace SkoleDBWebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Error
    {
        [Key]
        public int Fid { get; set; }

        public int Cpr { get; set; }

        public int Uid { get; set; }

        [Required]
        public string ErrorMessage { get; set; }

        public DateTime Create { get; set; }

        public DateTime? Update { get; set; }

        public bool IsRepaired { get; set; }

        public int? HasRepaired { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}
