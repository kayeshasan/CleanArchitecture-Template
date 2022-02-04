using Core.Domain.Shared.Wrappers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Helper
{
    public static class HttpHelper
    {
        //private static readonly string apiBasicUri = ConfigurationManager.AppSettings["apiBasicUri"];

        public static async Task Post<T>(string apiBasicUri, string url, T contentValue)
        {
            //using (var client = new HttpClient())
            //{
            //    //client.BaseAddress = new Uri(apiBasicUri);
            //    //var content = new StringContent(JsonConvert.SerializeObject(contentValue), Encoding.UTF8, "application/json");
            //    //var result = await client.PostAsync(url, content);
            //    //result.EnsureSuccessStatusCode();

            //}
            //var client = new RestClient(apiBasicUri);
            //var request = new RestRequest(url, Method.POST, DataFormat.Json);
            //var requestBody = new RequestBody("application/json", "command", contentValue );
            //var result = (await client.ExecuteAsync<Response<T>>(request)).Data;

        }

        public static async Task<T> Get<T>(string apiBasicUri, string url)
        {
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(apiBasicUri);
            //    var result = await client.GetAsync(url);
            //    result.EnsureSuccessStatusCode();
            //    string resultContentString = await result.Content.ReadAsStringAsync();
            //    T resultContent = JsonConvert.DeserializeObject<T>(resultContentString);
            //    return resultContent;
            //}
            var client = new RestClient(apiBasicUri);
            var request = new RestRequest(url, Method.GET, DataFormat.Json);
            var result = (await client.ExecuteAsync<Response<T>>(request)).Data;
            return result.Data;
        }
    }
}
