using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.Persistencies
{
    public class ErrorPersistency
    {
        const string _serverURL = "http://localhost:52667";
        const string _employeeURI = "Errors";
        const string _apiPrefix = "api";

        private WebAPIAsync<Error> _webApi;

        public ErrorPersistency()
        {
            _webApi = new WebAPIAsync<Error>(_serverURL, _apiPrefix, _employeeURI);
        }

        public Task<List<Error>> GetErrors()
        {
            return _webApi.Load();
        }

        public async Task<Error> GetError(int key)
        {
            return await _webApi.Read(key);
        }

        public async Task DeleteError(int key)
        {
            await _webApi.Delete(key);
        }

        public async Task UpdateError(int key, Error obj)
        {
            await _webApi.Update(key, obj);
        }

        public async Task CreateError(Error obj)
        {
            await _webApi.Create(obj);
        }
    }
}
