using System;
using ItManagement;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ItManagement.Commands;
using ItManagement.Persistencies;

namespace ItManagement.ViewModel
{
    public class TeacherViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _password;
        private string _username;
        private int _cpr;
        private bool _isAdmin;
        private RelayCommand _addEmployeeButton;
        private RelayCommand _getEmployeeList;
        private List<Employee> _employees;


        public TeacherViewModel()
        {
            _addEmployeeButton = new RelayCommand(AddEmployeeMethod);
            _getEmployeeList = new RelayCommand(GetEmployeeList);
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

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { _isAdmin = value; }
        }

        public RelayCommand AddEmployeeButton
        {
            get { return _addEmployeeButton; }
            set { _addEmployeeButton = value; }

        }

        public List<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; }

        }

        public async void GetEmployeeList()
        {
            Employees = WebApiEmployee.GetEmployees("api/Employees/");
        }

        public RelayCommand GetEmployeeCommand
        {
            get { return _getEmployeeList; }
            set { _getEmployeeList = value; }

        }
        public async void AddEmployeeMethod()
        {
            
            GetEmployeeList();
            if (UsernameCheck(Username, Employees)
                && CprCheck(CPR, Employees)
                && PasswordCheck(Password)
                && NameCheck(Name))
            {
                Employee Emp = new Employee(Username, CPR, Password, Name, true);
                await WebApiEmployee.PostEmployee("api/Employees/", Emp);
            }

        }

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

        public bool PasswordCheck(string password)
        {

            if (password.Length >= 8 && password.Length <= 16)
            {
                return true;
            }
            return false;


        }

        public bool NameCheck(string name)
        {
            if (name.Length <= 50)
            {
                return true;
            }

            return false;
        }




        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged
            ([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new
                PropertyChangedEventArgs(propertyName));
        }
    }
}
