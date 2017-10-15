using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib.Protocol {
    public class ConnectPayload : Payload {
        private EncodedString _clientID;
        private Will _w;
        private Authentication _auth;

        public ConnectPayload(EncodedString clientID, Will w = null, Authentication auth = null) {
            _clientID = clientID;
            _w = w;
            _auth = auth;
        }
        public override IEnumerable<byte> Encode() {
            List<byte> encodedBytes = new List<byte>();
            encodedBytes.AddRange(_clientID?.Encode());
            if (_w != null) {
                encodedBytes.AddRange(new EncodedString(_w.Topic).Encode());
                encodedBytes.AddRange(_w.Message.Encode());
            }
            if (_auth != null) {
                encodedBytes.AddRange(new EncodedString(_auth.Username).Encode());
                if (_auth.Password != null) {
                    encodedBytes.AddRange(new EncodedString(_auth.Password).Encode());
                }
            }
            return encodedBytes;
        }
    }
}
