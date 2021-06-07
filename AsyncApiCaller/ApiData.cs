using AsyncApiCaller.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AsyncApiCaller
{
    public class ApiData
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public int GetRandomNumberWithoutAsyncAwait()
        {
            var responseTask = _httpClient.GetAsync("https://seriouslyfundata.azurewebsites.net/api/generatearandomnumber");
            responseTask.Wait();
            responseTask.Result.EnsureSuccessStatusCode();

            var responseStringTask = responseTask.Result.Content.ReadAsStringAsync();
            responseStringTask.Wait();

            return Convert.ToInt32(responseStringTask.Result);
        }

        public async Task<int> GetRandomNumber()
        {
            var response = await _httpClient.GetAsync("https://seriouslyfundata.azurewebsites.net/api/generatearandomnumber");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            return Convert.ToInt32(responseString);
        }

        public async Task<string> GetChuckNorrisFact()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://seriouslyfundata.azurewebsites.net/api/chucknorrisfact");
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            var fact = Newtonsoft.Json.JsonConvert.DeserializeObject<ChuckNorrisFact>(responseContent);

            return fact.Joke;
        }

        public async Task<List<Seleucid>> GetTheSeleucids()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://seriouslyfundata.azurewebsites.net/api/seleucids");
            response.EnsureSuccessStatusCode();
            string responseContent = await response.Content.ReadAsStringAsync();
            var seleucidResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SeleucidResponse>(responseContent);

            return seleucidResponse.Seleucids;
        }
    }
}
