using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Common
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpResponseMessage> SendRequest(this HttpClient httpClient, HttpMethod httpMethod,
            string relativePath, string content = null)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = new Uri(new Uri("http://localhost/"), relativePath),
                Method = httpMethod
            };
            if (!string.IsNullOrEmpty(content))
            {
                request.Content =
                    new StringContent(content, Encoding.UTF8, "application/json");
            }

            return await httpClient.SendAsync(request);
        }
    }
}
