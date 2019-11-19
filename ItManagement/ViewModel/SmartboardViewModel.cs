using ItManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.ViewModel
{
    class SmartboardViewModel : INotifyPropertyChanged
    {

        private Smartboard _domainObejctSB;


        public SmartboardViewModel()
        {
            _domainObejctSB = new Smartboard();
        }

        public string Type
        {
            get { return _domainObejctSB.Type; }

            set
            {
                _domainObejctSB.Type = value;
                OnPropertyChanged();
            }
        }

        public int SBID
        {
            get { return _domainObejctSB.EquipmentID; }

            set
            {
                _domainObejctSB.EquipmentID = value;
                OnPropertyChanged();
            }
        }

        public int SBRoom
        {
            get { return _domainObejctSB.RoomID; }

            set
            {
                _domainObejctSB.RoomID = value;
                OnPropertyChanged();
            }
        }

        public bool SBIsWorking
        {
            get { return _domainObejctSB.IsWorking; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged
            ([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new
                PropertyChangedEventArgs(propertyName));
        }
    }
}
