using System;
using Newtonsoft.Json;

namespace MMOS.SDK.Tests {

    public static class Config {
        static Config() {

        }

        public static Api.ApiKey ApiKey = new Api.ApiKey(
                Environment.GetEnvironmentVariable("MMOS_SDK_TEST_API_KEY"),
                Environment.GetEnvironmentVariable("MMOS_SDK_TEST_API_SECRET"));
        public static string Game = Environment.GetEnvironmentVariable("MMOS_SDK_TEST_GAME");
        public static string Host = "api.depo.mmos.blue";
        public static string Project = "unige-exoplanet";
        public static string Player = "testPlayer";
        public static string PlayerAccountCode = "testAccount";
        public static string PlayerGroup = "testPlayerGroup";
        public static dynamic Result = JsonConvert.DeserializeObject("{\"transits\": [{\"epoch\": 2454132.32909,\"period\": 4.29507,\"transitMarkers\": [54137.336412, 54141.631482, 54145.926552]}, {\"epoch\": 2454132.32909,\"period\": 4.29507,\"transitMarkers\": [54152.221622]}],\"stellarActivity\": [100]}");
        public static dynamic Circumstances = JsonConvert.DeserializeObject("{\"t\": 6000}");

        public static Api.ApiConfig ApiConfig = new Api.ApiConfig(
            Config.ApiKey,
            Config.Game,
            null,
            Config.Host,
            null,
            null
        );

    }

}
