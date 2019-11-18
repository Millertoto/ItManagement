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

        public Errors(int fid, string error, DateTime created, DateTime edited, bool isRepaired)
        {
            _fid = fid;
            _error = error;
            _created = created;
            _edited = edited;
            _isRepaired = isRepaired;
        }

        public int Fid { get; set; }

        public string Error { get; set; }

        public DateTime Created { get; set; }

        public DateTime Edited { get; set; }

        public bool IsRepaired { get; set; }

    }
}
