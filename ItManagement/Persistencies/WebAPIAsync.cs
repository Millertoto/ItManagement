﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ItManagement.Persistencies
{
    // Skrevet af David

    public class WebAPIAsync<T> : IwebAPIAsync<T> where T : class

    {
        #region Instance Field

        private string _serverURL;
        private string _apiPrefix;
        private string _apiID;
        private HttpClientHandler _httpClientHandler;
        private HttpClient _httpClient;

        #endregion

        #region Constructor

        public WebAPIAsync(string serverUrl, string apiPrefix, string apiId)
        {
            _serverURL = serverUrl;
            _apiPrefix = apiPrefix;
            _apiID = apiId;
            _httpClientHandler = new HttpClientHandler();
            _httpClientHandler.UseDefaultCredentials = true;
            _httpClient = new HttpClient(_httpClientHandler);
            _httpClient.BaseAddress = new Uri(_serverURL);
        }

        #endregion

        #region Tasks

        // Gør det muligt for en metode at fjerne et objekt i databasen
        public async Task Delete(int key)
        {
            HttpClient client = new HttpClient();
            string url = _serverURL + "/" + _apiPrefix + "/" + _apiID + "/" + key;
            HttpResponseMessage response = client.DeleteAsync(url).Result;
            response.EnsureSuccessStatusCode();
        }


        // Gør det muligt for en metode at få en liste over all objekter af en slags fra databasen
        public async Task<List<T>> Load()
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = true };
            string url = _serverURL + "/" + _apiPrefix + "/" + _apiID;
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var status = response.Content.ReadAsStringAsync().Result;
                        var itemlist = JsonConvert.DeserializeObject<List<T>>(status);
                        return itemlist;
                    }

                    return new List<T>();
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        // Gør det muligt for en metode at få et enkelt objekt af en slags fra databasen
        public async Task<T> Read(int key)
        {
            HttpClient client = new HttpClient();
            string url = _serverURL + "/" + _apiPrefix + "/" + _apiID + "/" + key;
            string response = await client.GetStringAsync(url);
            var readobj = JsonConvert.DeserializeObject<T>(response);
            return readobj;
        }


        // Gør det muligt for en metode at ændre noget ved et objekt i databasen
        public async Task Update(int key, T obj)
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string url = _serverURL + "/" + _apiPrefix + "/" + _apiID + "/" + key;
                try
                {
                    var serializedString = JsonConvert.SerializeObject(obj);
                    StringContent content = new StringContent(serializedString, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage = await client.PutAsync(url, content);
                    if (responseMessage.IsSuccessStatusCode)
                        await responseMessage.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }


        // Gør det muligt for en metode at oprette et nyt objekt af en slags til databasen
        public async Task Create(T obj)
        {
            string url = _serverURL + "/" + _apiPrefix + "/" + _apiID;
            string postBody = JsonConvert.SerializeObject(obj);
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsync(url, new StringContent(postBody, Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
        }
    }
    #endregion
}
