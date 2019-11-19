using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItManagement.Models;

namespace ItManagement
{
    class Errors
    {
        private int _fid;
        private string _error;
        private DateTime _created;
        private DateTime _edited;
        private bool _isRepaired;
        private List<Equipment> _equipments;

        public Errors(string error, DateTime created, DateTime edited, bool isRepaired)
        {
            
            _error = error;
            _created = created;
            _edited = edited;
            _isRepaired = false;
        }

        public int Fid { get; }

        public string Error { get; set; }

        public DateTime Created { get; set; }

        public DateTime Edited { get; set; }

        public bool IsRepaired { get; set; }

        public List<Equipment> EquipmentList { get; set; }

        public Employee Employee { get; set; }

    }
}
