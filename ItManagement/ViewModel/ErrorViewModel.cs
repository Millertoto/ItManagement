using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace ItManagement.ViewModel
{
    /*public class ErrorViewModel : INotifyPropertyChanged
    {
        private ErrorsCatalogSingleton singleton;
        private ObservableCollection<Errors> _errors;
        private Errors _selected;

        public ErrorViewModel()
        {
            _selected = new Errors();

        }

        private int _id;
        public int FID
        {
            get { return _id; }

            set
            { _id = value;
                OnPropertyChanged();
            }
        }

        private string _error;
        public string Error
        {
            get { return _error; }

            set
            {
                _error = value;
                OnPropertyChanged();
            }
        }

        public Errors Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        public int ErrorsCount
        {
            get { return singleton.Count; }
        }

        public ObservableCollection<Errors> All_Errors
        {
            get
            {
                _errors = new ObservableCollection<Errors>(singleton);
                return _errors;
            }
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
