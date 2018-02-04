using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using MQTTLib.Protocol;
using MQTTLib;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace MQTTLib_Test.Protocol {
    [TestClass]
    public class ConnectPayloadTest {

        [TestClass]
        public class EncodeMethod {
            UTF8Encoding utf8 = new UTF8Encoding();

            [TestMethod]
            public void Client_ID_encoded_correctly() {
                EncodedString clientID = "This is a client id";
                IByteEncodable cp = new ConnectPayload(clientID);

                EncodedString parsedEncodedString = 
                    new EncodedString(
                        new MemoryStream(cp.Encode().ToArray()));

                Assert.IsTrue(clientID == parsedEncodedString);
            }

            [TestMethod]
            public void Will_topic_encoded_when_provided() {
                EncodedString willTopic = "Will Topic";
                Will w = 
                    new Will(
                        QoSLevel.AtMostOnce, 
                        false, willTopic.Value, 
                        new EncodedDataField(utf8.GetBytes("Will Message")));

                EncodedString clientID = "test 1";

                IByteEncodable cp = new ConnectPayload(clientID, w);
                IEnumerable<byte> encodedBytes = cp.Encode();
                EncodedString encodedTopic = 
                    new EncodedString(
                        new MemoryStream(
                            encodedBytes.Skip(clientID.Encode().Count()).ToArray()));

                Assert.IsTrue(willTopic == encodedTopic);
            }

            [TestMethod]
            public void Will_msg_encoded_when_provided() {
                EncodedString clientID = new EncodedString("test 1");
                EncodedString willTopic = new EncodedString("Will Topic");
                EncodedDataField willMsg = new EncodedDataField(utf8.GetBytes("Will Message"));
                Will w = new Will(QoSLevel.AtMostOnce, false, willTopic, willMsg);
                IByteEncodable cp = new ConnectPayload(clientID, w);
                IEnumerable<byte> encodedBytes = cp.Encode();

                string encodedMsg = utf8.GetString(
                    new EncodedDataField(
                        encodedBytes.Skip(clientID.Length + 2)
                        .Skip(willTopic.Length + 4)
                        .ToArray()));

                Assert.IsTrue(utf8.GetString(willMsg.Data.ToArray()) == encodedMsg);
            }

            [TestMethod]
            public void Username_encoded_when_provided() {
                EncodedString username = new EncodedString("username");
                Authentication auth = new Authentication(username);
                EncodedString clientID = "ClientID";
                IByteEncodable cp = new ConnectPayload(clientID, null, auth);
                IEnumerable<byte> encodedBytes = cp.Encode();

                EncodedString encodedUsername = 
                    new EncodedString(
                        new MemoryStream(
                            encodedBytes.Skip(clientID.Encode().Count()).ToArray()));

                Assert.IsTrue(username == encodedUsername);

            }

            [TestMethod]
            public void Password_encoded_when_provided() {
                EncodedString clientID = "ClientID";
                Authentication auth = new Authentication("username", "TestPassword");
                ConnectPayload cp = new ConnectPayload(clientID, null, auth);
                IEnumerable<byte> encodedBytes = cp.Encode();

                string password = utf8.GetString(encodedBytes.Skip(clientID.Length + 2)
                                                             .Skip(auth.Username.Length + 4)
                                                             .ToArray());
                Assert.IsTrue(auth.Password == password);
            }
        }
    }
}
