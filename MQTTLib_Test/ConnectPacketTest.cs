using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQTTLib.Protocol;
using MQTTLib;
using System.Linq;

namespace MQTTLib_Test {
    [TestClass]
    public class ConnectPacketTest {
        System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();
        private ConnectPacket SimpleConnectPacket;

        public ConnectPacketTest() {
            SimpleConnectPacket = new ConnectPacket("TEST");
        }

        [TestMethod]
        public void VerifySimpleConnectPacket() {
            byte[] exampleCP = new byte[] { 0x10,
                                            14,
                                            0x00,
                                            0x04,
                                            utf8.GetBytes("M")[0],
                                            utf8.GetBytes("Q")[0],
                                            utf8.GetBytes("T")[0],
                                            utf8.GetBytes("T")[0],
                                            0x04,
                                            0x02,
                                            0x00,
                                            0x00,
                                            utf8.GetBytes("T")[0],
                                            utf8.GetBytes("E")[0],
                                            utf8.GetBytes("S")[0],
                                            utf8.GetBytes("T")[0]
            };

            FixedHeader fh = SimpleConnectPacket.FixedHeader;
            Assert.AreEqual(FixedHeader.ControlPacketType.CONNECT, fh.Type);
            Assert.AreEqual("MQTT", SimpleConnectPacket.Protocol);

            byte[] encodedCP = SimpleConnectPacket.Encode().ToArray();

            Assert.AreEqual(exampleCP[0], encodedCP[0]);
            Assert.AreEqual(exampleCP[1], encodedCP[1]);  
            Assert.AreEqual(exampleCP[2], encodedCP[2]);
            Assert.AreEqual(exampleCP[3], encodedCP[3]);
            Assert.AreEqual(exampleCP[4], encodedCP[4]);
            Assert.AreEqual(exampleCP[5], encodedCP[5]);
            Assert.AreEqual(exampleCP[6], encodedCP[6]);
            Assert.AreEqual(exampleCP[7], encodedCP[7]);
            Assert.AreEqual(exampleCP[8], encodedCP[8]);
            Assert.AreEqual(exampleCP[9], encodedCP[9]);
            Assert.AreEqual(exampleCP[10], encodedCP[10]);
            Assert.AreEqual(exampleCP[11], encodedCP[11]);
            Assert.AreEqual(exampleCP[12], encodedCP[12]);
            Assert.AreEqual(exampleCP[13], encodedCP[13]);
            Assert.AreEqual(exampleCP[14], encodedCP[14]);
            Assert.AreEqual(exampleCP[15], encodedCP[15]);
        }

        [TestMethod]
        public void VerifyConnectPacketClientID() {
            string testClientID = "Client ID 1";
            ConnectPacket cp = new ConnectPacket(testClientID);

            Assert.AreEqual(testClientID, cp.ClientID);
            byte[] encodedCP = cp.Encode().ToArray();

            string encodedID = utf8.GetString(encodedCP.Skip(12).Take(testClientID.Length).ToArray());
            Assert.AreEqual(testClientID, encodedID);
        }

        [TestMethod]
        public void AuthIsEncoded() {

            string clientId = "Client ID 1";
            string username = "admin";
            string password = "password";
            Authentication auth = new Authentication(username, password);
            ConnectPacket cp = new ConnectPacket(clientId, null, auth);

            //TODO: verify username & password flags are set
            //TODO: verify username & password payload are present and correct
        }
    }
}
