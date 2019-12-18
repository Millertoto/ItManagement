using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.Persistencies
{
    public class EmployeePersistency
    {
        // Skrevet af David

        #region Instance Field
        const string ServerUrl = "http://localhost:52667";
        const string EmployeeUri = "Employees";
        const string ApiPrefix = "api";

        private WebAPIAsync<Employee> _webApi;
        #endregion

        #region Constructor
        public EmployeePersistency()
        {
            _webApi = new WebAPIAsync<Employee>(ServerUrl, ApiPrefix, EmployeeUri);
        }
        #endregion

        #region Tasks
        public Task<List<Employee>> GetEmployees()
        {
            return _webApi.Load();
        }

        public async Task<Employee> GetEmployee(int key)
        {
            return await _webApi.Read(key);
        }

        public async Task DeleteEmployee (int key)
        {
            await _webApi.Delete(key);
        }

        public async Task UpdateEmployee(int key, Employee obj)
        {
            await _webApi.Update(key, obj);
        }

        public async Task CreateEmployee(Employee obj)
        {
            await _webApi.Create(obj);
        }
        #endregion
    }
}
