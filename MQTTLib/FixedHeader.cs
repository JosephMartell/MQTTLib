using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib {

    public class FixedHeader {
        public enum ControlPacketType : byte {
            CONNECT = 1,
            CONNACK = 2,
            PUBLISH = 3,
            PUBACK = 4,
            PUBREC = 5,
            PUBREL = 6,
            PUBCOMP = 7,
            SUBSCRIBE = 8,
            SUBACK = 9,
            UNSUBSCRIBE = 10,
            UNSUBACK = 11,
            PINGREQ = 12,
            PINGRESP = 13,
            DISCONNECT = 14
        }

        public class ControlPacketFlags {
            public bool Duplicate { get; protected set; }
            public QoSLevel QoS { get; protected set; }
            public bool Retain { get; protected set; }

            protected ControlPacketFlags(bool duplicate, QoSLevel qos, bool retain) {
                Duplicate = duplicate;
                QoS = qos;
                Retain = retain;
            }

            public IEnumerable<byte> ToBytes() {
                byte retByte = 0x00;
                if (Duplicate) {
                    retByte |= 0x08;
                }
                retByte |= (byte)(((int)QoS) << 1);
                if (Retain) {
                    retByte |= 0x01;
                }

                return new List<byte>() { retByte };
            }
            internal static ControlPacketFlags GetDefinedFlags(ControlPacketType type) {
                switch (type) {
                    case ControlPacketType.CONNECT:
                    case ControlPacketType.CONNACK:
                    case ControlPacketType.PUBACK:
                    case ControlPacketType.PUBREC:
                    case ControlPacketType.PUBCOMP:
                    case ControlPacketType.SUBACK:
                    case ControlPacketType.UNSUBACK:
                    case ControlPacketType.PINGREQ:
                    case ControlPacketType.PINGRESP:
                    case ControlPacketType.DISCONNECT:
                        return new ControlPacketFlags(false, QoSLevel.AtMostOnce, false);
                    case ControlPacketType.PUBREL:
                    case ControlPacketType.SUBSCRIBE:
                    case ControlPacketType.UNSUBSCRIBE:
                        return new ControlPacketFlags(false, QoSLevel.AtLeastOnce, false);
                    case ControlPacketType.PUBLISH:
                        throw new ArgumentException(nameof(type), "There is no defined flag configuration for a PUBLISH control packet.");
                    default:
                        throw new ArgumentException(nameof(type), "Unknown Control Packet Type supplied.  Default flags unknown.");
                }
            }
            internal static ControlPacketFlags GetPublishFlags(bool duplicate, QoSLevel qos, bool retain) {
                return new ControlPacketFlags(duplicate, qos, retain);
            }
        }


        public ControlPacketType Type { get; private set; }
        public ControlPacketFlags Flags { get; private set; }

        protected FixedHeader(ControlPacketType type, ControlPacketFlags flags) {
            Type = type;
            Flags = flags;
        }

        public IEnumerable<byte> ToBytes() {
            List<byte> bytes = new List<byte>();
            byte byte1 = (byte)Type;
            byte1 <<= 4;

            byte1 |= Flags.ToBytes().ToArray()[0];
            bytes.Add(byte1);
            return bytes;
        }


        public static FixedHeader CreatePublishHeader(bool duplicate, QoSLevel qos, bool retain) {
            return new FixedHeader(ControlPacketType.PUBLISH, ControlPacketFlags.GetPublishFlags(duplicate, qos, retain));
        }

        public static FixedHeader CreateStandardHeader(ControlPacketType type) {
            if (type == ControlPacketType.PUBLISH) {
                throw new ArgumentException(nameof(type), "There is no defined flag configuration for a PUBLISH control packet.  Use the CreatePublishHeader method to create this header type");
            }
            return new FixedHeader(type, ControlPacketFlags.GetDefinedFlags(type));
        }


    }




}
