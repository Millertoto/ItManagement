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
    public class ComputerViewModel : INotifyPropertyChanged
    {
        private Computer _domainObjectC;

        public ComputerViewModel()
        {
            _domainObjectC = new Computer("",6841);
        }

        public string Type
        {
            get { return _domainObjectC.Type; }
            set
            {
                _domainObjectC.Type = value;
                OnPropertyChanged();
            }
        }

        public int CID
        {
            get { return _domainObjectC.EquipmentID; }

            set
            { _domainObjectC.EquipmentID = value;
                OnPropertyChanged();
            }

            
        }

        

        public bool CIsWorking
        {
            get { return _domainObjectC.IsWorking; }
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
        

    

