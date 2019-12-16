
using System;
using ItManagement;
using ItManagement.PersSingleton;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItManagement.ViewModel;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        private LoginViewModel _loginViewModelTest;
        private Employee testEmployee;
        private Employee testEmployee2;

        public UnitTest1()
        {
            testEmployee = new Employee("heyhvaså", 1307949493, "heyhvaså", "huggabugga");
            testEmployee2 = new Employee("BBBBlmao", 1307941500, "heyssa22", "Martin Holm");
        }


        [TestMethod]
        public void TestLoginCheck()
        {

              //Act & Assert
              Assert.ThrowsException<ArgumentException>(() =>
              {
                  _loginViewModelTest.LoginCheck("heyhvaså", "heyhvaså", _loginViewModelTest.Employees);
              });


        }

        [TestMethod]
        public void TestAdminCheck()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                _loginViewModelTest.AdminCheck(testEmployee);

            });
        }

        [TestMethod]    
        public void TestAddEmployee()
        {

        }


        [TestMethod]
        public void TestAddEquipment()
        {

        }
        [TestMethod]
        public void TestAddError()
        {

        }

        [TestMethod]
        public void TestEditEquipment()
        {

        }

        [TestMethod]
        public void TestEditEmployee()
        {

        }

        [TestMethod]
        public void TestEditError()
        {

        }

        [TestMethod]
        public void TestFix()
        {

        }

        [TestMethod]
        public void TestSearch()
        {

        }

        [TestMethod]
        public void TestRemoveError()
        {

        }

        [TestMethod]
        public void TestRemoveEquipment()
        {

        }

        [TestMethod]
        public void TestRemoveEmployee()
        {

        }
    }
}
