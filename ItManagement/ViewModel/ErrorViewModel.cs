﻿using System;
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

        private string _selectedErrorDescription;
        private Error _selected;
        private Error _toBeCreated;
        private List<Equipment> _listOfEquipment;
        private int _uid;
        private string _errorText;
        private Employee _creatorOfError;
        private RelayCommand _addErrorButton;
        private RelayCommand _getErrors;
        private Equipment _currentEquipment;
        private RelayCommand _deleteButton;
        private RelayCommand _editButton;
        private RelayCommand _fixButton;
        private List<Error> _newListOfError;
        private ObservableCollection<Error> _newObsErrors;


        #endregion

        #region Constructor
        public ErrorViewModel()
        {
            _creatorOfError = Singleton.Instance.CurrentUser;
            _newListOfError = new List<Error>();
            _newObsErrors = new ObservableCollection<Error>();
            _addErrorButton = new RelayCommand(AddError);
            _deleteButton = new RelayCommand(DeleteMethod);
            _editButton = new RelayCommand(EditMethod);
            _listOfEquipment = Singleton.Instance.EQP.GetEquipments().Result;
            _goBack = new RelayCommand(GoBackMethod);
            _fixButton = new RelayCommand(FixMethod);



            NewConvertToObs();


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

        /// <summary>
        /// Edited 13/12
        /// </summary>
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

        public Employee CreatorOfError
        {
            get { return _creatorOfError; }
            set
            {
                _creatorOfError = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Edited 13/12
        /// </summary>
        public string ErrorDescription
        {
            get { return _selectedErrorDescription; }
            set
            {
                _selectedErrorDescription = value;
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
                    if (ErrorDescription.Length <= 255)
                    {
                        _toBeCreated = new Error(CreatorOfError.Cpr, uid, ErrorDescription, false, 1234567891);

                        await Singleton.Instance.ERP.CreateError(_toBeCreated);
                        CurrentEquipment.IsWorking = false;
                        await Singleton.Instance.EQP.UpdateEquipment(CurrentEquipment.Uid, CurrentEquipment);
                        var messageDialogue = new MessageDialog($"Error report has been created for the equipment with the following ID: {uid}");
                        messageDialogue.Commands.Add(new UICommand("Close"));
                        await messageDialogue.ShowAsync();

                    }
                    else
                    {
                        var messageDialogue = new MessageDialog($"Error message is too long, keep it within 255 characters");
                        messageDialogue.Commands.Add(new UICommand("Close"));
                        await messageDialogue.ShowAsync();

                        throw new ArgumentException("Failure to add error due to description being too long");
                    }
                   
                }
                


            }
            else
            {
                var messageDialogue = new MessageDialog($"The given uid, {UidForCreation}, does not exist");
                messageDialogue.Commands.Add(new UICommand("Close"));
                await messageDialogue.ShowAsync();

                throw new ArgumentException("Failure to add error due to wrong UID");
            }

            UidForCreation = 0;
            CurrentEquipment = null;
            NewObsErrors.Clear();
            NewConvertToObs();


        }

        public async void EditMethod()
        {
            if (SelectedError.IsRepaired == false)
            {
                if (SelectedError.ErrorMessage.Length <= 255)
                {
                    SelectedError.ErrorMessage = ErrorDescription;
                    SelectedError.Update = DateTime.Now;
                    await Singleton.Instance.ERP.UpdateError(SelectedError.Fid, SelectedError);
                    var messageDialogue = new MessageDialog($"Error: {SelectedError} has been updated");
                    messageDialogue.Commands.Add(new UICommand("Close"));
                    await messageDialogue.ShowAsync();

                    

                }
                else
                {
                    var messageDialogue = new MessageDialog($"Error message is too long, keep it within 255 characters");
                    messageDialogue.Commands.Add(new UICommand("Close"));
                    await messageDialogue.ShowAsync();

                    throw new ArgumentException("Failure to edit error due to description being too long");
                }
            }
            else
            {
                var messageDialogue = new MessageDialog($"Selected error has been marked as resolved and is no longer open for editing");
                messageDialogue.Commands.Add(new UICommand("Close"));
                await messageDialogue.ShowAsync();

                throw new ArgumentException("Failure to edit error due to the error being closed for editing");
            }
            
            NewObsErrors.Clear();
            NewConvertToObs();

        }

        public async void FixMethod()
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

                    var messageDialogue = new MessageDialog($"Error report: {SelectedError.Fid}, has been resolved");
                    messageDialogue.Commands.Add(new UICommand("Close"));
                    await messageDialogue.ShowAsync();

                }
                else
                {
                    var messageDialogue = new MessageDialog($"Select an error you wish to resolve");
                    messageDialogue.Commands.Add(new UICommand("Close"));
                    await messageDialogue.ShowAsync();

                    throw new ArgumentException("Failure to fix error due to not having selected the error");
                }
            }
            else
            {
                var messageDialogue = new MessageDialog($"Error report: {SelectedError.Fid}, has been marked as resolved and is no longer open for editing");
                messageDialogue.Commands.Add(new UICommand("Close"));
                await messageDialogue.ShowAsync();
                throw new ArgumentException("Failure to fix error due to the error no longer being open for fixing");
            }
            
            

            CurrentEquipment = null;
            NewObsErrors.Clear();
            NewConvertToObs();

        }

        public async void DeleteMethod()
        {
            if (SelectedError != null)
            {
                await Singleton.Instance.ERP.DeleteError(SelectedError.Fid);
                var messageDialogue = new MessageDialog($"Error report has been removed");
                messageDialogue.Commands.Add(new UICommand("Close"));
                await messageDialogue.ShowAsync();

            }
            else
            {
                var messageDialogue = new MessageDialog($"You have to select an error you wish to remove");
                messageDialogue.Commands.Add(new UICommand("Close"));
                await messageDialogue.ShowAsync();

                throw new ArgumentException("Failure to remove error due to the error not being selected");
            }
            NewObsErrors.Clear();
            NewConvertToObs();
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
