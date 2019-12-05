using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.Persistencies
{
    class TabletPersistency
    {
        const string ServerUrl = "http://localhost:52667";
        const string EmployeeUri = "Tablets";
        const string ApiPrefix = "api";

        private WebAPIAsync<Tablet> _webApi;

        public TabletPersistency()
        {
            _webApi = new WebAPIAsync<Tablet>(ServerUrl, ApiPrefix, EmployeeUri);
        }

        public Task<List<Tablet>> GetTablets()
        {
            return _webApi.Load();
        }

        public async Task<Tablet> GetTablet(int key)
        {
            return await _webApi.Read(key);
        }

        public async Task DeleteTablet(int key)
        {
            await _webApi.Delete(key);
        }

        public async Task UpdateTablet(int key, Tablet obj)
        {
            await _webApi.Update(key, obj);
        }

        public async Task CreateTablet(Tablet obj)
        {
            await _webApi.Create(obj);
        }
    }
}
