using System;
using ItManagement.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace ItManagement.ViewModel
{
    public class TabletViewModel : INotifyPropertyChanged
    {
        private Tablet  _domainObjectT;

        public TabletViewModel()
        {
            _domainObjectT = new Tablet();
        }

        public string TType
        {
            get { return _domainObjectT.Type; }

            set
            {
                _domainObjectT.Type = value;
                OnPropertyChanged();
            }
        }

        public int TID
        {
            get { return _domainObjectT.EquipmentID; }

            set
            {
                _domainObjectT.EquipmentID = value;
                OnPropertyChanged();
            }
        }

        public bool TIsWorking
        {
            get { return _domainObjectT.IsWorking; }

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
