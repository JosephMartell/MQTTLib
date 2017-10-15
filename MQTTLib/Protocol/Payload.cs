using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib.Protocol {
    public abstract class Payload : IByteEncodable {
        public abstract IEnumerable<byte> Encode();
    }

    public class PayloadNone : Payload {
        public override IEnumerable<byte> Encode() {
            return new List<byte>();
        }
    }
}
