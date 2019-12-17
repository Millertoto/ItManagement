﻿
using System;
using System.Collections.Generic;
using System.Linq;
using ItManagement;
using ItManagement.ViewModel;
using ItManagement.PersSingleton;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject3
{
    [TestClass]
    public class UnitTest1
    {
        private LoginViewModel _testLoginViewModel;
        private EmployeeViewModel _testEmployeeViewModel;
        private EquipmentViewModel _testEquipmentViewModel;
        private ErrorViewModel _testErrorViewModel;
        
        public UnitTest1()
        {
            
            

            Employee EmployeeToBeAdded = new Employee("LmaoLmao22", 1307941588, "heyhvaså", "Olaf Den Tredje");
            _testLoginViewModel = new LoginViewModel();
            _testEmployeeViewModel = new EmployeeViewModel();
            _testEquipmentViewModel = new EquipmentViewModel();
            _testErrorViewModel = new ErrorViewModel();
        }
        [TestMethod]
        public void TestLoginCheck()
        {

            // Arrange

            Employee _testEmployee = new Employee("heyhvaså", 1307949493, "heyhvaså", "huggabugga");
            // Act & Assert

            Assert.IsTrue(_testLoginViewModel.LoginCheck(_testEmployee.Username, _testEmployee.Password, _testLoginViewModel.Employees));
        }

        [TestMethod]
        public void TestWrongLoginCheck()
        {
            // Arrange
            Employee _testEmployee4 = new Employee("donesntExist2", 1313131313, "heyhvaså", "Olaf");

            // Act & Assert

            Assert.IsFalse(_testLoginViewModel.LoginCheck(_testEmployee4.Username, _testEmployee4.Password,
                _testLoginViewModel.Employees));
            
        }

        [TestMethod]
        public void TestAdminCheck()
        {
            // Arrange
            Employee _testEmployee = new Employee("heyhvaså", 1307949493, "heyhvaså", "huggabugga");
            _testEmployee.IsAdmin = true;
            // Act & Assert

            Assert.IsTrue(_testLoginViewModel.AdminCheck(_testEmployee));

        }

        [TestMethod]
        public void TestTeacherCheck()
        {

            //  Arrange
            Employee _testEmployee2 = new Employee("BBBBlmao", 1307941500, "heyssa22", "Martin Holm");
            _testEmployee2.IsAdmin = false;
            // Act & Assert



            Assert.IsFalse(_testLoginViewModel.AdminCheck(_testEmployee2));

        }

        [TestMethod]
        public void TestAddEmployee()
        {
            //Arrange
            Employee _testEmployee3 = new Employee("donesntExist", 1313131313, "heyhvaså", "Olaf");
            _testEmployee3.IsAdmin = true;
            // Act & Assert
            Singleton.Instance.EP.CreateEmployee(_testEmployee3);

            Assert.IsTrue(_testEmployeeViewModel.EmployeeExists(_testEmployee3.Cpr));


        }

        [TestMethod]
        public void TestFailureAddEmployeeWrongPassword()
        {

        }

        [TestMethod]
        public void TestEditEmployee()
        {
            //Arrange
            Employee _testEmployee3 = new Employee("donesntExist", 1313131313, "heyhvaså", "Olaf");
            _testEmployeeViewModel.IsAdmin = "False";
            _testEmployeeViewModel.SetAdmin(_testEmployeeViewModel.IsAdmin, _testEmployee3);
            _testEmployee3.Name = "Henrik";
            // Act & Assert
            

            Assert.IsNull(Singleton.Instance.EP.UpdateEmployee(_testEmployee3.Cpr, _testEmployee3).Exception);

        }

        
        [TestMethod]
        
        public void TestDeleteEmployee()
        {
            //Arrange
            Employee _testEmployee3 = new Employee("donesntExist", 1313131313, "heyhvaså", "Olaf");
            // Act & Assert
            

            Assert.IsNull(Singleton.Instance.EP.DeleteEmployee(_testEmployee3.Cpr).Exception);


        }

        [TestMethod]
        public void TestAddEquipment()
        {
            //Arrange

            Equipment _testEquipment = new Equipment("Computer");
            // Act & Assert

            Assert.IsNull(Singleton.Instance.EQP.CreateEquipment(_testEquipment).Exception);


        }

        [TestMethod]
        public void TestEditEquipment()
        {
            //Arrange
            List<Equipment> temp = Singleton.Instance.EQP.GetEquipments().Result;
            Equipment _testEquipment = temp.Last();
            _testEquipment.Type = "Tablet";
            // Act & Assert

            Assert.IsNull(Singleton.Instance.EQP.UpdateEquipment(_testEquipment.Uid, _testEquipment).Exception);


        }


        [TestMethod]

        public void TestDeleteEquipment()
        {
            //Arrange
            List<Equipment> temp = Singleton.Instance.EQP.GetEquipments().Result;
            Equipment _testEquipment = temp.Last();
            // Act & Assert

            Assert.IsNull(Singleton.Instance.EQP.DeleteEquipment(_testEquipment.Uid).Exception);

        }

        [TestMethod]
        public void TestAddError()
        {
            //Arrange
            Equipment _testEquipment = new Equipment("Computer");
            Singleton.Instance.EQP.CreateEquipment(_testEquipment);
            List<Equipment> _temp = Singleton.Instance.EQP.GetEquipments().Result;

            _testEquipment = _temp.Last();

            Error tempError = new Error(1307949493, _testEquipment.Uid, "shit aint working", false, 1234567891);


            // Act & Assert

            Assert.IsNull(Singleton.Instance.ERP.CreateError(tempError).Exception);



        }

        [TestMethod]
        public void TestEditError()
        {
            //Arrange
            List<Error> _temp = Singleton.Instance.ERP.GetErrors().Result;
            Error tempError = _temp.Last();
            tempError.ErrorMessage = "Changed";
            // Act & Assert

            Assert.IsNull(Singleton.Instance.ERP.UpdateError(tempError.Uid, tempError).Exception);

        }


        [TestMethod]

        public void TestDeleteError()
        {
            //Arrange
            List<Error> _temp = Singleton.Instance.ERP.GetErrors().Result;
            Error tempError = _temp.Last();
            // Act & Assert

            Assert.IsNull(Singleton.Instance.ERP.DeleteError(tempError.Fid).Exception);

        }

    }
}
