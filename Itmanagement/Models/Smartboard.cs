using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement
{
    class Smartboard : Equipment
    {
        #region Instance Field

        private int _roomID;
        #endregion

        #region Constructor

        public Smartboard() : base()
        {

        }

        #endregion

        #region Properties

        public int RoomID
        {
            get;
            set;
        }

        #endregion

        #region Methods

        #endregion
    }
}
