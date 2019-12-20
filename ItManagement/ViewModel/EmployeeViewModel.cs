using ItManagement.Commands;
using ItManagement.PersSingleton;
using ItManagement.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ItManagement.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        // Skrevet af Martin

        #region Instance Field
        private string _name;
        private string _password;
        private string _username;
        private string _isAdmin;

        private Employee _selectedEmployee;

        private int _cpr;

        private List<Employee> _employees;

        private ObservableCollection<Employee> _obsEmps;
        private ObservableCollection<string> _adminObs;

        private RelayCommand _addEmployeeButton;
        private RelayCommand _deleteEmp;
        private RelayCommand _editButton;
        private RelayCommand _goBack;

        #endregion

        #region Constructor

        public EmployeeViewModel()
        {
            _addEmployeeButton = new RelayCommand(AddEmployeeMethod);
            _deleteEmp = new RelayCommand(DeleteEmpMethod);
            _editButton = new RelayCommand(EditMethod);
            _goBack = new RelayCommand(GoBackMethod);

            _obsEmps = new ObservableCollection<Employee>();
            _adminObs = new ObservableCollection<string>() {"True", "False"};

            Employees = Singleton.Instance.EP.GetEmployees().Result;
            _selectedEmployee = new Employee();

            ConvertToObs();
        }
        #endregion

        #region Properties

        #region EmployeeProps

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        public int CPR
        {
            get { return _cpr; }
            set
            {
                _cpr = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region RelayCommands

        public RelayCommand DeleteButton
        {
            get { return _deleteEmp; }
            set { _deleteEmp = value; }
        }

        public RelayCommand EditButton
        {
            get { return _editButton; }
            set { _editButton = value; }
        }
        public RelayCommand AddEmployeeButton
        {
            get { return _addEmployeeButton; }
            set { _addEmployeeButton = value; }

        }

        public RelayCommand GoBack
        {
            get { return _goBack; }
            set { _goBack = value; }
        }

        #endregion

        #region Lists and Obs

        public List<Employee> Employees
        {
            get { return _employees; }
            set
            {
                _employees = value;
                OnPropertyChanged();
            }

        }
        public ObservableCollection<string> AdminObs
        {
            get { return _adminObs; }
            set { _adminObs = value; }

        }

        public ObservableCollection<Employee> ObsEmployees
        {
            get { return _obsEmps; }
            set
            {
                _obsEmps = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #endregion

        #region Methods

        #region AddEmployee

        // Tilføjer en ansat med given information
        public async void AddEmployeeMethod()
        {

            GetEmployeeList();
            if (UsernameCheck(SelectedEmployee.Username, Employees)
                && CprCheck(SelectedEmployee.Cpr, Employees)
                && PasswordCheck(SelectedEmployee.Password)
                && NameCheck(SelectedEmployee.Name))
            {
                Employee Emp = new Employee(SelectedEmployee.Username, SelectedEmployee.Cpr, SelectedEmployee.Password, SelectedEmployee.Name);
                SetAdmin(IsAdmin, Emp);
                await Singleton.Instance.EP.CreateEmployee(Emp);

                var messageDialogue = new MessageDialog($"The Account, {Username}, has been created");
                messageDialogue.Commands.Add(new UICommand("Close"));
                await messageDialogue.ShowAsync();

            }
            else
            {
                var messageDialogue = new MessageDialog($"Failure, Given values does not match requisites. Username and Password must be between 8-16");
                messageDialogue.Commands.Add(new UICommand("Close"));
                await messageDialogue.ShowAsync();

            }
            GetEmployeeList();

        }
        #endregion

        #region DeleteEmployee

        // Fjerner en ansat med given information
        public async void DeleteEmpMethod()
        {
            
                if (SelectedEmployee != null || SelectedEmployee.Cpr > 0)
                {
                    await Singleton.Instance.EP.DeleteEmployee(SelectedEmployee.Cpr);
                }
                else
                {
                    var messageDialogue = new MessageDialog($"You must select the employee you wish to remove");
                    messageDialogue.Commands.Add(new UICommand("Close"));
                    await messageDialogue.ShowAsync();

                    
                }
                GetEmployeeList();
            
        }

        #endregion

        #region Edit Employee

        // Ændrer en ansat med given information
        public async void EditMethod()
        {
            if (SelectedEmployee.Cpr != null || SelectedEmployee.Cpr > 0)
            {
                SelectedEmployee.Name = Name;
                SelectedEmployee.Password = Password;
                SelectedEmployee.Username = Username;
                SetAdmin(IsAdmin, SelectedEmployee);
                await Singleton.Instance.EP.UpdateEmployee(SelectedEmployee.Cpr, SelectedEmployee);
            }
            else
            {
                var messageDialogue = new MessageDialog($"You must select the employee you wish to edit");
                messageDialogue.Commands.Add(new UICommand("Close"));
                await messageDialogue.ShowAsync();

            }
            
            GetEmployeeList();

        }

        #endregion

        #region Checks
        // Checker om den ansatte eksisterer
        public bool EmployeeExists(int cpr)
        {
            bool c = false;
            Employee temp = Singleton.Instance.EP.GetEmployee(cpr).Result;
            if (temp.Cpr == cpr)
            {
                c = true;
            }

            return c;
        }

        // Checker om navnet og cpr nummeret eksisterer og tilhører den samme person
        public bool CheckIfNameExists(int cpr, string name)
        {
            bool c = false;
            Employee temp = Singleton.Instance.EP.GetEmployee(cpr).Result;
            if ( temp.Cpr == cpr && temp.Name == name)
            {
                c = true;
            }

            return c;

        }

        // Checker om den ansatte eksisterer
        public bool CheckIfDeleted(int cpr)
        {
            bool c = false;
            List<Employee> templist = Singleton.Instance.EP.GetEmployees().Result;
            foreach (Employee e in templist)
            {
                if (cpr == e.Cpr)
                {
                    c = true;
                    break;
                }
            }

            return c;
        }

        // Sætter hvorvidt den ansatte er admin
        public void SetAdmin(string admincheck, Employee e)
        {
            if (admincheck == "true" || admincheck == "True")
            {
                e.IsAdmin = true;
            }
            else
            {
                e.IsAdmin = false;
            }
        }

        // Checker om brugernavnet eksisterer og om det er indefor de regler vi har sat
        public bool UsernameCheck(string username, List<Employee> list)
        {
            bool c = false;
            if (username.Length >= 8 && username.Length <= 16)
            {
                c = true;
                foreach (Employee e in list)
                {
                    if (username == e.Username)
                    {
                        c = false;
                        break;
                    }
                }
            }

            return c;

        }


        // Checker om CPR nummeret eksisterer og om det er indefor de regler vi har sat
        public bool CprCheck(int cpr, List<Employee> list)
        {
            bool c = false;
            if (cpr.ToString().Length == 10)
            {
                c = true;
                foreach (Employee e in list)
                {
                    if (cpr == e.Cpr)
                    {
                        c = false;
                        break;
                    }
                }
            }

            return c;
        }

        // Checker om kodeordet er indefor de regler vi har sat
        public bool PasswordCheck(string password)
        {

            if (password.Length >= 8 && password.Length <= 16)
            {
                return true;
            }
            return false;


        }

        // Checker om navnet er indefor de regler vi har sat
        public bool NameCheck(string name)
        {
            if (name.Length <= 50)
            {
                return true;
            }

            return false;
        }
        #endregion

        #region List/Obs Methods
        public void GetEmployeeList()
        {
            Employees = Singleton.Instance.EP.GetEmployees().Result;
            ObsEmployees.Clear();
            foreach (Employee e in Employees)
            {
                ObsEmployees.Add(e);
            }
        }

        public void ConvertToObs()
        {
            foreach (Employee e in Employees)
            {
                    ObsEmployees.Add(e);
            }
        }
        #endregion

        #region GoBack
        public void GoBackMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(AdminMainpage));
        }
        #endregion 

        #endregion

        #region INotify

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
