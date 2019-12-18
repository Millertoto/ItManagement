using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.Persistencies
{
    public class SmartphonePersistency
    {
        // Skrevet af David

        #region Instance Field
        const string ServerUrl = "http://localhost:52667";
        const string smartphoneUri = "SmartPhones";
        const string ApiPrefix = "api";

        private WebAPIAsync<SmartPhone> _webApi;
        #endregion

        #region Constructor
        public SmartphonePersistency()
        {
            _webApi = new WebAPIAsync<SmartPhone>(ServerUrl, ApiPrefix, smartphoneUri);
        }

        #endregion

        #region Tasks

        public Task<List<SmartPhone>> GetSmartphones()
        {
            return _webApi.Load();
        }

        public async Task<SmartPhone> GetSmartphone(int key)
        {
            return await _webApi.Read(key);
        }

        public async Task DeleteSmartphone(int key)
        {
            await _webApi.Delete(key);
        }

        public async Task UpdateSmartphone(int key, SmartPhone obj)
        {
            await _webApi.Update(key, obj);
        }

        public async Task CreateSmartphone(SmartPhone obj)
        {
            await _webApi.Create(obj);
        }
        #endregion
    }
}
