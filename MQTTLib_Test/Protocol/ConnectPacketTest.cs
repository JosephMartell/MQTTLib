using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQTTLib.Protocol;
using MQTTLib;
using System.Linq;
using System.Collections.Generic;

namespace MQTTLib_Test {
    [TestClass]
    public class ConnectPacketTest {
        System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();
        Will w;
        Authentication auth;
        EncodedString clientID;
        ConnectPacket cp;

        public ConnectPacketTest() {
            w = new Will(QoSLevel.AtLeastOnce, true, "Test Topic", new EncodedDataField(utf8.GetBytes("Will payload")));
            auth = new Authentication("username", "password");
            clientID = new EncodedString("Client ID");
            cp = new ConnectPacket(clientID, w, auth, true, 5);
        }

        [TestMethod]
        public void CorrectFixedHeader() {
            FixedHeader fh = FixedHeader.CreateStandardHeader(ControlPacketType.CONNECT, (UInt16)(cp.VariableHeader.Encode().Count() + cp.Payload.Encode().Count()));
            byte[] expectedBytes = fh.Encode().ToArray();
            for (int i = 0; i < expectedBytes.Count(); i++) {
                Assert.AreEqual(expectedBytes[i], cp.FixedHeader.Encode().ToArray()[i]);
            }
        }

        [TestMethod]
        public void VariableHeader() {
            ConnectVariableHeader cvh = new ConnectVariableHeader(w, auth, true, 5);

            var expectedBytes = cvh.Encode().ToArray();
            for (int i = 0; i < expectedBytes.Count(); i++) {
                Assert.AreEqual(expectedBytes[i], cp.VariableHeader.Encode().ToArray()[i]);
            }
        }

        [TestMethod]
        public void Payload() {
            ConnectPayload payload = new ConnectPayload(clientID, w, auth);
            var expectedBytes = payload.Encode().ToArray();

            for (int i = 0; i < expectedBytes.Count(); i++) {
                Assert.AreEqual(expectedBytes[i], cp.Payload.Encode().ToArray()[i]);
            }
        }
    }
}
