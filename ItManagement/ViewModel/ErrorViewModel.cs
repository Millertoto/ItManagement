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
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ItManagement.Commands;
using ItManagement.PersSingleton;
using ItManagement.Persistencies;
using ItManagement.View;


namespace ItManagement.ViewModel
{
    public class ErrorViewModel : INotifyPropertyChanged
    {


        #region Instance Field

        private Error _selected;
        private Error _toBeCreated;
        private List<Equipment> _listOfEquipment;
        private int _uid;
        private string _errorText;
        private Employee _creatorOfError;
        private RelayCommand _addErrorButton;
        private RelayCommand _getErrors;
        private List<Error> _allErrors;
        private Equipment _currentEquipment;
        private ObservableCollection<Error> _obsErrors;
        private RelayCommand _deleteButton;
        private RelayCommand _editButton;


        #endregion

        #region Constructor
        public ErrorViewModel()
        {
            _creatorOfError = Singleton.Instance.CurrentUser;
            _addErrorButton = new RelayCommand(AddError);
            _deleteButton = new RelayCommand(DeleteMethod);
            _editButton = new RelayCommand(EditMethod);
            _listOfEquipment = Singleton.Instance.EQP.GetEquipments().Result;
            _allErrors = Singleton.Instance.ERP.GetErrors().Result;
            _obsErrors = new ObservableCollection<Error>();
            _goBack = new RelayCommand(GoBackMethod);

            ConvertToObs();

            _uid = default(int);


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

        public Equipment CurrentEquipment
        {
            get { return _currentEquipment; }
            set { _currentEquipment = value; }
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

        private string _selectedOption;
        public string SelectedOption
        {
            get
            {
                return _selectedOption; ;
            }
            set
            {
                _selectedOption = value;
                OnPropertyChanged();
            }
        }
        #region Lists
        public List<Equipment> ListOfEquipment
        {
            get { return _listOfEquipment; }
            set
            {
                _listOfEquipment = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> IsWorking
        {
            get { return new ObservableCollection<string>(){"true", "false"}; }

        }
        public List<Error> ListOfErrors
        {
            get { return _allErrors; }
            set
            {
                _allErrors = value;
                OnPropertyChanged();
            }

        }

        public ObservableCollection<Error> ObsListOfErrors
        {
            get { return _obsErrors; }
            set
            {
                _obsErrors = value; 
                OnPropertyChanged();
            }
        }
        #endregion


        #endregion

        #region RelayCommands

        public RelayCommand EditButton
        {
            get { return _editButton; }
            set { _editButton = value; }
        }

        public RelayCommand AddErrorButton
        {
            get { return _addErrorButton; }
            set { _addErrorButton = value; }
        }

        public RelayCommand DeleteDis
        {
            get { return _deleteButton; }
            set { _deleteButton = value; }
        }
        #endregion

        
        #region Methods


        public async void AddError()
        {

            int uid = UidForCreation;
            if (EquipmentCheck(uid) && uid != 0)
            {
                if (ErrorDescription == null)
                {
                    var messageDialogue = new MessageDialog($"You need to type in a description for the error");
                    messageDialogue.Commands.Add(new UICommand("Close"));
                    await messageDialogue.ShowAsync();
                }
                else
                {
                    _toBeCreated = new Error(CreatorOfError.Cpr, uid, ErrorDescription);

                    await Singleton.Instance.ERP.CreateError(_toBeCreated);
                    CurrentEquipment.IsWorking = false;
                    await Singleton.Instance.EQP.UpdateEquipment(CurrentEquipment.Uid, CurrentEquipment);
                    var messageDialogue = new MessageDialog($"Error report has been created for the equipment with the following ID: {uid}");
                    messageDialogue.Commands.Add(new UICommand("Close"));
                    await messageDialogue.ShowAsync();
                }
                

            }
            ObsListOfErrors.Clear();
            ConvertToObs();


        }

        public async void EditMethod()
        {
            SelectedError.ErrorMessage = ErrorDescription;
            SelectedError.Update = DateTime.Now;
            await Singleton.Instance.ERP.UpdateError(SelectedError.Fid, SelectedError);
            ObsListOfErrors.Clear();
            ConvertToObs();

        }

        public async void DeleteMethod()
        {
            if (SelectedError != null)
            {
                await Singleton.Instance.ERP.DeleteError(SelectedError.Fid);
            }
            ObsListOfErrors.Clear();
            ConvertToObs();
        }

        public bool EquipmentCheck(int uid)
        {
            
            bool c = false;

            ListOfEquipment = Singleton.Instance.EQP.GetEquipments().Result;

            foreach (Equipment e in ListOfEquipment)
            {
                if (uid == e.Uid)
                {
                    c = true;
                    CurrentEquipment = e;
                    break;
                }
            }

            return c;


        }

        public void ConvertToObs()
        {
            ListOfErrors = Singleton.Instance.ERP.GetErrors().Result;
                foreach (Error e in ListOfErrors)
                {
                    ObsListOfErrors.Add(e);
                }
            
        }

        #endregion
        #region GoBack
        private RelayCommand _goBack;

        public RelayCommand GoBack
        {
            get { return _goBack; }
            set { _goBack = value; }
        }

        public void GoBackMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(AdminMainpage));
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
