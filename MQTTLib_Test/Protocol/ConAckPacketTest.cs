using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQTTLib;
using MQTTLib.Protocol;
using System.Linq;
using System.Collections.Generic;

namespace MQTTLib_Test.Protocol {
    [TestClass]
    public class ConAckPacketTest {

        public bool Test(IByteEncodable expected, IByteEncodable actual) {
            Assert.AreEqual(expected.Encode().Count(), actual.Encode().Count());
            for (int i = 0; i < expected.Encode().Count(); i++) {
                Assert.AreEqual(expected.Encode().ToArray()[i], actual.Encode().ToArray()[i]);
            }

            return true;
        }

        PayloadNone pn = new PayloadNone();

        ConnAckPacket Accepted = new ConnAckPacket(true, ConnectReturnCode.Accepted);
        [TestMethod]
        public void AcceptedTest() {
            VariableHeader vh = new ConnAckVariableHeader(true, ConnectReturnCode.Accepted);
            FixedHeader fh = FixedHeader.CreateStandardHeader(ControlPacketType.CONNACK, (UInt16)(vh.Encode().Count() + pn.Encode().Count()));
            Assert.IsTrue(Test(fh, Accepted.FixedHeader));
            Assert.IsTrue(Test(vh, Accepted.VariableHeader));
            Assert.IsTrue(Test(pn, Accepted.Payload));
        }


        ConnAckPacket UnsupportedProtocol = new ConnAckPacket(false, ConnectReturnCode.UnsupportedProtocol);
        [TestMethod]
        public void UnsupportedProtocolTest() {
            VariableHeader vh = new ConnAckVariableHeader(false, ConnectReturnCode.UnsupportedProtocol);
            FixedHeader fh = FixedHeader.CreateStandardHeader(ControlPacketType.CONNACK, (UInt16)(vh.Encode().Count() + pn.Encode().Count()));
            Assert.IsTrue(Test(fh, UnsupportedProtocol.FixedHeader));
            Assert.IsTrue(Test(vh, UnsupportedProtocol.VariableHeader));
            Assert.IsTrue(Test(pn, UnsupportedProtocol.Payload));
        }


        ConnAckPacket IdentifierRejected = new ConnAckPacket(true, ConnectReturnCode.IdentifierRejected);
        [TestMethod]
        public void IdentifierRejectedTest() {
            VariableHeader vh = new ConnAckVariableHeader(true, ConnectReturnCode.IdentifierRejected);
            FixedHeader fh = FixedHeader.CreateStandardHeader(ControlPacketType.CONNACK, (UInt16)(vh.Encode().Count() + pn.Encode().Count()));
            Assert.IsTrue(Test(fh, IdentifierRejected.FixedHeader));
            Assert.IsTrue(Test(vh, IdentifierRejected.VariableHeader));
            Assert.IsTrue(Test(pn, IdentifierRejected.Payload));
        }

        ConnAckPacket ServerUnavailable = new ConnAckPacket(false, ConnectReturnCode.ServerUnavailable);
        [TestMethod]
        public void ServerUnavailableTest() {
            VariableHeader vh = new ConnAckVariableHeader(false, ConnectReturnCode.ServerUnavailable);
            FixedHeader fh = FixedHeader.CreateStandardHeader(ControlPacketType.CONNACK, (UInt16)(vh.Encode().Count() + pn.Encode().Count()));
            Assert.IsTrue(Test(fh, ServerUnavailable.FixedHeader));
            Assert.IsTrue(Test(vh, ServerUnavailable.VariableHeader));
            Assert.IsTrue(Test(pn, ServerUnavailable.Payload));
        }


        ConnAckPacket AuthenticationFailed = new ConnAckPacket(true, ConnectReturnCode.AuthenticationFailed);
        [TestMethod]
        public void AuthenticationFailedTest() {
            VariableHeader vh = new ConnAckVariableHeader(true, ConnectReturnCode.AuthenticationFailed);
            FixedHeader fh = FixedHeader.CreateStandardHeader(ControlPacketType.CONNACK, (UInt16)(vh.Encode().Count() + pn.Encode().Count()));
            Assert.IsTrue(Test(fh, AuthenticationFailed.FixedHeader));
            Assert.IsTrue(Test(vh, AuthenticationFailed.VariableHeader));
            Assert.IsTrue(Test(pn, AuthenticationFailed.Payload));
        }

        ConnAckPacket NotAuthorized = new ConnAckPacket(false, ConnectReturnCode.NotAuthorized);
        [TestMethod]
        public void NotAuthorizedTest() {
            VariableHeader vh = new ConnAckVariableHeader(false, ConnectReturnCode.NotAuthorized);
            FixedHeader fh = FixedHeader.CreateStandardHeader(ControlPacketType.CONNACK, (UInt16)(vh.Encode().Count() + pn.Encode().Count()));
            Assert.IsTrue(Test(fh, NotAuthorized.FixedHeader));
            Assert.IsTrue(Test(vh, NotAuthorized.VariableHeader));
            Assert.IsTrue(Test(pn, NotAuthorized.Payload));
        }
    }
}
