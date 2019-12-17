using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItManagement.Persistencies;

namespace ItManagement.PersSingleton
{
    public class Singleton
    {
        // Skrevet af David og Martin

        #region Instance Field

        private Employee _currentUser;
        private static Singleton _instance;
        public EmployeePersistency EP = new EmployeePersistency();
        public EquipmentPersistency EQP = new EquipmentPersistency();
        public ErrorPersistency ERP = new ErrorPersistency();
        public TabletPersistency TAB = new TabletPersistency();
        public SmartboardPersistency SB = new SmartboardPersistency();
        public ComputerPersistency COM = new ComputerPersistency();
        public SmartphonePersistency SP = new SmartphonePersistency();


        #endregion

        #region Constructor

        #endregion

        #region Properties
        public Employee CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }


        // Laver en ny instance hvis der ikke eksistere en i forvejen
        public static Singleton Instance
        {
            get
            {
                if (_instance == null) _instance = new Singleton();
                return _instance;
            }
        }

        #endregion

        #region Methods
        // Tager fat i en bruger, så den samme bruger kan benyttes igennem hele programmet
        public void SetCurrentUser(Employee setCurrentUser)
        {
            CurrentUser = setCurrentUser;
        }



        #endregion
    }
}
