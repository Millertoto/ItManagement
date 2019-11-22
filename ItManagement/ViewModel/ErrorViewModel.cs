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
            var e1 = new Employee(68486, "", "", "", false);
            var eq1 = new Computer("", 897);
            _selected = new Errors("",eq1,e1);
            singleton = ErrorsCatalogSingleton.Instance;

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
                _errors = new ObservableCollection<Errors>(singleton.ErrorsList);
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
