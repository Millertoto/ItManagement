namespace SkoleDBWebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SmartBoard")]
    public partial class SmartBoard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Uid { get; set; }

        public int Roomid { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}
