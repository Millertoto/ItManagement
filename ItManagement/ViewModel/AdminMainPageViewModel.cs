﻿using System;
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
        #region Instance Field
        /*private Employee _adminUser;*/
        private RelayCommand _logoutButton;
        private RelayCommand _GoToCreateEmployee;
        private RelayCommand _GoToCreateEquipment;
        private RelayCommand _GoToErrorPage;
        private RelayCommand _GoToOverviewPage;
        #endregion

        #region Constructor
        public AdminMainPageViewModel()
        {
           _logoutButton = new RelayCommand(LogoutMethod);
           _GoToCreateEmployee = new RelayCommand(EmployeeMethod);
           _GoToCreateEquipment = new RelayCommand(EquipmentMethod);
           _GoToErrorPage = new RelayCommand(ErrorMethod);
           _GoToOverviewPage = new RelayCommand(OverviewMethod);
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
            get { return _GoToCreateEmployee; }
            set { _GoToCreateEmployee = value; }

        }

        public RelayCommand EquipmentButton
        {
            get { return _GoToCreateEquipment; }
            set { _GoToCreateEquipment = value; }
        }

        public RelayCommand ErrorButton
        {
            get { return _GoToErrorPage; }
            set { _GoToErrorPage = value; }
        }

        public RelayCommand OverviewButton
        {
            get { return _GoToOverviewPage; }
            set { _GoToOverviewPage = value; }
        }
        #endregion

        #region Method

        public void EquipmentMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(EquipmentPageAdmin));
        }

        public void OverviewMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(OverviewAdmin));
        }

        public void ErrorMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(ErrorPageAdmin));
        }

        public void LogoutMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(MainPage));
        }

        public void EmployeeMethod()
        {
            Frame currentFrame = Window.Current.Content as Frame;
            currentFrame.Navigate(typeof(EmployeePageAdmin));
        }
        #endregion



    }
}
