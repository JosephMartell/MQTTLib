using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTLib.Protocol;

namespace MQTTLib {
    public class Will {
        public QoSLevel QoS { get; }
        public bool Retain { get; }
        public string Topic { get; }
        public string Message { get; }

        public Will(QoSLevel qos, bool retain, string topic, string message) {
            QoS = qos;
            Retain = retain;
            Topic = topic;
            Message = message;
        }
    }
}
