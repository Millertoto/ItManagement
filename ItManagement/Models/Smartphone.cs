﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace ItManagement
{
    public partial class SmartPhone
    {
        public int Uid { get; set; }
        public bool IsBorrowed { get; set; }

        public virtual Equipment U { get; set; }
    }
}