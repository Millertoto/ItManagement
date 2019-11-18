using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement
{
    class Equipment
    {
        #region Instance Field

        private string _type;
        private int _eid;
        private bool _isWorking;

        #endregion
        #region Constructor

        public Equipment()
        {
        }

        #endregion

        #region Properties

        public string Type
        {
            get;
            set;
        }

        public int EquipmentID
        {
            get;
            set;
        }

        public bool IsWorking
        {
            get;
            set;
        }



        #endregion

        #region Methods

        #endregion
    }
}
