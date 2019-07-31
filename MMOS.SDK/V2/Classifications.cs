
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MMOS.SDK.V2 {

    public class Classifications {
        private Api api;

        public Classifications(Api api) {
            this.api = api;
        }


        public async Task<dynamic> Create(dynamic body) {            
            string url = "classifications";
            List<int> expectedStatusCodes = new List<int> { 201 };

            body.game = api.Game;

            return await api.Call(new Api.ApiEndpoint(Api.METHOD.POST, url), body, expectedStatusCodes);
        }
    }
}
















//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Dynamic;

//namespace MmosSdk
//{
//    public class Classifications
//    {
//        private Api api;

//        public Classifications(Api api) {  this.api = api; }

//        /**
//         *
//         * @param taskId Task Id
//         * @param jsonResult Project specific result string. Must be valid JSON
//         * @param timeTaken time taken to solve the task in milliseconds
//         * @param playerCode Player Code
//         * @param playerGroup Player Group Code
//         * @return
//         * @throws MMOSRequestException
//         * @throws MMOSAuthenticationException
//         */
//        public String Create(int taskId, dynamic jsonResult, int timeTaken, String playerCode, String playerGroup)
//        {
//            String url = "/classifications";
//            Object[] callArgs = { };
//            List<int> expectedStatusCodes = new List<int> { 201 };

//            dynamic task = new ExpandoObject();
//            task.id = taskId;
//            task.result = jsonResult;

//            dynamic circumstances = new ExpandoObject();
//            circumstances.t = timeTaken;

//            dynamic requestObject = new ExpandoObject();
//            requestObject.game = api.game;
//            requestObject.task = task;
//            requestObject.circumstances = circumstances;
//            requestObject.player = playerCode;
//            requestObject.playergroup = playerGroup;

//            string body = JsonConvert.SerializeObject(requestObject, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
//            return api.Post(String.Format(url, callArgs), body,expectedStatusCodes);
//        }

//    }  
//}
