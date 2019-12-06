using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.Persistencies
{
    class SmartboardPersistency
    {
        #region Instance Field
        const string ServerUrl = "http://localhost:52667";
        const string EmployeeUri = "SmartBoards";
        const string ApiPrefix = "api";

        private WebAPIAsync<SmartBoard> _webApi;
        #endregion

        #region Constructor
        public SmartboardPersistency()
        {
            _webApi = new WebAPIAsync<SmartBoard>(ServerUrl, ApiPrefix, EmployeeUri);
        }

        #endregion

        #region Tasks
        public Task<List<SmartBoard>> GetSmartboards()
        {
            return _webApi.Load();
        }

        public async Task<SmartBoard> GetSmartboard(int key)
        {
            return await _webApi.Read(key);
        }

        public async Task DeleteSmartboard(int key)
        {
            await _webApi.Delete(key);
        }

        public async Task UpdateSmartboard(int key, SmartBoard obj)
        {
            await _webApi.Update(key, obj);
        }

        public async Task CreateSmartboard(SmartBoard obj)
        {
            await _webApi.Create(obj);
        }
        #endregion
    }
}
