﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace ItManagement
{
    public partial class Equipment
    {
        private string _type;
        public Equipment(string Type)
        {
            _type = Type;
            Errors = new HashSet<Error>();
            IsWorking = true;
        }

        public int Uid { get; set; }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public bool IsWorking { get; set; }

        public virtual Computer Computer { get; set; }
        public virtual SmartBoard SmartBoard { get; set; }
        public virtual SmartPhone SmartPhone { get; set; }
        public virtual Tablet Tablet { get; set; }
        public virtual ICollection<Error> Errors { get; set; }
    }
}