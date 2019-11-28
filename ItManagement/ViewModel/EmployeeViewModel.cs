using ItManagement.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ItManagement.ViewModel
{
    class EmployeeViewModel : INotifyPropertyChanged
    {
        private int _cpr;
        private string _userName;
        private string _password;
        private string _name;
        private bool _isAdmin;
        private List<Employee> _listOfEmployees;
        private RelayCommand _addEmployeeButton;
        private RelayCommand _getEmployee;



        public ICommand AddEmployeeButton
        {
            get
            {
                if (_addEmployeeButton == null)
                {
                    _addEmployeeButton = new RelayCommand(AddEmployee);
                }

                return _addEmployeeButton;
            }


        }

        public ICommand GetEmployeeButton
        {
            get
            {
                if (_getEmployee == null)
                {
                    _getEmployee = new RelayCommand(GetEmployees);
                }

                return _getEmployee;
            }


        }

        public void AddEmployee()
        {
            Employee ep1 = new Employee
            {
                Cpr = _cpr,
                Username = _userName,
                Password = _password,
                Name = _name,
                IsAdmin = false,

            };

            using (var db = new SkoledbContext())
            {
                db.Add(ep1);
                db.SaveChanges();
            }
        }

        public void GetEmployees()
        {
            _listOfEmployees.Clear();

            using (var db = new SkoledbContext())
            {
                foreach (Employee e in db.Employees)
                {
                    _listOfEmployees.Add(e);
                }
            }
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
