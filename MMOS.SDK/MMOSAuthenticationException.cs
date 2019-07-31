using System;

namespace MMOS.SDK {
    public class MMOSAuthenticationException : System.Exception {
        public MMOSAuthenticationException(string message) : base(message) { }
        public MMOSAuthenticationException(string message, System.Exception inner) : base(message, inner) { }
    }
}
