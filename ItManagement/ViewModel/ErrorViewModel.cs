using System;
using ItManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ItManagement.Commands;

namespace ItManagement.ViewModel
{
    public class ErrorViewModel : INotifyPropertyChanged
    {
        private ErrorCatalogSingleton singleton;
        private ObservableCollection<Errors> _errors;
        private Errors _selected;

        public ErrorViewModel()
        {
            _selected = new Errors();
            singleton = ErrorCatalogSingleton.Instance;
            _errors = new ObservableCollection<Errors>();
            AddCommand = new RelayCommand(toAddNewError);
            DeleteCommand = new RelayCommand(toDelete);
            UpdateCommand = new RelayCommand(toUpdate);
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

        public RelayCommand AddCommand
        {
            get; set;
        }

        public RelayCommand DeleteCommand
        {
            get; set;
        }

        public RelayCommand UpdateCommand
        {
            get; set;
        }

        public void toDelete()
        {
            singleton.DeleteError(Selected);
            OnPropertyChanged(nameof(All_Errors));
            OnPropertyChanged(nameof(ErrorsCount));
        }

        public void toUpdate()
        {
            singleton.UpdateError(Selected);
            OnPropertyChanged(nameof(All_Errors));
            OnPropertyChanged(nameof(ErrorsCount));
        }

        public void toAddNewError()
        {
            Errors NewStudent = new Errors(FID, "error", , );
            singleton.AddError(NewStudent);
            OnPropertyChanged(nameof(All_Errors));
            OnPropertyChanged(nameof(ErrorsCount));

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
