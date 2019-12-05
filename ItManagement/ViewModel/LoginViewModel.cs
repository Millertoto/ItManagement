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
using ItManagement.Persistencies;

namespace ItManagement.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged
    {

        #region Instance Field

        private string _userName;
        private string _password;
        private RelayCommand _enter;
        private Employee _currentUser;
        private List<Employee> _employees;
        private const string _employeeurl = "api/Employees/";


        #endregion
        #region Constructor

        public LoginViewModel()
        {
            _enter = new RelayCommand(LoginButtonMethod);
        }
        
        #endregion
        #region Properties

        public List<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; }
        }

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

        public RelayCommand Enter
        {
            get { return _enter; }
            set { _enter = value; }


        }
        #endregion
        #region Methods

        public void LoginButtonMethod()
        {

            Employees = WebApi<Employee>.GetList(_employeeurl);


            if (LoginCheck(UserName, Password, Employees))
            {
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
            /*else
            {
            some kind of error
            }*/

        }

        public bool AdminCheck(Employee currentUser)
        {
            if (currentUser.IsAdmin)
            {
                return true;
            }

            /*removable*/
            else
            {
                return false;
            }
                
            
        }

        public bool LoginCheck(string username, string password, List<Employee> employees)
        {
            bool c = false;

                foreach (Employee e in employees)
                {
                    if (username == e.Username && password == e.Password)
                    {
                        c = true;
                        EmployeeSingleton.Instance.CurrentUser = e;
                        break;
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
