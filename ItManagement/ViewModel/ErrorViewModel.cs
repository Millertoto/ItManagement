using System;
using ItManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using ItManagement.Commands;
using ItManagement.Folder;
using ItManagement.Persistencies;


namespace ItManagement.ViewModel
{
    public class ErrorViewModel : INotifyPropertyChanged
    {


        #region Instance Field
        //private ErrorsCatalogSingleton singleton;
        //private ObservableCollection<Errors> _errors;
        private Error _selected;
        private List<Equipment> _listOfEquipment;
        private int _uid;
        private string _errorText;
        private Employee _creatorOfError;
        private RelayCommand _addErrorButton;
        private RelayCommand _getErrors;


        //private SkoledbContext _dbcontext;


        #endregion




        

        #region Constructor
        public ErrorViewModel()
        {
            _creatorOfError = EmployeeSingleton.Instance.CurrentUser;
            _addErrorButton = new RelayCommand(AddError);
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
            get { return _uid; }
            set
            {
                _uid = value;
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

        public List<Equipment> ListOfEquipment
        {
            get { return _listOfEquipment; }
            set { _listOfEquipment = value; }




        }


        #endregion

        #region RelayCommands

        public RelayCommand AddErrorButton
        {
            get { return _addErrorButton; }
            set { _addErrorButton = value; }
        }
        #endregion

        #region Methods


        public async void AddError()
        {

            int uid = UidForCreation;
            if (EquipmentCheck(uid))
            {
                Error e = new Error(CreatorOfError.Cpr, uid, ErrorDescription);
                await WebApiError.PostError("api/Errors/", e);
            }
            


        }

        public bool EquipmentCheck(int uid)
        {
            
            bool c = false;
            ListOfEquipment = WebApiEquipment.GetEquipment("api/Equipments/");

            foreach (Equipment e in ListOfEquipment)
            {
                if (uid == e.Uid)
                {
                    c = true;
                    break;
                }
            }

            return c;


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
