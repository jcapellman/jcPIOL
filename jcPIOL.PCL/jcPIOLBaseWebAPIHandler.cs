using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace jcPIOL.PCL {
    public class jcPIOLBaseWebAPIHandler {
        private readonly string _baseWebAPIURL;
        private readonly HttpClient _httpClient;

        public jcPIOLBaseWebAPIHandler(string baseWebAPIURL, double timeOutSeconds = 60) {
            _baseWebAPIURL = baseWebAPIURL;

            var handler = new HttpClientHandler();

            _httpClient = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(timeOutSeconds) };
        }

        private string cleanURL(string url) {
            return url.Replace("=&", "=null&");
        }

        private Uri GetAddress(string urlArguments) {
            return new Uri(string.Format(_baseWebAPIURL + "{0}", cleanURL(urlArguments)));
        }

        private static StringContent GetCcontent<T>(T obj) {
            return new System.Net.Http.StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        private HttpClient CurrentClient => _httpClient;

        public async Task<TK> Send<T, TK>(string urlArguments, T obj) {
            var response = await CurrentClient.PostAsync(GetAddress(urlArguments), GetCcontent(obj));

            var data = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TK>(data);
        }

        public async Task<T> Get<T>(string urlArguments) {
            var responseString = await CurrentClient.GetStringAsync(GetAddress(urlArguments));
           
            return JsonConvert.DeserializeObject<T>(responseString);
        }

        public async Task<bool> Delete(string urlArguments) {
            var result = await CurrentClient.DeleteAsync(GetAddress(urlArguments));

            if (!result.IsSuccessStatusCode) {
                throw new Exception(result.Content.ToString());
            }

            return true;
        }
    }
}