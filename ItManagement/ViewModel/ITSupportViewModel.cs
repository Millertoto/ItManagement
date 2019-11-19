using System;
using ItManagement.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.ViewModel
{
    class ITSupportViewModel : INotifyPropertyChanged
    {
        private Teacher _domainObjectITS;

        public ITSupportViewModel()
        {
            _domainObjectITS = new IT_Support();
        }

        public int ITSCPR
        {
            get { return _domainObjectITS.CPR; }
            set
            {
                _domainObjectITS.CPR = value;
                OnPropertyChanged();
            }
        }

        public string ITSUserName
        {
            get { return _domainObjectITS.UserName; }
            set
            {
                _domainObjectITS.UserName = value;
                OnPropertyChanged();
            }
        }

        public string ITSPassword
        {
            get { return _domainObjectITS.Password; }
            set
            {
                _domainObjectITS.Password = value;
                OnPropertyChanged();
            }
        }

        public string TeName
        {
            get { return _domainObjectITS.Name; }
            set
            {
                _domainObjectITS.Name = value;
                OnPropertyChanged();
            }
        }

        public bool TeIsAdmin
        {
            get { return _domainObjectITS.IsAdmin; }

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
