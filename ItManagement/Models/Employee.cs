using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement
{
    public class Employee
    {
        private int _cpr;
        private string _userName;
        private string _password;
        private string _name;
        private bool _isAdmin;

        public Employee(int cpr, string userName, string password, string name, bool isAdmin)
        {
            _cpr = cpr;
            _userName = userName;
            _password = password;
            _name = name;
            _isAdmin = isAdmin;
        }

        public int CPR { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public bool IsAdmin { get; set; }
    }

    
}
