﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItManagement.Persistencies
{
    class SmartphonePersistency
    {
        const string ServerUrl = "http://localhost:52667";
        const string EmployeeUri = "SmartPhones";
        const string ApiPrefix = "api";

        private WebAPIAsync<SmartPhone> _webApi;

        public SmartphonePersistency()
        {
            _webApi = new WebAPIAsync<SmartPhone>(ServerUrl, ApiPrefix, EmployeeUri);
        }

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
    }
}
