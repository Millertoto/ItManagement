using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ItManagement.Commands;
using ItManagement.Folder;
using ItManagement.View;
using ItManagement.Models;

namespace ItManagement.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged
    {

        #region Instance Field

        private string _userName;
        private string _password;
        private RelayCommand _enter;
        private Employee _currentUser;


        #endregion
        #region Constructor

        public LoginViewModel()
        {
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
            get { return _currentUser; }
            set { _currentUser = value; }
        }
        #endregion
        #region RelayCommands

        public ICommand EnterCommand
        {
            get
            {
                if (_enter == null)
                {
                    _enter = new RelayCommand(LoginButtonMethod);
                }

                return _enter;
            }


        }
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
                            _currentUser = e;
                            EmployeeSingleton.Instance.CurrentUser = e;

                        }   

                        break;
                    }

                    if (AdminCheck(EmployeeSingleton.Instance.CurrentUser))
                    {
                        Frame currentFrame = Window.Current.Content as Frame;
                        currentFrame.Navigate(typeof(ErrorPageAdmin));
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

            /*removable*/
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
