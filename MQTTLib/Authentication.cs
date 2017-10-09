using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTLib.Protocol;

namespace MQTTLib {
    public class Authentication : IByteEncodable {
        public string Username { get; }
        public string Password { get; }
        private EncodedRemainingLength _erl;
        public Authentication(string username, string password = null) {
            Username = username;
            Password = password;
            if ((password != null) && (password.Length > 0)) {
                _erl = new EncodedRemainingLength((uint)password.Length);
            }
        }

        public IEnumerable<byte> Encode() {
            UTF8Encoding utf8 = new UTF8Encoding();

            List<byte> retBytes = new List<byte>();
            retBytes.AddRange(utf8.GetBytes(Username));
            if (Password != null) {
                retBytes.AddRange(_erl.Encode());
                retBytes.AddRange(utf8.GetBytes(Password));
            }
            
            return retBytes;
        }
    }
}
