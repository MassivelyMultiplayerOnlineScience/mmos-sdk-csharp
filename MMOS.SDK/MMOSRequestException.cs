using System;

namespace MMOS.SDK {
    public class MMOSRequestException : System.Exception {
        public MMOSRequestException(string message) : base(message) { }
        public MMOSRequestException(string message, System.Exception inner) : base(message, inner) { }
    }
}
