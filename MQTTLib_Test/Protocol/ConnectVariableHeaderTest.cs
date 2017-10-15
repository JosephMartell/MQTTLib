using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQTTLib.Protocol;
using System.Text;
using System.Linq;
using MQTTLib;

namespace MQTTLib_Test.Protocol {
    [TestClass]
    public class ConnectVariableHeaderTest {
        UTF8Encoding utf8 = new UTF8Encoding();
        ConnectVariableHeader simpleHeader = new ConnectVariableHeader();

        public ConnectVariableHeaderTest() {

        }

        [TestMethod]
        public void CleanSessionFlagEncoded() {
            byte CleanSessionFlags = GetFlagsFromHeader(new ConnectVariableHeader(null, null, true));
            Assert.IsTrue((CleanSessionFlags & 0x02) == 0x02);

            byte NotCleanSessionFlags = GetFlagsFromHeader(new ConnectVariableHeader(null, null, false));
            Assert.IsTrue((NotCleanSessionFlags & 0x02) == 0x00);
        }

        [TestMethod]
        public void WillFlagEncoded() {
            byte WithWillFlags = GetFlagsFromHeader(new ConnectVariableHeader(new Will(QoSLevel.AtMostOnce, 
                                                                              false, 
                                                                              "Test Will", 
                                                                              new EncodedDataField(utf8.GetBytes("Test Wil")))));
            Assert.IsTrue((WithWillFlags & 0x04) == 0x04);

            byte NoWillFlags = GetFlagsFromHeader(new ConnectVariableHeader());
            Assert.IsTrue((NoWillFlags & 0x04) == 0x00);
        }

        [TestMethod]
        public void WillQoSEncoded() {
            var AtMostOnceWill = new Will(QoSLevel.AtMostOnce, false, "Test Will", new EncodedDataField(utf8.GetBytes("Test Will")));
            byte AtMostOnceflags = GetFlagsFromHeader(new ConnectVariableHeader(AtMostOnceWill));
            Assert.IsTrue((AtMostOnceflags & 0x18) == 0);

            var AtLeastOnceWill = new Will(QoSLevel.AtLeastOnce, false, "Test Will", new EncodedDataField(utf8.GetBytes("Test Will")));
            byte AtLeastOnceflags = GetFlagsFromHeader(new ConnectVariableHeader(AtLeastOnceWill));
            Assert.IsTrue((AtLeastOnceflags & 0x18) == 0x08);

            var ExactlyOnceWill = new Will(QoSLevel.ExactlyOnce, false, "Test Will", new EncodedDataField(utf8.GetBytes("Test Will")));
            byte ExactlyOnceflags = GetFlagsFromHeader(new ConnectVariableHeader(ExactlyOnceWill));
            Assert.IsTrue((ExactlyOnceflags & 0x18) == 0x18);
        }

        [TestMethod]
        public void WillRetainEncoded() {
            byte RetainedFlags = GetFlagsFromHeader(new ConnectVariableHeader(new Will(QoSLevel.AtMostOnce, true, "", new EncodedDataField(new byte[0]))));
            Assert.IsTrue((RetainedFlags & 0x20) == 0x20);

            byte NotRetainedFlags = GetFlagsFromHeader(new ConnectVariableHeader(new Will(QoSLevel.AtMostOnce, false, "", new EncodedDataField(new byte[0]))));
            Assert.IsTrue((NotRetainedFlags & 0x20) == 0x00);
        }

        [TestMethod]
        public void UsernameFlagEncoded() {
            byte WithUserFlags = GetFlagsFromHeader(new ConnectVariableHeader(null, new MQTTLib.Authentication("Username")));
            Assert.IsTrue((WithUserFlags & 0x80) == 0x80);

            byte NoUserFlags = GetFlagsFromHeader(new ConnectVariableHeader());
            Assert.IsTrue((NoUserFlags & 0x80) == 0x00);
        }

        [TestMethod]
        public void KeepAliveEncoded() {
            Random ran = new Random();
            UInt16 kaValue = (UInt16)ran.Next(0, 65535);
            ConnectVariableHeader cvh = new ConnectVariableHeader(null, null, true, kaValue);
            Assert.AreEqual(kaValue.MostSignificantByte(), cvh.Encode().ToArray()[8]);
            Assert.AreEqual(kaValue.LeastSignificantByte(), cvh.Encode().ToArray()[9]);
        }

        [TestMethod]
        public void SimpleVariableHeaderPropertiesSet() {
            Assert.AreEqual("MQTT", simpleHeader.ProtocolName);
            Assert.AreEqual(4, simpleHeader.ProtocolLevel);
            Assert.AreEqual(0, simpleHeader.KeepAliveTime);
            Assert.AreEqual(false, simpleHeader.WillFlag);
            Assert.AreEqual(false, simpleHeader.WillRetain);
            Assert.AreEqual(QoSLevel.AtMostOnce, simpleHeader.WillQoS);
            Assert.AreEqual(false, simpleHeader.HasUsername);
            Assert.AreEqual(false, simpleHeader.HasPassword);
            Assert.AreEqual(true, simpleHeader.CleanSession);

        }

        [TestMethod]
        public void SimpleVariableHeaderEncodedCorrectly() {
            byte[] expectedEncoding = new byte[] { 0x00,
                                                   0x04,
                                                   utf8.GetBytes("M").First(),
                                                   utf8.GetBytes("Q").First(),
                                                   utf8.GetBytes("T").First(),
                                                   utf8.GetBytes("T").First(),
                                                   0x04,
                                                   0x02,
                                                   0x00,
                                                   0x00};

            byte[] encodedCVH = simpleHeader.Encode().ToArray();
            Assert.AreEqual(expectedEncoding.Length, encodedCVH.Length);

            for (int i = 0; i < expectedEncoding.Length; i++) {
                Assert.AreEqual(expectedEncoding[i], encodedCVH[i]);
            }

        }

        private byte GetFlagsFromHeader(ConnectVariableHeader cvh) {
            return cvh.Encode().ToArray()[7];
        }
    }
}
