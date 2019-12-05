using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.Persistencies
{
    class EmployeePersistency
    {
        const string _serverURL = "http://localhost:52667";
        const string _employeeURI = "Employees";
        const string _apiPrefix = "api";

        private WebAPIAsync<Employee> _webApi;

        public EmployeePersistency()
        {
            _webApi = new WebAPIAsync<Employee>(_serverURL, _apiPrefix, _employeeURI);
        }

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
    }
}
