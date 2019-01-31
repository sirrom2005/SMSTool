using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SMSTool
{
    public class WebClient
    {
        public static async Task<int> PostDataAsync(string json)
        {
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
            {
               { "json", json }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(Config.API_URL, content);
            var tmp = await response.Content.ReadAsStringAsync();
            return int.Parse(tmp);
        }
    }
}
