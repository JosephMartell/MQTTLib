using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib {
    public class EncodedRemainingLength {
        public UInt32 Length { get; }

        public EncodedRemainingLength(UInt32 length) {
            if (length > 268435455) {
                throw new ArgumentOutOfRangeException(nameof(length), "Length was too high.  Maximum value of length is 268,435,455 bytes");
            }
            Length = length;
        }

        public EncodedRemainingLength(IEnumerable<byte> bytes) {
            UInt32 length = 0;
            byte temp = 0x00;
            int multiplier = 1;
            foreach (var b in bytes) {
                temp = (byte)(b & 0x7F);
                length += (UInt32)(temp * multiplier);
                multiplier *= 128;
            }
            Length = length;
        }

        public IEnumerable<byte> Encode() {
            UInt32 length = Length;

            byte encodedByte = 0x00;
            List<byte> returnBytes = new List<byte>();
            do {
                encodedByte = Convert.ToByte(length % 128);
                length /= 128;
                if (length > 0) {
                    encodedByte = Convert.ToByte(encodedByte | 128);
                }
                returnBytes.Add(encodedByte);
            } while (length > 0);

            return returnBytes;
        }
    }
}
