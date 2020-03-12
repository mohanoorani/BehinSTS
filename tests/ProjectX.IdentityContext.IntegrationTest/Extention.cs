using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjectX.IdentityContext.IntegrationTest
{
    public static class Extention
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient client, string url, object body)
        {
            return client.PostAsync(url,
                new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
        }

        public static Task<HttpResponseMessage> PutAsJsonAsync(this HttpClient client, string url, object body)
        {
            return client.PutAsync(url,
                new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));
        }

        public static async Task<T> ReadAsAsync<T>(this HttpContent content)
        {
            var contentString = await content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(contentString);
        }
    }
}