using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.Persistencies
{
    class AccesoryPersistency
    {
        const string _serverURL = "http://localhost:52667";
        const string _accesoryURI = "Accesories";
        const string _apiPrefix = "api";

        private WebAPIAsync<Accesory> _webApi;

        public AccesoryPersistency()
        {
            _webApi = new WebAPIAsync<Accesory>(_serverURL, _apiPrefix, _accesoryURI);
        }

        public Task<List<Accesory>> GetAccesories()
        {
            return _webApi.Load();
        }

        public async Task<Accesory> GetAccesory(int key)
        {
            return await _webApi.Read(key);
        }

        public async Task DeleteAccesory(int key)
        {
            await _webApi.Delete(key);
        }

        public async Task UpdateAccesory(int key, Accesory obj)
        {
            await _webApi.Update(key, obj);
        }

        public async Task CreateAccesory(Accesory obj)
        {
            await _webApi.Create(obj);
        }
    }
}
