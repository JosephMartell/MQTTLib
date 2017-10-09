using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib {
    public static class UINTExtensions {
        public static byte MostSignificantByte(this UInt16 ui) {
            UInt16 scratch = ui;

            scratch >>= 8;
            return (byte)(scratch);
        }

        public static byte LeastSignificantByte(this UInt16 ui) {
            UInt16 scratch = ui;
            return (byte)(scratch);
        }
    }
}
