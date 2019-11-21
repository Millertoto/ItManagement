using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement
{
    public class Teacher : Employee
    {
        public Teacher(int cpr, string userName, string password, string name, bool isAdmin)
            : base(cpr, userName, password, name, isAdmin)
        {
            
        }

        public int TCPR { get; set; }

        public string TUserName { get; set; }

        public string TPassword { get; set; }

        public string TName { get; set; }

        public bool TIsAdmin { get; set; }
    }
}
