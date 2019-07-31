using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MMOS.SDK.V2 {

    public class Players {
        private Api api;

        public Players(Api api) {
            this.api = api;
        }

        public async Task<dynamic> Get(string playerCode, string project) {       

            string url = String.Format("games/{0}/players/{1}", new string[] { api.Game, playerCode });

            if (project != null) {
                url += String.Format("?project={0}", new string[] { project });
            }

            List<int> expectedStatusCodes = new List<int> { 200, 404 };

            return await api.Call(new Api.ApiEndpoint(Api.METHOD.GET, url), null, expectedStatusCodes);
        }


        public async Task<dynamic> CreateTask(string playerCode, dynamic body) {
            string url = String.Format("games/{0}/players/{1}/tasks", new string[] { api.Game, playerCode });
            List<int> expectedStatusCodes = new List<int> { 201 };
   
            return await api.Call(new Api.ApiEndpoint(Api.METHOD.POST, url), body, expectedStatusCodes);
        }
    }
}
