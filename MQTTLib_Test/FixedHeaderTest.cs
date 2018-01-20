using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQTTLib.Protocol;
using MQTTLib;
using System.Linq;

namespace MQTTLib_Test {
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class FixedHeaderTest {

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotCreateStandardPublishHeader() {
            FixedHeader fh = FixedHeader.CreateStandardHeader(ControlPacketType.PUBLISH, 0);
        }

        [TestMethod]
        public void TestConnectPacketByteValues() {
            //EncodedRemainingLength erl = new EncodedRemainingLength(5);
            //FixedHeader fh = FixedHeader.CreateStandardHeader(ControlPacketType.CONNECT, 20);

            //List<byte> expectedBytes = new List<byte>();
            //expectedBytes.Add(0x10);
            //expectedBytes.AddRange(erl.Encode().ToArray());
            
            //byte[] fhBytes = fh.Encode().ToArray();

            //Assert.AreEqual(expectedBytes.Count, fhBytes.Length);
            //for (int i = 0; i < expectedBytes.Count; i++) {
            //    Assert.AreEqual(expectedBytes[i], fhBytes[i]);
            //}
        }

        [TestMethod]
        public void TestUnsubscribePacketByteValues() {
            FixedHeader fh = FixedHeader.CreateStandardHeader(ControlPacketType.UNSUBSCRIBE, 0);

            byte[] expectedBytes = new byte[] { 0xa2, 0 };
            byte[] fhBytes = fh.Encode().ToArray();
            Assert.AreEqual(expectedBytes.Length, fhBytes.Length);
            Assert.AreEqual(expectedBytes[0], fhBytes[0]);
        }

        [TestMethod]
        public void SubscribeWithDupQoS2Retain() {
            FixedHeader fh = FixedHeader.CreatePublishHeader(true, QoSLevel.ExactlyOnce, true, 0);
            byte[] expectedBytes = new byte[] { 0x3d, 0 };
            byte[] fhBytes = fh.Encode().ToArray();
            Assert.AreEqual(expectedBytes.Length, fhBytes.Length);
            Assert.AreEqual(expectedBytes[0], fhBytes[0]);
        }


        [TestMethod]
        public void MSB() {
            UInt16 packetID = 0xff88;
            Assert.AreEqual(0xff, packetID.MostSignificantByte());

        }

        

        public void LSB() {
            UInt16 packetID = 0xff88;
            Assert.AreEqual(0x88, packetID.LeastSignificantByte());
        }
    }
}
