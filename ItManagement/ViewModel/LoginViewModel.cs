﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ItManagement.Commands;
using ItManagement.View;

namespace ItManagement.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged
    {

        #region Instance Field

        private string _userName;
        private string _password;
        private RelayCommand _enter;
        private Employee _CurrentUser;


        #endregion
        #region Constructor

        public LoginViewModel()
        {
            RelayCommand Enter = new RelayCommand(LoginButtonMethod);
        }
        
        #endregion
        #region Properties

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public Employee CurrentUser
        {
            get { return _CurrentUser; }
            set { _CurrentUser = value; }
        }
        #endregion
        #region RelayCommands
        #endregion
        #region Methods

        public void LoginButtonMethod()
        {
            if (LoginCheck(UserName, Password))
            {
                using (var db = new SkoledbContext())
                {
                    foreach (Employee e in db.Employees)
                    {
                        if ((UserName == e.Username) && (Password == e.Password))
                        {
                            _CurrentUser = e;
                            
                        }   

                        break;
                    }

                    if (AdminCheck(CurrentUser))
                    {
                        Frame currentFrame = Window.Current.Content as Frame;
                        currentFrame.Navigate(typeof(AdminMainpage));
                    }
                    else
                    {
                        Frame currentFrame = Window.Current.Content as Frame;
                        currentFrame.Navigate(typeof(ErrorPageTeacher));
                    }
                }
            }
            /*else
            {
            some kind of error
            }*/

        }

        public bool AdminCheck(Employee currentUser)
        {
            if (CurrentUser.IsAdmin)
            {
                return true;
            }
            else
            {
                return false;
            }
                
            
        }

        public bool LoginCheck(string username, string password)
        {
            bool c = false;

            using (var db = new SkoledbContext())
            {
                foreach (Employee e in db.Employees)
                {
                    if (username == e.Username && password == e.Password)
                    {
                        c = true;
                    }
                    
                }
            }
            return c;
        }
        #endregion

        #region INotifyPropertyChanged

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
