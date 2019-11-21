using System;
using ItManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ItManagement.ViewModel
{
    public class TeacherViewModel : INotifyPropertyChanged
    {
        private Teacher _domainObjectTe;

        public TeacherViewModel()
        {
            _domainObjectTe = new Teacher(88465348, "", "", "", false);
        }

        public int TeCPR
        {
            get { return _domainObjectTe.CPR; }
            set
            {
                _domainObjectTe.CPR = value;
                OnPropertyChanged();
            }
        }

            public string TeUserName
        {
            get { return _domainObjectTe.UserName; }
            set
            {
                _domainObjectTe.UserName = value;
                OnPropertyChanged();
            }
        }

        public string TePassword
        {
            get { return _domainObjectTe.Password; }
            set
            {
                _domainObjectTe.Password = value;
                OnPropertyChanged();
            }
        }

        public string TeName
        {
            get { return _domainObjectTe.Name; }
            set
            {
                _domainObjectTe.Name = value;
                OnPropertyChanged();
            }
        }

        public bool TeIsAdmin
        {
            get { return _domainObjectTe.IsAdmin; }
            
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
