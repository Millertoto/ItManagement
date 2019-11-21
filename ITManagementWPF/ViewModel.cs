using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ITManagementWPF
{
    class ViewModel
    {
        
        private ICommand _createCommand;

        public ICommand CreateCommand
        {
            get
            {
                if (_createCommand == null)
                {
                    
                    _createCommand = new RelayCommand(
                        param => this.CanSave(),
                        param => this.CreateEmployees()
                    );
                }
                return _createCommand;
            }
        }

        private bool CanSave()
        {
            int i = 1;
            if (i == 1)
            {
                return true;
            }
            return false;
        }

        private void SaveObject()
        {
            // Save command execution logic
        }

        public void CreateEmployees()
        {
            Employee e1 = new Employee { Cpr = 1307941593, Username = "AAA", Password = "heyssa22", Name = "Martin Holm", IsAdmin = true };
            Employee e2 = new Employee { Cpr = 1307941594, Username = "BBB", Password = "heyssa23", Name = "Gudrund Holm", IsAdmin = false };
            Employee e3 = new Employee { Cpr = 1307941595, Username = "CCC", Password = "heyssa24", Name = "William Holm", IsAdmin = false };

            using (var db = new SkoleDBContext())
            {
                db.Employees.Add(e1);
                db.Employees.Add(e2);
                db.Employees.Add(e3);
                db.SaveChanges();
            }
        }
    }
}
