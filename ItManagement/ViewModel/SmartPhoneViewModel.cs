using System;
using ItManagement;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.ViewModel
{
    /*public class SmartPhoneViewModel : INotifyPropertyChanged
    {
        private Smartphone _domainObjectSP;

        public SmartPhoneViewModel()
        {
            _domainObjectSP = new Smartphone();
        }

        public string Type
        {
            get { return _domainObjectSP.Type; }

            set
            {
                _domainObjectSP.Type = value;
                OnPropertyChanged();
            }
        }

        public int SPID
        {
            get { return _domainObjectSP.EquipmentID; }

            set
            {
                _domainObjectSP.EquipmentID = value;
                OnPropertyChanged();
            }
        }

        public bool SPIsWorking
        {
            get { return _domainObjectSP.IsWorking; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged
            ([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new
                PropertyChangedEventArgs(propertyName));
        }
    }*/
}
