using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement
{
    class Errors
    {
        private int _fid;
        private string _error;
        private DateTime _created;
        private DateTime _edited;
        private bool _isRepaired;

        public int Fid
        {
            get { return _fid;}
            set { _fid = value; }

        }

        public string Error
        {
            get { return _error;}
            set { _error = value; }

        }
        public DateTime Created
        {
            get { return _created;}
            set { _created = value; }

        }
        public DateTime Edited
        {
            get { return _edited;}
            set { _edited = value; }

        }
        public bool isRepaired
        {
            get { return _isRepaired;}
            set { _isRepaired = value; }

        }

    }
}
