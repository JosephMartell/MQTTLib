using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using MQTTLib.Protocol;

namespace MQTTLib {
    public class Client {
        Stream _stream;
        public string ID { get; }

        public Client(Stream stream, string id) {
            ID = id;
            _stream = stream;
        }

        /// <summary>
        /// Connect to an MQTT broker.  The borker must be at the receive end
        /// of teh stream passed in the constructor.
        /// </summary>
        /// <param name="clean">When true, a clean session with the broker will be established.</param>
        /// <param name="will">If a will is provided it will be passed to the server to be published upon this client's death.</param>
        /// <param name="auth">Authentication parameters passed to server</param>
        public void Connect(bool clean = true, Will will = null, Authentication auth = null, UInt16 keepAlive = 0) {
        //    ConnectPacket cp = new ConnectPacket(ID, will);
        //    _stream.Write(cp.Encode().ToArray(), 0, cp.Encode().Count());
        }

    }
}
