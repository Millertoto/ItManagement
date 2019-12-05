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
using ItManagement.Commands;
using ItManagement.Folder;


namespace ItManagement.ViewModel
{
    public class ErrorViewModel : INotifyPropertyChanged
    {


        #region Instance Field
        //private ErrorsCatalogSingleton singleton;
        //private ObservableCollection<Errors> _errors;
        private Error _selected;
        private List<Error> _listOfErrors;
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
        #endregion

        #region Methods

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
