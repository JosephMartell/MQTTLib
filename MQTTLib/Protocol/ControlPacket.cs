using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib.Protocol {

    public abstract class ControlPacket : IByteEncodable {
        public FixedHeader FixedHeader { get; protected set; }

        public VariableHeader VariableHeader { get; protected set; }

        public Payload Payload { get; protected set; }

        protected ControlPacket(VariableHeader vh, Payload pl) {
            VariableHeader = vh;
            Payload = pl;
        }

        public IEnumerable<byte> Encode() {
            return FixedHeader.Encode()
                              .Concat(VariableHeader.Encode())
                              .Concat(Payload.Encode());
        }
    }
}
