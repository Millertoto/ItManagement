
using System;
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
        private Employee _testEmployee;
        private Employee _testEmployee2;
        private Employee _testEmployee3;
        private LoginViewModel _testLoginViewModel;
        private EmployeeViewModel _testEmployeeViewModel;
        
        public UnitTest1()
        {
            _testEmployee = new Employee("heyhvaså", 1307949493, "heyhvaså", "huggabugga");
            _testEmployee2 = new Employee("BBBBlmao", 1307941500, "heyssa22", "Martin Holm");
            _testEmployee3 = new Employee("donesntExist", 1313131313, "heyhvaså", "Olaf");
            Employee EmployeeToBeAdded = new Employee("LmaoLmao22", 1307941588, "heyhvaså", "Olaf Den Tredje");
            _testLoginViewModel = new LoginViewModel();
            _testEmployeeViewModel = new EmployeeViewModel();

        }
        [TestMethod]
        public void TestLoginCheck()
        {
            // Act & Assert

            Assert.IsTrue(_testLoginViewModel.LoginCheck(_testEmployee.Username, _testEmployee.Password, _testLoginViewModel.Employees));
        }

        [TestMethod]
        public void TestWrongLoginCheck()
        {
            // Act & Assert

            Assert.IsFalse(_testLoginViewModel.LoginCheck(_testEmployee3.Username, _testEmployee3.Password,
                _testLoginViewModel.Employees));
            
        }

        [TestMethod]
        public void TestAdminCheck()
        {
            // Arrange

            _testEmployee.IsAdmin = true;
            // Act & Assert

            Assert.IsTrue(_testLoginViewModel.AdminCheck(_testEmployee));

        }

        [TestMethod]
        public void TestTeacherCheck()
        {

            //  Arrange

            _testEmployee2.IsAdmin = false;
            // Act & Assert



            Assert.IsFalse(_testLoginViewModel.AdminCheck(_testEmployee2));

        }

        [TestMethod]
        public void TestAddEmployee()
        {
            //Arrange
            _testEmployee3.IsAdmin = true;
            // Act & Assert
            Assert.IsTrue(Singleton.Instance.EP.CreateEmployee(_testEmployee3).IsCompletedSuccessfully);
            

        }

        public void TestEditEmployee()
        {
            //Arrange
            _testEmployee3.IsAdmin = true;
            // Act & Assert
            Assert.IsTrue(Singleton.Instance.EP.CreateEmployee(_testEmployee3).IsCompletedSuccessfully);


        }

        public void TestDeleteEmployee()
        {
            //Arrange
            _testEmployee3.IsAdmin = true;
            // Act & Assert
            Assert.IsTrue(Singleton.Instance.EP.CreateEmployee(_testEmployee3).IsCompletedSuccessfully);


        }
    }
}
