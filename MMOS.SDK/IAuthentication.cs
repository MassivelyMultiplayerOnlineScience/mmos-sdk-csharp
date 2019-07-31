using System;
using System.Collections.Generic;

namespace MMOS.SDK {

    public interface IAuthentication {
        Dictionary<string, string> PrepareHeaders(Api.ApiKey apiKey, string method, string path, string data);
    }

}
