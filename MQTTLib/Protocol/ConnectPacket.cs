using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib.Protocol {
    public class ConnectPacket : ControlPacket {
        public ConnectPacket(string clientID, Will w, Authentication auth, bool cleanSession, UInt16 keepAliveTime) 
            : base(new ConnectVariableHeader(w,auth,cleanSession, keepAliveTime), new ConnectPayload(clientID, w, auth)) {
            FixedHeader = FixedHeader.CreateStandardHeader(ControlPacketType.CONNECT, (UInt16)(VariableHeader.Encode().Count() + Payload.Encode().Count()));
        }
    }
}