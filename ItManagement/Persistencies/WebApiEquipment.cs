using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ItManagement.Persistencies
{
    class WebApiEquipment
    {

        private const string ServerUrl = "http://localhost:52667";

        public static List<Equipment> GetEquipment(string url)
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var status = response.Content.ReadAsStringAsync().Result;
                        var equipmentlist = JsonConvert.DeserializeObject<List<Equipment>>(status);
                        return equipmentlist;
                    }

                    return new List<Equipment>();
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                    return null;
                }
            }
        }


        public static async Task<string> PostEquipment(string url, Equipment objectToPost)
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var serializedString = JsonConvert.SerializeObject(objectToPost);
                    StringContent content = new StringContent(serializedString, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage = await client.PostAsync(url, content);

                    if (responseMessage.IsSuccessStatusCode)
                        return await responseMessage.Content.ReadAsStringAsync();

                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }

        }


        public static async Task DeleteEquipment(string url)
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage responseMessage = await client.DeleteAsync(url);
                    if (responseMessage.IsSuccessStatusCode)
                        await responseMessage.Content.ReadAsStringAsync();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                }
            }
        }

        public async static Task PutEquipment(string url, Equipment objectToPut)
        {
            HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var serializedString = JsonConvert.SerializeObject(objectToPut);
                    StringContent content = new StringContent(serializedString, Encoding.UTF8, "application/json");
                    HttpResponseMessage responseMessage = await client.PutAsync(url, content);
                    if (responseMessage.IsSuccessStatusCode) await responseMessage.Content.ReadAsStringAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
        }


    }
}

