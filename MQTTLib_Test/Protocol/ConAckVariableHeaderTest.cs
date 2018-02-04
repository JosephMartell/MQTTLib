using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTLib.Protocol;

namespace MQTTLib_Test.Protocol {

    [TestClass]
    public class ConAckVariableHeaderTest {

        [TestClass]
        public class SessionPresentFlag {
            [TestMethod]
            public void True_Encoded() {
                IByteEncodable cavh = new ConnAckVariableHeader(true, ConnectReturnCode.Accepted);

                IEnumerable<byte> encodedBytes = cavh.Encode();
                Assert.AreEqual(0x01, encodedBytes.First());
            }

            [TestMethod]
            public void False_Encoded() {
                IByteEncodable cavh = new ConnAckVariableHeader(false, ConnectReturnCode.Accepted);

                IEnumerable<byte> encodedBytes = cavh.Encode();
                Assert.AreEqual(0x00, encodedBytes.First());
            }
        }

        [TestClass]
        public class ConnectReturnCodes {
            [TestMethod]
            public void Accepted_encoded() {
                IByteEncodable cavh = new ConnAckVariableHeader(false, ConnectReturnCode.Accepted);
                Assert.AreEqual(0x00, cavh.Encode().Skip(1).First());
            }

            [TestMethod]
            public void Unsupported_Protocol_encoded() {
                IByteEncodable cavh = new ConnAckVariableHeader(false, ConnectReturnCode.UnsupportedProtocol);
                Assert.AreEqual(0x01, cavh.Encode().Skip(1).First());
            }

            [TestMethod]
            public void Identifier_Rejected_encoded() {
                IByteEncodable cavh = new ConnAckVariableHeader(false, ConnectReturnCode.IdentifierRejected);
                Assert.AreEqual(0x02, cavh.Encode().Skip(1).First());
            }

            [TestMethod]
            public void Server_Unavailable_encoded() {
                IByteEncodable cavh = new ConnAckVariableHeader(false, ConnectReturnCode.ServerUnavailable);
                Assert.AreEqual(0x03, cavh.Encode().Skip(1).First());
            }

            [TestMethod]
            public void Authentication_Failed_encoded() {
                IByteEncodable cavh = new ConnAckVariableHeader(false, ConnectReturnCode.AuthenticationFailed);
                Assert.AreEqual(0x04, cavh.Encode().Skip(1).First());
            }

            [TestMethod]
            public void Not_Authorized_encoded() {
                IByteEncodable cavh = new ConnAckVariableHeader(false, ConnectReturnCode.NotAuthorized);
                Assert.AreEqual(0x05, cavh.Encode().Skip(1).First());
            }
        }
    }
}
