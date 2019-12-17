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
    public class AdminMainPageViewModel
    {

        // Skrevet af Martin

        #region Instance Field

        private RelayCommand _logoutButton;
        private RelayCommand _goToCreateEmployee;
        private RelayCommand _goToCreateEquipment;
        private RelayCommand _goToErrorPage;
        private RelayCommand _goToOverviewPage;
        #endregion

        #region Constructor
        public AdminMainPageViewModel()
        {
           _logoutButton = new RelayCommand(LogoutMethod);
           _goToCreateEmployee = new RelayCommand(EmployeeMethod);
           _goToCreateEquipment = new RelayCommand(EquipmentMethod);
           _goToErrorPage = new RelayCommand(ErrorMethod);
           _goToOverviewPage = new RelayCommand(OverviewMethod);
        }

        #endregion

        #region Properties

        public RelayCommand LogoutButton
        {
            get { return _logoutButton; }
            set { _logoutButton = value; }
        }

        public RelayCommand EmployeeButton
        {
            get { return _goToCreateEmployee; }
            set { _goToCreateEmployee = value; }

        }

        public RelayCommand EquipmentButton
        {
            get { return _goToCreateEquipment; }
            set { _goToCreateEquipment = value; }
        }

        public RelayCommand ErrorButton
        {
            get { return _goToErrorPage; }
            set { _goToErrorPage = value; }
        }

        public RelayCommand OverviewButton
        {
            get { return _goToOverviewPage; }
            set { _goToOverviewPage = value; }
        }
        #endregion

        #region Method
        // Sender brugeren videre til udstyrs siden.
        public void EquipmentMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(EquipmentPageAdmin));
        }

        // Sender brugeren videre til oversigts siden.
        public void OverviewMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(OverviewAdmin));
        }

        // Sender brugeren videre til fejl siden.
        public void ErrorMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(ErrorPageAdmin));
        }

        // Sender brugeren videre tilbage til login siden.
        public void LogoutMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(MainPage));
        }

        // Sender brugeren videre til ansatte siden.
        public void EmployeeMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(EmployeePageAdmin));
        }
        #endregion



    }
}
