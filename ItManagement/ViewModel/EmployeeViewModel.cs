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



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged
           ([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new
                PropertyChangedEventArgs(propertyName));
        }
    }
}
