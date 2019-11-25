using System;
using ItManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ItManagement.Commands;

    namespace ItManagement.ViewModel
{
    public class ErrorViewModel : INotifyPropertyChanged
    {


        #region Instance Field
        /*private ErrorsCatalogSingleton singleton;
        private ObservableCollection<Errors> _errors;*/
        private Error _selected;
        private List<Error> _listOfErrors;
        private int _uID;
        private string _errorText;
        private Employee _creatorOfError;
        private RelayCommand _addErrorButton;
        private RelayCommand _getErrors;


        /*private SkoledbContext _dbcontext;*/


        #endregion


        #region Constructor
        public ErrorViewModel(Employee SessionUser)
        {
            _creatorOfError = SessionUser;

        }
        #endregion

        #region Properties


        public Error SelectedError
        {
            get { return _selected; }

            set
            {
                _selected = value;
                OnPropertyChanged();
            }
        }

        public int UidForCreation
        {
            get { return _uID; }
            set
            {
                _uID = value;
                OnPropertyChanged();
            }
        }

        public Employee CreatorOfError
        {
            get { return _creatorOfError; }
            set
            {
                _creatorOfError = value;
                OnPropertyChanged();
            }
        }

        public string ErrorDescription
        {
            get
            {
                return _errorText;
            }
            set
            {
                _errorText = value;
                OnPropertyChanged();
            }
        }

        public List<Error> ListofErrors
        {
            get { return _listOfErrors; }
            set { _listOfErrors = value; }




        }



        /*public int ErrorsCount
        {
            get { return singleton.Count; }
        }*/

        /*public ObservableCollection<Error> All_Errors
        {
            get
            {
                _errors = new ObservableCollection<Error>(singleton.ErrorsList);
                return _error;
            }
        }*/

        #endregion

        #region RelayCommands

        public ICommand AddErrorButton
        {
            get
            {
                if (_addErrorButton == null)
                {
                    _addErrorButton = new RelayCommand(AddError);
                }

                return _addErrorButton;
            }


        }

        public ICommand GetErrorButton
        {
            get
            {
                if (_getErrors == null)
                {
                    _getErrors = new RelayCommand(GetErrors);
                }

                return _getErrors;
            }


        }
        #endregion

        #region Methods

        public void AddError()
        {
            Error e1 = new Error
            {
                Uid = _uID,
                Cpr = CreatorOfError.Cpr,
                ErrorMessage = _errorText,
                Create = DateTime.Now,
                Update = DateTime.Now,
                IsRepaired = false
            };

            using (var db = new SkoledbContext())
            {
                db.Add(e1);
                db.SaveChanges();
            }
        }

        public void GetErrors()
        {
            _listOfErrors.Clear();

            using (var db = new SkoledbContext())
            {
                foreach (Error e in db.Errors)
                {
                    _listOfErrors.Add(e);
                }
            }
        }
        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged
            ([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new
                PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}