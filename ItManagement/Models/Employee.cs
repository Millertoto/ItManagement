﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace ItManagement
{
    public partial class Employee
    {
        public Employee()
        {
            Errors = new HashSet<Error>();
        }

        public int Cpr { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Error> Errors { get; set; }
    }
}