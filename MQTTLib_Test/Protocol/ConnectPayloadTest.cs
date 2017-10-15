using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using MQTTLib.Protocol;
using MQTTLib;
using System.Linq;
using System.Collections.Generic;

namespace MQTTLib_Test.Protocol {
    [TestClass]
    public class ConnectPayloadTest {

        [TestClass]
        public class Encode {
            UTF8Encoding utf8 = new UTF8Encoding();

            [TestMethod]
            public void ClientIDPresent() {
                EncodedString clientID = "This is a client id";
                IByteEncodable cp = new ConnectPayload(clientID);

                EncodedString parsedEncodedString = new EncodedString(cp.Encode());
                Assert.IsTrue(clientID == parsedEncodedString);
            }

            [TestMethod]
            public void WillTopicPresent() {
                EncodedString willTopic = "Will Topic";
                Will w = new Will(QoSLevel.AtMostOnce, false, willTopic.Value, new EncodedDataField(utf8.GetBytes("Will Message")));
                EncodedString clientID = "test 1";

                IByteEncodable cp = new ConnectPayload(clientID, w);
                IEnumerable<byte> encodedBytes = cp.Encode();
                EncodedString encodedTopic = new EncodedString(encodedBytes.Skip(clientID.Encode().Count()));

                Assert.IsTrue(willTopic == encodedTopic);
            }

            [TestMethod]
            public void WillMsgPresent() {
                EncodedString clientID = new EncodedString("test 1");
                EncodedString willTopic = new EncodedString("Will Topic");
                EncodedDataField willMsg = utf8.GetBytes("Will Message");
                Will w = new Will(QoSLevel.AtMostOnce, false, willTopic, willMsg);
                IByteEncodable cp = new ConnectPayload(clientID, w);
                IEnumerable<byte> encodedBytes = cp.Encode();
                string encodedMsg = utf8.GetString(new EncodedDataField(encodedBytes.Skip(clientID.Length + 2)
                                                                                    .Skip(willTopic.Length + 4)
                                                                                    .ToArray()));
                Assert.IsTrue(utf8.GetString(willMsg.Data.ToArray()) == encodedMsg);
            }

            [TestMethod]
            public void Username() {
                EncodedString username = new EncodedString("username");
                Authentication auth = new Authentication(username);
                EncodedString clientID = "ClientID";
                IByteEncodable cp = new ConnectPayload(clientID, null, auth);
                IEnumerable<byte> encodedBytes = cp.Encode();
                EncodedString encodedUsername = new EncodedString(encodedBytes.Skip(clientID.Encode().Count()));

                Assert.IsTrue(username == encodedUsername);

            }

            [TestMethod]
            public void Password() {
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
