using System.Collections.Generic;


namespace MQTTLib.Protocol {
    public abstract class VariableHeader : IByteEncodable {
        public abstract IEnumerable<byte> Encode();
    }

    public class VariableHeaderNone : VariableHeader {
        public override IEnumerable<byte> Encode() {
            return new List<byte>();
        }
    }
}
