using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.Models

{
    class Equipment
    {
        #region Instance Field

        private string _type;
        private int _uid;
        private bool _isWorking;

        #endregion

        /*#region Constructor

        public Equipment(string type, int eid)
        {
            _type = type;
            _eid = eid;
            _isWorking = true;
        }

        #endregion*/

        #region Properties

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public int EquipmentID
        {
            get { return _uid;}
        }

        public bool IsWorking
        {
            get { return _isWorking;}
            set { _isWorking = value; }
        }
        


        #endregion

        #region Methods

        #endregion
    }
}
