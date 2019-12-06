using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Graphics.Holographic;
using Windows.Media.Streaming.Adaptive;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ItManagement.Commands;
using ItManagement.PersSingleton;
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


        #endregion

        #region Constructor

        public LoginViewModel()
        {
            _enter = new RelayCommand(LoginButtonMethod);
            /*Employee e = new Employee("AAA", 1307941500, "heyssa22", "Martin Holm", true);
            Singleton.Instance.EP.CreateEmployee(e);*/
        }

        #endregion

        #region Properties

        #region Lists
        public List<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; }
        }
        #endregion 

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

        public async void LoginButtonMethod()
        {

            Employees = Singleton.Instance.EP.GetEmployees().Result;


            if (LoginCheck(UserName, Password, Employees))
            {
                if (AdminCheck(Singleton.Instance.CurrentUser))
                {
                    Frame currentFrame = Window.Current.Content as Frame;
                    currentFrame.Navigate(typeof(ErrorPageAdmin));

                    var messageDialogue = new MessageDialog($"Welcome Back, {Singleton.Instance.CurrentUser.Name}");
                    messageDialogue.Commands.Add(new UICommand("Close"));
                    await messageDialogue.ShowAsync();
                }

                else
                {
                        Frame currentFrame = Window.Current.Content as Frame;
                        currentFrame.Navigate(typeof(ErrorPageTeacher));

                        var messageDialogue = new MessageDialog($"Welcome, {CurrentUser.Name}");
                        messageDialogue.Commands.Add(new UICommand("Close"));
                        await messageDialogue.ShowAsync();
                }



                
            }
            else
            {
                var messageDialogue = new MessageDialog("Nah fam, no accses for u");
                messageDialogue.Commands.Add(new UICommand("Close"));
                await messageDialogue.ShowAsync();
            }

        }

        public bool AdminCheck(Employee currentUser)
        {
            if (currentUser.IsAdmin)
            {
                return true;
            }
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
                        Singleton.Instance.CurrentUser = e;
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
