using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib.Protocol {
    public class ConnectPacket : IByteEncodable {
        public FixedHeader FixedHeader { get; }

        public string Protocol { get { return "MQTT"; } }

        public byte ProtocolLevel { get { return 4; } }
   
        public bool CleanSession { get; }

        public string ClientID {  get; }

        public Will Will { get; }

        public string Username { get; }

        public string Password { get; }

        public ConnectPacket(string clientID, Will will = null, Authentication auth = null) {
            ClientID = clientID;
            Will = null;
            FixedHeader = FixedHeader.CreateStandardHeader(FixedHeader.ControlPacketType.CONNECT, 14);
        }

        public ConnectPacket(string clientID, Will will) {
            ClientID = clientID;
            Will = will;
            FixedHeader = FixedHeader.CreateStandardHeader(FixedHeader.ControlPacketType.CONNECT, 14);
        }

        public IEnumerable<byte> Encode() {

            UTF8Encoding utf8 = new UTF8Encoding();

            List<byte> retBytes = FixedHeader.Encode().ToList();
            retBytes.Add(0x00);
            retBytes.Add(0x04);
            retBytes.AddRange (utf8.GetBytes(Protocol));
            retBytes.Add(ProtocolLevel);
            retBytes.Add(0x02);
            retBytes.Add(0x00);
            retBytes.Add(0x00);
            retBytes.AddRange(utf8.GetBytes(ClientID));
            return retBytes;
        }
    }
}
