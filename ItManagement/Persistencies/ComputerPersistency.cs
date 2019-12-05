using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.Persistencies
{
    class ComputerPersistency
    {
        const string ServerUrl = "http://localhost:52667";
        const string EmployeeUri = "Computers";
        const string ApiPrefix = "api";

        private WebAPIAsync<Computer> _webApi;

        public ComputerPersistency()
        {
            _webApi = new WebAPIAsync<Computer>(ServerUrl, ApiPrefix, EmployeeUri);
        }

        public Task<List<Computer>> GetComputers()
        {
            return _webApi.Load();
        }

        public async Task<Computer> GetComputer(int key)
        {
            return await _webApi.Read(key);
        }

        public async Task DeleteComputer(int key)
        {
            await _webApi.Delete(key);
        }

        public async Task UpdateComputer(int key, Computer obj)
        {
            await _webApi.Update(key, obj);
        }

        public async Task CreateComputer(Computer obj)
        {
            await _webApi.Create(obj);
        }
    }
}
