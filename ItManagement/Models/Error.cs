﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace ItManagement
{
    public partial class Error
    {
        public int Fid { get; set; }
        public int? Cpr { get; set; }
        public int? Uid { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Create { get; set; }
        public DateTime? Update { get; set; }
        public bool IsRepaired { get; set; }

        public virtual Employee CprNavigation { get; set; }
        public virtual Equipment U { get; set; }
    }
}