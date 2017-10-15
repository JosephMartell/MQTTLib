using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib.Protocol {
    public class ConnAckPacket : ControlPacket {
        public ConnAckPacket(bool sessionPresent, ConnectReturnCode returnCode)
            : base(new ConnAckVariableHeader(sessionPresent, returnCode), new PayloadNone()) {
            FixedHeader = FixedHeader.CreateStandardHeader(ControlPacketType.CONNACK, (UInt16)(VariableHeader.Encode().Count() + Payload.Encode().Count()));
        }
    }
}
