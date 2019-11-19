using System;
using ItManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ItManagement.ViewModel
{
    class ErrorViewModel : INotifyPropertyChanged
    {
        private Errors _domainObjectErr;

        public ErrorViewModel()
        {
            _domainObjectErr = new Errors();

        }

        public int FID
        {
            get { return _domainObjectErr.Fid; }

            set
            { _domainObjectErr.Fid = value;
                OnPropertyChanged();
            }
        }

        public string Error
        {
            get { return _domainObjectErr.Error; }

            set
            {
                _domainObjectErr.Error = value;
                OnPropertyChanged();
            }
        }

        public DateTime Created
        {
            get { return _domainObjectErr.Created; }

            set
            {
                _domainObjectErr.Created = value;
                OnPropertyChanged();
            }
        }

        public bool IsRepaired
        {
            get { return _domainObjectErr.IsRepaired; }
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
