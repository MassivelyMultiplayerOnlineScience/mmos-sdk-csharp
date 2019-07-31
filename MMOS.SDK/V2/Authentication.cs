using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MMOS.SDK.V2 {

    public class Authentication : IAuthentication {
        private const string CONTENT_SEPARATOR = "|";
        private const string MMOS_SIGNING_ALGORITHM = "MMOS1-HMAC-SHA256";

        public Dictionary<string, string> PrepareHeaders(Api.ApiKey apiKey, string method, string path, string data) {
        
            long timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            long nonce = (long)(Math.Floor(new System.Random().NextDouble() * timestamp)) + 1;
            string signature;
            string contentData = data ?? "{}";
            string[] contentParts = {
                MMOS_SIGNING_ALGORITHM,
                apiKey.Key,
                timestamp.ToString(),
                nonce.ToString(),
                method,
                path,
                contentData
            };

            string contents = String.Join(CONTENT_SEPARATOR, contentParts);

            string keyHash = CreateHmac(timestamp.ToString(), apiKey.Secret);
            signature = CreateHmac(keyHash, contents);

            Dictionary<string, string> headers = new Dictionary<string, string> {
                { "X-MMOS-Algorithm", MMOS_SIGNING_ALGORITHM },
                { "X-MMOS-Credential",apiKey.Key },
                { "X-MMOS-Timestamp", timestamp.ToString() },
                { "X-MMOS-Nonce", nonce.ToString() },
                { "X-MMOS-Signature", signature }
            };
            return headers;
        }

        private static string CreateHmac(string message, string key) {
            Encoding encoding = Encoding.ASCII;
            HMACSHA256 keyMac = new HMACSHA256(encoding.GetBytes(message));
            byte[] hash = keyMac.ComputeHash(encoding.GetBytes(key));
            return ByteToString(hash);
        }

        public static string ByteToString(byte[] buff) {
            string sbinary = "";
            for (int i = 0; i < buff.Length; i++) sbinary += buff[i].ToString("x2");
            return (sbinary);
        }
    }

}
