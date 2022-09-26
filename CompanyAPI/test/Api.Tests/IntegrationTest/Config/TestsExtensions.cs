using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Api.Tests.IntegrationTest.Config
{
    public static class TestsExtensions
    {
        public static void AssignToken(this HttpClient client, string response)
        {
            client.AssignJsonMediaType();
            var token = ExtractAccessToken(response);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static void AssignJsonMediaType(this HttpClient client)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static string ExtractAccessToken(string response)
        {
            dynamic jsonValue = JObject.Parse(response);
            return Convert.ToString(jsonValue.data.accessToken);
        }
    }
}
