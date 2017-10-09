using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib.Protocol {
    interface IByteEncodable {
        IEnumerable<byte> Encode();
    }
}
