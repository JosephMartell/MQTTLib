using System.Collections.Generic;

namespace MQTTLib.Protocol {
    public class ConnAckVariableHeader : VariableHeader {
        public bool SessionPresent { get; }
        public ConnectReturnCode ReturnCode { get; }

        public ConnAckVariableHeader(bool sessionPresent, ConnectReturnCode returnCode) {
            SessionPresent = sessionPresent;
            ReturnCode = returnCode;
        }

        public override IEnumerable<byte> Encode() {
            return new byte[] {
                (byte)(SessionPresent ? 0x01 : 0x00),
                (byte)ReturnCode
            };
        }
    }
}
