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
        public class SessionPresent {
            [TestMethod]
            public void TrueEncoded() {
                IByteEncodable cavh = new ConnAckVariableHeader(true, ConnectReturnCode.Accepted);

                IEnumerable<byte> encodedBytes = cavh.Encode();
                Assert.AreEqual(0x01, encodedBytes.First());
            }

            [TestMethod]
            public void FalseEncoded() {
                IByteEncodable cavh = new ConnAckVariableHeader(false, ConnectReturnCode.Accepted);

                IEnumerable<byte> encodedBytes = cavh.Encode();
                Assert.AreEqual(0x00, encodedBytes.First());
            }
        }

        [TestClass]
        public class ConnectReturnCodes {
            [TestMethod]
            public void Accepted() {
                IByteEncodable cavh = new ConnAckVariableHeader(false, ConnectReturnCode.Accepted);
                Assert.AreEqual(0x00, cavh.Encode().Skip(1).First());
            }

            [TestMethod]
            public void UnsupportedProtocol() {
                IByteEncodable cavh = new ConnAckVariableHeader(false, ConnectReturnCode.UnsupportedProtocol);
                Assert.AreEqual(0x01, cavh.Encode().Skip(1).First());
            }

            [TestMethod]
            public void IdentifierRejected() {
                IByteEncodable cavh = new ConnAckVariableHeader(false, ConnectReturnCode.IdentifierRejected);
                Assert.AreEqual(0x02, cavh.Encode().Skip(1).First());
            }

            [TestMethod]
            public void ServerUnavailable() {
                IByteEncodable cavh = new ConnAckVariableHeader(false, ConnectReturnCode.ServerUnavailable);
                Assert.AreEqual(0x03, cavh.Encode().Skip(1).First());
            }

            [TestMethod]
            public void AuthenticationFailed() {
                IByteEncodable cavh = new ConnAckVariableHeader(false, ConnectReturnCode.AuthenticationFailed);
                Assert.AreEqual(0x04, cavh.Encode().Skip(1).First());
            }

            [TestMethod]
            public void NotAuthorized() {
                IByteEncodable cavh = new ConnAckVariableHeader(false, ConnectReturnCode.NotAuthorized);
                Assert.AreEqual(0x05, cavh.Encode().Skip(1).First());
            }
        }
    }
}
