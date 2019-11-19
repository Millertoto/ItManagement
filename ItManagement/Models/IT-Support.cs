using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement
{
    class ItSupport : Employee
    {
        

        public ItSupport(int cpr, string userName, string password, string name, bool isAdmin)
            : base(cpr, userName, password, name, isAdmin)
        {
            
        }

        public int ITCPR { get; set; }

        public string ITUserName { get; set; }

        public string ITPassword { get; set; }

        public string ITName { get; set; }

        public bool ITIsAdmin { get; set; }

    }
}
