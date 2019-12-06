using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using ItManagement.Commands;
using ItManagement.View;
using Windows.UI.Xaml;

namespace ItManagement.ViewModel
{
    class AdminMainpage
    {
        private Employee _adminUser;

        public Employee AdminUser
        { 
            get { return AdminUser; }
            set { _adminUser = value; }
        }

        //public AdminMainpage()
        //{
        //    RelayCommand GoTo = new RelayCommand(NextPageButtonMethod);
        //}

       
        //if (AdminMainpage(ErrorPageAdmin))
        //{
        //   Frame currentFrame = Window.Current.Content as Frame;
        //   currentFrame.Navigate(typeof(ErrorPageAdmin));
        //}
        //else if()
        //{
        //   Frame currentFrame = Window.Current.Content as Frame;
        //   currentFrame.Navigate(typeof(EquipmentPage));
        //}
        

    }
}
