using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.Persistencies
{
    class EquipmentPersistency
    {
        const string _serverURL = "http://localhost:52667";
        const string _equipmentURI = "Equipments";
        const string _apiPrefix = "api";

        private WebAPIAsync<Equipment> _webApi;

        public EquipmentPersistency()
        {
            _webApi = new WebAPIAsync<Equipment>(_serverURL, _apiPrefix, _equipmentURI);
        }

        public Task<List<Equipment>> GetEquipment()
        {
            return _webApi.Load();
        }

        public async Task<Equipment> GetEquipment(int key)
        {
            return await _webApi.Read(key);
        }

        public async Task DeleteEquipment(int key)
        {
            await _webApi.Delete(key);
        }

        public async Task UpdateEquipment(int key, Equipment obj)
        {
            await _webApi.Update(key, obj);
        }

        public async Task CreateEquipment(Equipment obj)
        {
            await _webApi.Create(obj);
        }
    }
}
