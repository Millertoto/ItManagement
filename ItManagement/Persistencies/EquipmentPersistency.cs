using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.Persistencies
{
    class EquipmentPersistency
    {
        #region Instance Field
        const string ServerUrl = "http://localhost:52667";
        const string EmployeeUri = "Equipments";
        const string ApiPrefix = "api";

        private WebAPIAsync<Equipment> _webApi;
        #endregion

        #region Constructor

        public EquipmentPersistency()
        {
            _webApi = new WebAPIAsync<Equipment>(ServerUrl, ApiPrefix, EmployeeUri);
        }

        #endregion

        #region Tasks
        public Task<List<Equipment>> GetEquipments()

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
        #endregion

    }
}
