using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib.Protocol {

    public class FixedHeader : IByteEncodable {

        public ControlPacketType Type { get; private set; }
        public ControlPacketFlags Flags { get; private set; }
        public EncodedRemainingLength RemainingLength { get; private set; }

        protected FixedHeader(ControlPacketType type, ControlPacketFlags flags, UInt32 remainingLength = 0) {
            Type = type;
            Flags = flags;
            RemainingLength = new EncodedRemainingLength(remainingLength);
        }

        public IEnumerable<byte> Encode() {
            List<byte> bytes = new List<byte>();
            byte byte1 = (byte)Type;
            byte1 <<= 4;

            byte1 |= Flags.ToBytes().ToArray()[0];
            bytes.Add(byte1);
            bytes.AddRange(RemainingLength.Encode());
            return bytes;
        }

        public static FixedHeader CreatePublishHeader(bool duplicate, QoSLevel qos, bool retain, UInt32 remainingLength) {
            return new FixedHeader(ControlPacketType.PUBLISH, ControlPacketFlags.GetPublishFlags(duplicate, qos, retain),remainingLength);
        }

        public static FixedHeader CreateStandardHeader(ControlPacketType type, UInt32 remainingLength) {
            if (type == ControlPacketType.PUBLISH) {
                throw new ArgumentException(nameof(type), "There is no defined flag configuration for a PUBLISH control packet.  Use the CreatePublishHeader method to create this header type");
            }
            return new FixedHeader(type, ControlPacketFlags.GetDefinedFlags(type), remainingLength);
        }

    }
}
