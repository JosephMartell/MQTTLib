using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTLib;

namespace MQTTLib.Protocol {
    public class ConnectVariableHeader : VariableHeader {
        public string ProtocolName {
            get { return "MQTT"; }
        }
        public byte ProtocolLevel {
            get { return 4; }
        }

        public bool HasUsername { get; }
        public bool HasPassword { get; }
        public bool WillRetain { get; }
        public bool WillFlag { get; }
        public QoSLevel WillQoS { get; }
        public bool CleanSession { get; }

        public UInt16 KeepAliveTime { get; }

        public ConnectVariableHeader(Will will = null, Authentication auth = null, bool cleanSession = true, UInt16 keepAliveTime = 0) {
            WillRetain = false;
            WillFlag = false;
            WillQoS = QoSLevel.AtMostOnce;
            if (will != null) {
                WillRetain = will.Retain;
                WillFlag = true;
                WillQoS = will.QoS;
            }
            HasUsername = false;
            HasPassword = false;
            if (auth != null) {
                HasUsername = (auth.Username != null);
                HasPassword = (auth.Password != null);
            }

            CleanSession = cleanSession;
            KeepAliveTime = keepAliveTime;
        }

        public override IEnumerable<byte> Encode() {
            UTF8Encoding utf8 = new UTF8Encoding();
            List<byte> retBytes = new List<byte>();
            UInt16 ProtocolNameLen = (UInt16)ProtocolName.Length;

            retBytes.Add(ProtocolNameLen.MostSignificantByte());
            retBytes.Add(ProtocolNameLen.LeastSignificantByte());
            retBytes.AddRange(utf8.GetBytes(ProtocolName));
            retBytes.Add(ProtocolLevel);
            retBytes.Add(GetFlags());
            retBytes.Add(KeepAliveTime.MostSignificantByte());
            retBytes.Add(KeepAliveTime.LeastSignificantByte());

            return retBytes;
        }

        protected byte GetFlags() {
            byte flag = 0x00;
            if (HasUsername) {
                flag |= 0x80;
            }
            if (HasPassword) {
                flag |= 0x40;
            }
            if (WillRetain) {
                flag |= 0x20;
            }
            switch (WillQoS) {
                case QoSLevel.AtMostOnce:
                    flag |= 0x00;
                    break;
                case QoSLevel.AtLeastOnce:
                    flag |= 0x08;
                    break;
                case QoSLevel.ExactlyOnce:
                    flag |= 0x18;
                    break;
            }

            if (WillFlag) {
                flag |= 0x04;
            }
            if (CleanSession) {
                flag |= 0x02;
            }
            return flag;
        }
    }
}
