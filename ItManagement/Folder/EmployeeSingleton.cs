using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.Folder
{
    class EmployeeSingleton
    {
        #region Instance Field

        private Employee _currentUser;
        private static EmployeeSingleton _instance;


        #endregion

        #region Constructor

        #endregion

        #region Properties
        public Employee CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        public static EmployeeSingleton Instance
        {
            get
            {
                if (_instance == null) _instance = new EmployeeSingleton();
                return _instance;
            }
        }

        #endregion

        #region Methods

        public void SetCurrentUser(Employee setCurrentUser)
        {
            CurrentUser = setCurrentUser;
        }



        #endregion
    }
}
