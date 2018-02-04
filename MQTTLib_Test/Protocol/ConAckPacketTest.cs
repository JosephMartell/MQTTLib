using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQTTLib;
using MQTTLib.Protocol;
using System.Linq;
using System.Collections.Generic;

namespace MQTTLib_Test.Protocol {

    //Integration test - variable header tested separately for all possible response codes
    //                   Payload is always none
    //                   Fixed header is always the same
    [TestClass]
    public class ConAckPacketTest {

        PayloadNone pn = new PayloadNone();

        ConnAckPacket Accepted = new ConnAckPacket(true, ConnectReturnCode.Accepted);
        [TestMethod]
        public void Connection_accepted_response_encoded_correctly() {
            VariableHeader vh = new ConnAckVariableHeader(true, ConnectReturnCode.Accepted);
            FixedHeader fh = FixedHeader.CreateStandardHeader(ControlPacketType.CONNACK, (UInt16)(vh.Encode().Count() + pn.Encode().Count()));
            Assert.IsTrue(CheckExpectedByteEncoding(fh, Accepted.FixedHeader));
            Assert.IsTrue(CheckExpectedByteEncoding(vh, Accepted.VariableHeader));
            Assert.IsTrue(CheckExpectedByteEncoding(pn, Accepted.Payload));
        }

        ConnAckPacket ServerUnavailable = new ConnAckPacket(false, ConnectReturnCode.ServerUnavailable);
        [TestMethod]
        public void ServerUnavailableTest() {
            VariableHeader vh = new ConnAckVariableHeader(false, ConnectReturnCode.ServerUnavailable);
            FixedHeader fh = FixedHeader.CreateStandardHeader(ControlPacketType.CONNACK, (UInt16)(vh.Encode().Count() + pn.Encode().Count()));
            Assert.IsTrue(CheckExpectedByteEncoding(fh, ServerUnavailable.FixedHeader));
            Assert.IsTrue(CheckExpectedByteEncoding(vh, ServerUnavailable.VariableHeader));
            Assert.IsTrue(CheckExpectedByteEncoding(pn, ServerUnavailable.Payload));
        }


        ConnAckPacket AuthenticationFailed = new ConnAckPacket(true, ConnectReturnCode.AuthenticationFailed);
        [TestMethod]
        public void AuthenticationFailedTest() {
            VariableHeader vh = new ConnAckVariableHeader(true, ConnectReturnCode.AuthenticationFailed);
            FixedHeader fh = FixedHeader.CreateStandardHeader(ControlPacketType.CONNACK, (UInt16)(vh.Encode().Count() + pn.Encode().Count()));
            Assert.IsTrue(CheckExpectedByteEncoding(fh, AuthenticationFailed.FixedHeader));
            Assert.IsTrue(CheckExpectedByteEncoding(vh, AuthenticationFailed.VariableHeader));
            Assert.IsTrue(CheckExpectedByteEncoding(pn, AuthenticationFailed.Payload));
        }

        public bool CheckExpectedByteEncoding(IByteEncodable expected, IByteEncodable actual)
        {
            Assert.AreEqual(expected.Encode().Count(), actual.Encode().Count());
            for (int i = 0; i < expected.Encode().Count(); i++)
            {
                Assert.AreEqual(expected.Encode().ToArray()[i], actual.Encode().ToArray()[i]);
            }

            return true;
        }

    }
}
