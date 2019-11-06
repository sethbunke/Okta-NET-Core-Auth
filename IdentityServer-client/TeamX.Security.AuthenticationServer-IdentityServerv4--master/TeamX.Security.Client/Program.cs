using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TeamX.Security.Client
{
    class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {

            var url = "https://apigee-poc-sys-48655.appspot.com";
            //var url = "http://localhost:5000";
            // discover endpoints from the metadata by calling Auth server hosted on 5000 port
            //var discoveryClient = await DiscoveryClient.GetAsync(url);
            
            //if (discoveryClient.IsError)
            //{
            //    Console.WriteLine(discoveryClient.Error);
            //    //return;
            //}

            // request the token from the Auth server
            //var tokenClient = new TokenClient(discoveryClient.TokenEndpoint, "client", "secret");
            var tokenEndpoint = "http://apigee-poc-sys-48655.appspot.com/connect/token";
            var tokenClient = new TokenClient(tokenEndpoint, "client", "secret");

            var response = await tokenClient.RequestClientCredentialsAsync("api1");

            if (response.IsError)
            {
                Console.WriteLine(response.Error);
                return;
            }

            Console.WriteLine(response.Json);
            Console.WriteLine("\n\n");

            // call api
            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);

            var apiResponse = await client.GetAsync("http://localhost:5001/identity");
            if (!apiResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(apiResponse.StatusCode);
            }
            else
            {
                var content = await apiResponse.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
