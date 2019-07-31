using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MMOS.SDK {

    public class Api {

        public enum ApiVersion {
            V2 = 2
        }

        public struct ApiV2 {
            public ApiV2(Api api) {
                this.Authentication = new MMOS.SDK.V2.Authentication();
                this.Players = new MMOS.SDK.V2.Players(api);
                this.Classifications = new MMOS.SDK.V2.Classifications(api);
            }
            public IAuthentication Authentication { get; }
            public MMOS.SDK.V2.Players Players { get; }
            public MMOS.SDK.V2.Classifications Classifications { get; }
        }

        public struct ApiKey {
            public ApiKey(string key, string secret) {
                this.Key = key;
                this.Secret = secret;
            }
            public string Key;
            public string Secret;
        }

        public struct ApiConfig {
            public ApiConfig(ApiKey apiKey, string game, string protocol, string host, int? port, ApiVersion? version) {
                this.ApiKey = apiKey;
                this.Game = game;
                this.Protocol = protocol ?? DEFAULT_PROTOCOL;
                this.Host = host;
                this.Port = port == null ? DEFAULT_PORT : (int)port;
                this.Version = version == null ? CURRENT_VERSION : (ApiVersion)version;
            }
            public ApiKey ApiKey;
            public string Game;
            public string Protocol;
            public string Host;
            public int Port;
            public ApiVersion Version;
        }

        public struct ApiEndpoint {
            public ApiEndpoint(string method, string path) {
                this.Method = method;
                this.Path = "/" + path;
            }
            public string Method;
            public string Path;
        }

        public struct METHOD {
            public const string GET = "GET";
            public const string POST = "POST";
            public const string DELETE = "DELETE";
        }

        public const ApiVersion CURRENT_VERSION = ApiVersion.V2;
        public const string DEFAULT_PROTOCOL = "https";
        public const int DEFAULT_PORT = 443;

        public ApiConfig Config { get; }
        public readonly string Game;
        public readonly ApiV2 V2;
        private readonly IAuthentication Authentication;
        private readonly HttpClient client = new HttpClient();

        public Api(ApiConfig config) {
            this.Config = config;
            this.Game = this.Config.Game;
            switch (this.Config.Version) {
                case ApiVersion.V2:
                    this.V2 = new ApiV2(this);
                    this.Authentication = this.V2.Authentication;
                    break;
            }
        }

        public async Task<dynamic> Info() {        
            List<int> expectedStatusCodes = new List<int> { 200 };

            return await this.Call(new Api.ApiEndpoint(Api.METHOD.GET, ""), null, expectedStatusCodes);
        }

        public async Task<dynamic> Call(ApiEndpoint endpoint, dynamic body, List<int> expectedStatusCodes) {
            string url = this.Config.Protocol + "://" + this.Config.Host + ":" + this.Config.Port + endpoint.Path;

            string data = "{}";
            if (body != null) { data = JsonConvert.SerializeObject(body); }

            client.DefaultRequestHeaders.Clear();
            foreach (var header in this.Authentication.PrepareHeaders(this.Config.ApiKey, endpoint.Method, endpoint.Path, data)) {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }


            HttpResponseMessage response = null;

            switch (endpoint.Method) {
                case Api.METHOD.GET:
                    response = await client.GetAsync(url);
                    break;
                case Api.METHOD.POST:                    
                    response = await client.PostAsync(url, new StringContent(data, Encoding.UTF8, "application/json"));
                    break;
                case Api.METHOD.DELETE:
                    response = await client.DeleteAsync(url);
                    break;
            }
                        
            string responseBody = await HandleResponseAsync(response, expectedStatusCodes);
            
            return JsonConvert.DeserializeObject("{'statusCode': " + (int)response.StatusCode + "," + " 'body': " + responseBody + "}");
        }

        private async Task<string> HandleResponseAsync(HttpResponseMessage response, List<int> expectedStatusCodes) {
            int statusCode = (int)response.StatusCode;
            string responseBody = await response.Content.ReadAsStringAsync();

            if (expectedStatusCodes != null && expectedStatusCodes.IndexOf(statusCode) == -1) {
                string errorMessage = "{ 'statusCode': " + statusCode + "," + " 'body': " + responseBody + "}";

                errorMessage = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(errorMessage));

                throw new MMOSRequestException(errorMessage);
            } else return responseBody;
        }

    }

}
