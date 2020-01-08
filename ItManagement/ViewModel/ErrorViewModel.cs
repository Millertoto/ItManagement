using System;
using ItManagement;
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
        // Skrevet af Martin og David


        #region Instance Field

        private List<Equipment> _listOfEquipment;
        private List<Error> _newListOfError;

        private ObservableCollection<Error> _newObsErrors;

        private int _uid;

        private string _selectedErrorDescription;

        private Employee _creatorOfError;

        private Equipment _currentEquipment;

        private Error _selected;
        private Error _toBeCreated;

        private RelayCommand _addErrorButton;
        private RelayCommand _deleteButton;
        private RelayCommand _editButton;
        private RelayCommand _fixButton;
        private RelayCommand _goBack;
        private RelayCommand _goBackTeacher;


        #endregion

        #region Constructor
        public ErrorViewModel()
        {
            _creatorOfError = Singleton.Instance.CurrentUser;

            _newListOfError = new List<Error>();
            _listOfEquipment = Singleton.Instance.EQP.GetEquipments().Result;

            _newObsErrors = new ObservableCollection<Error>();

            _addErrorButton = new RelayCommand(AddError);
            _deleteButton = new RelayCommand(DeleteMethod);
            _editButton = new RelayCommand(EditMethod);
            _goBack = new RelayCommand(GoBackMethod);
            _fixButton = new RelayCommand(FixMethod);
            _goBackTeacher = new RelayCommand(GoBackTeacherMethod);
            _selected = new Error();


            NewConvertToObs();


        }
        #endregion

        #region Properties

        #region ErrorProps

        public Error SelectedError
        {
            get { return _selected; }

            set
            {
                _selected = value;
                OnPropertyChanged(nameof(SelectedError));
            }
        }

        public string ErrorDescription
        {
            get { return _selectedErrorDescription; }
            set
            {
                _selectedErrorDescription = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region EmployeeProps

        public Employee CreatorOfError
        {
            get { return _creatorOfError; }
            set
            {
                _creatorOfError = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region EquipmentProps

        public int UidForCreation
        {
            get { return _uid; }
            set { _uid = value; }
        }

        public Equipment CurrentEquipment
        {
            get { return _currentEquipment; }
            set { _currentEquipment = value; }
        }

        #endregion

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

        public List<Error> NewErrorList
        {
            get { return _newListOfError; }
            set
            {
                _newListOfError = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Error> NewObsErrors
        {
            get { return _newObsErrors; }
            set { _newObsErrors = value; }
        }


        #endregion

        #region RelayCommands

        public RelayCommand FixButton
        {
            get { return _fixButton; }
            set { _fixButton = value; }
        }
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

        public RelayCommand GoBack
        {
            get { return _goBack; }
            set { _goBack = value; }
        }

        public RelayCommand GoBackTeacher
        {
            get { return _goBackTeacher; }
            set { _goBackTeacher = value; }
        }

        #endregion

        #endregion

        #region Methods

        #region AddError
        /// <summary>
        /// Tilføjer en fejl
        /// </summary>
        public async void AddError()
        {

            int uid = SelectedError.Uid;
            if (EquipmentCheck(uid) && uid != 0)
            {
                if (SelectedError.ErrorMessage == null)
                {
                    var messageDialogue = new MessageDialog($"Du skal skrive en beskrivelse af fejlen");
                    messageDialogue.Commands.Add(new UICommand("Luk"));
                    await messageDialogue.ShowAsync();

                }
                else
                {
                    if (SelectedError.ErrorMessage.Length <= 255)
                    {
                        _toBeCreated = new Error(CreatorOfError.Cpr, uid, SelectedError.ErrorMessage, false, 1234567891);

                        await Singleton.Instance.ERP.CreateError(_toBeCreated);
                        CurrentEquipment.IsWorking = false;
                        await Singleton.Instance.EQP.UpdateEquipment(CurrentEquipment.Uid, CurrentEquipment);
                        var messageDialogue = new MessageDialog($"Fejlmeldingen angående udstyr: {uid}, er blevet oprettet");
                        messageDialogue.Commands.Add(new UICommand("Luk"));
                        await messageDialogue.ShowAsync();

                    }
                    else
                    {
                        var messageDialogue = new MessageDialog($"Fejlmbeskrivelsen er for lang, hold det inde for 255 karaktere");
                        messageDialogue.Commands.Add(new UICommand("Luk"));
                        await messageDialogue.ShowAsync();

                    }
                   
                }
                


            }
            else
            {
                var messageDialogue = new MessageDialog($"Det given uid, {SelectedError.Uid}, eksisterer ikke");
                messageDialogue.Commands.Add(new UICommand("Luk"));
                await messageDialogue.ShowAsync();

            }

            CurrentEquipment = null;
            NewObsErrors.Clear();
            NewConvertToObs();


        }
        #endregion

        #region EditError
        /// <summary>
        /// Ændrer en fejl
        /// </summary>
        public async void EditMethod()
        {
            if (SelectedError != null || SelectedError.Fid != 0)
            {
                if (SelectedError.IsRepaired == false)
                {
                    if (SelectedError.ErrorMessage.Length <= 255)
                    {
                        SelectedError.Update = DateTime.Now;
                        await Singleton.Instance.ERP.UpdateError(SelectedError.Fid, SelectedError);
                        var messageDialogue = new MessageDialog($"Fejl: {SelectedError.Fid} er blevet opdateret");
                        messageDialogue.Commands.Add(new UICommand("Luk"));
                        await messageDialogue.ShowAsync();



                    }
                    else
                    {
                        var messageDialogue = new MessageDialog($"Fejlbeskrivelsen er for lang, hold det inde for 255 karaktere");
                        messageDialogue.Commands.Add(new UICommand("Luk"));
                        await messageDialogue.ShowAsync();

                    }
                }
                else
                {
                    var messageDialogue = new MessageDialog($"Valgte fejl er blevet fixet og er derfor ikke længere åben for redigering");
                    messageDialogue.Commands.Add(new UICommand("Luk"));
                    await messageDialogue.ShowAsync();

                }

            }
            
            
            NewObsErrors.Clear();
            NewConvertToObs();

        }
        #endregion

        #region FixError
        /// <summary>
        /// Fixer en fejl
        /// </summary>
        public async void FixMethod()
        {
            if (SelectedError != null || SelectedError.Fid != 0)
            {
                if (SelectedError.IsRepaired == false)
                {
                    if (SelectedError != null)
                    {
                        SelectedError.IsRepaired = true;
                        SelectedError.HasRepaired = Singleton.Instance.CurrentUser.Cpr;
                        SelectedError.Update = DateTime.Now;
                        await Singleton.Instance.ERP.UpdateError(SelectedError.Fid, SelectedError);

                        EquipmentCheck(SelectedError.Uid);
                        CurrentEquipment.IsWorking = true;
                        await Singleton.Instance.EQP.UpdateEquipment(CurrentEquipment.Uid, CurrentEquipment);

                        var messageDialogue = new MessageDialog($"Fejlmelding: {SelectedError.Fid}, er blevet løst!");
                        messageDialogue.Commands.Add(new UICommand("Luk"));
                        await messageDialogue.ShowAsync();

                    }
                    else
                    {
                        var messageDialogue = new MessageDialog($"Vælg en fejl før du kan løse den");
                        messageDialogue.Commands.Add(new UICommand("Luk"));
                        await messageDialogue.ShowAsync();

                    }
                }
                else
                {
                    var messageDialogue = new MessageDialog($"Fejlmelding: {SelectedError.Fid}, er allerede løst");
                    messageDialogue.Commands.Add(new UICommand("Luk"));
                    await messageDialogue.ShowAsync();
                }
            }
            
            
            

            CurrentEquipment = null;
            NewObsErrors.Clear();
            NewConvertToObs();

        }
        #endregion

        #region DeleteError
        /// <summary>
        /// Fjerner en fejl
        /// </summary>
        public async void DeleteMethod()
        {

            if (SelectedError != null)
            {
                await Singleton.Instance.ERP.DeleteError(SelectedError.Fid);
                var messageDialogue = new MessageDialog($"Fejlmelding fjernet");
                messageDialogue.Commands.Add(new UICommand("Luk"));
                await messageDialogue.ShowAsync();

            }
            else
            {
                var messageDialogue = new MessageDialog($"Vælg en fejl før du kan fjerne den");
                messageDialogue.Commands.Add(new UICommand("Luk"));
                await messageDialogue.ShowAsync();

            }
            NewObsErrors.Clear();
            NewConvertToObs();
        }
        #endregion

        #region Checks
        /// <summary>
        /// Checker om et udstyr eksisterer
        /// </summary>
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
        #endregion

        #region List/Obs Method
        public void NewConvertToObs()
        {
            NewErrorList = Singleton.Instance.ERP.GetErrors().Result;

            if (NewErrorList != null)
            {
                foreach (Error e in NewErrorList)
                {
                    NewObsErrors.Add(e);
                }
            }
            else
            {

            }
        }
        #endregion

        #region GoBack


        public void GoBackMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(AdminMainpage));
        }

        public void GoBackTeacherMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(MainPage));
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

        #endregion

    }
}
