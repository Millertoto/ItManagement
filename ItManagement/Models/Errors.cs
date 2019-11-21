using ItManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItManagement.Models;

namespace ItManagement
{
    public class Errors
    {
        private int _fid;
        private string _error;
        private DateTime _created;
        private DateTime _edited;
        private bool _isRepaired;
        private int _equipmentID;
        private int _employeeID;


        public Errors(int fid, string error, Equipment equipment, Employee employee)

        {
            
            _error = error;
            _created = DateTime.Now;
            _edited = DateTime.Now;
            _isRepaired = false;
            _equipmentID = equipment.EquipmentID;
            _employeeID = employee.CPR;
            

        }

        public int Fid { get; }

        public string Error { get; set; }

        public DateTime Created { get; set; }

        public DateTime Edited { get; set; }

        public bool IsRepaired { get; set; }
        


    }
}
