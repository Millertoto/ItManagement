﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace ItManagement
{
    public partial class Employee
    {
        #region Instance Field

        private int _cpr;
        private string _username;
        private string _password;
        private string _name;
        private bool _isAdmin;
        #endregion

        #region Constructor
        public Employee(string username, int cpr, string password, string name, bool isadmin )
        {
            Errors = new HashSet<Error>();
            _username = username;
            _cpr = cpr;
            _password = password;
            _name = name;
            _isAdmin = isadmin;
        }
        #endregion

        #region Properties
        public int Cpr
        {
            get { return _cpr; }
            set { _cpr = value; }
        }

        public string Username
        {
            get { return _username;}
            set { _username = value; }

        }

        public string Password
        {
            get { return _password;}
            set { _password = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; }
        }

        public virtual ICollection<Error> Errors { get; set; }
    }
    #endregion
}