using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQTTLib.Protocol;
using MQTTLib;
using System.Linq;

namespace MQTTLib_Test.Protocol
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class FixedHeaderTest
    {
        [TestClass]
        public class FactoryMethods
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Creating_publish_header_using_CreateStandardHeader_throws_exception()
            {
                FixedHeader fh = FixedHeader.CreateStandardHeader(ControlPacketType.PUBLISH, 0);
            }

            //TODO: test other factory methods

        }

        //Fixed header is used throughout the MQTT standard.  Some testing is included here,
        //but packet-type-specific encoding will be tested by other tests.
        [TestMethod]
        public void Unsubscribe_packet_encoded_correctly()
        {
            FixedHeader fh = FixedHeader.CreateStandardHeader(ControlPacketType.UNSUBSCRIBE, 0);

            byte[] expectedBytes = new byte[] { 0xa2, 0 };
            byte[] fhBytes = fh.Encode().ToArray();
            Assert.AreEqual(expectedBytes.Length, fhBytes.Length);
            Assert.AreEqual(expectedBytes[0], fhBytes[0]);
        }

        [TestMethod]
        public void Subscribe_packet_with_Dup_QoS2_Retain_encoded_correctly()
        {
            FixedHeader fh = FixedHeader.CreatePublishHeader(true, QoSLevel.ExactlyOnce, true, 0);
            byte[] expectedBytes = new byte[] { 0x3d, 0 };
            byte[] fhBytes = fh.Encode().ToArray();
            Assert.AreEqual(expectedBytes.Length, fhBytes.Length);
            Assert.AreEqual(expectedBytes[0], fhBytes[0]);
        }


        [TestMethod]
        public void Verify_MSB_Helper_Function()
        {
            UInt16 packetID = 0xff88;
            Assert.AreEqual(0xff, packetID.MostSignificantByte());

        }

        [TestMethod]
        public void Verify_LSB_Helper_Function()
        {
            UInt16 packetID = 0xff88;
            Assert.AreEqual(0x88, packetID.LeastSignificantByte());
        }



    }
}
