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

        //TODO: re-organize - make packet-type subclasses and test all fields type-by-type
        [TestClass]
        public class ControlPacketTypeTest
        {
            [TestMethod]
            public void Connect_packet_type_is_encoded_correctly()
            {
                FixedHeader connectFixedHeader =
                    FixedHeader.CreateStandardHeader(
                        ControlPacketType.CONNECT,
                        0);

                Assert.AreEqual(ControlPacketType.CONNECT, connectFixedHeader.Type);
                Assert.AreEqual(
                    0x10,
                    connectFixedHeader.Encode().ToArray()[0]);
            }
            [TestMethod]
            public void CONNACK_packet_type_is_encoded_correctly()
            {
                FixedHeader connackFixedHeader =
                    FixedHeader.CreateStandardHeader(
                        ControlPacketType.CONNACK,
                        0);

                Assert.AreEqual(ControlPacketType.CONNACK, connackFixedHeader.Type);
                Assert.AreEqual(
                    0x20,
                    connackFixedHeader.Encode().ToArray()[0]);
            }
            [TestMethod]
            public void PUBLISH_packet_type_is_encoded_correctly()
            {
                FixedHeader publishFixedHeader =
                    FixedHeader.CreatePublishHeader(
                        false, 
                        QoSLevel.AtMostOnce, 
                        false, 
                        0);


                Assert.AreEqual(
                    ControlPacketType.PUBLISH, 
                    publishFixedHeader.Type);

                Assert.AreEqual(
                    0x30,
                    publishFixedHeader.Encode().ToArray()[0]);
            }

            [TestMethod]
            public void PUBACK_packet_type_is_encoded_correctly()
            {
                FixedHeader pubackFixedHeader =
                    FixedHeader.CreateStandardHeader(
                        ControlPacketType.PUBACK,
                        0);

                Assert.AreEqual(ControlPacketType.PUBACK, pubackFixedHeader.Type);
                Assert.AreEqual(
                    0x40,
                    pubackFixedHeader.Encode().ToArray()[0]);
            }

            [TestMethod]
            public void PUBREC_packet_type_is_encoded_correctly()
            {
                FixedHeader pubrecFixedHeader =
                    FixedHeader.CreateStandardHeader(
                        ControlPacketType.PUBREC,
                        0);
                ControlPacketType expectedType = ControlPacketType.PUBREC;

                Assert.AreEqual(expectedType, pubrecFixedHeader.Type);
                Assert.AreEqual(
                    0x50,
                    pubrecFixedHeader.Encode().ToArray()[0]);
            }

            [TestMethod]
            public void PUBREL_packet_type_is_encoded_correctly()
            {
                FixedHeader pubrelFixedHeader =
                    FixedHeader.CreateStandardHeader(
                        ControlPacketType.PUBREL,
                        0);
                ControlPacketType expectedType = ControlPacketType.PUBREL;

                Assert.AreEqual(expectedType, pubrelFixedHeader.Type);
                Assert.AreEqual(
                    0x62,
                    pubrelFixedHeader.Encode().ToArray()[0]);
            }

            [TestMethod]
            public void PUBCOMP_packet_type_is_encoded_correctly()
            {
                FixedHeader pubcompFixedHeader =
                    FixedHeader.CreateStandardHeader(
                        ControlPacketType.PUBCOMP,
                        0);
                ControlPacketType expectedType = ControlPacketType.PUBCOMP;

                Assert.AreEqual(expectedType, pubcompFixedHeader.Type);
                Assert.AreEqual(
                    0x70,
                    pubcompFixedHeader.Encode().ToArray()[0]);
            }

            [TestMethod]
            public void SUBSCRIBE_packet_type_is_encoded_correctly()
            {
                FixedHeader subscribeFixedHeader =
                    FixedHeader.CreateStandardHeader(
                        ControlPacketType.SUBSCRIBE,
                        0);
                ControlPacketType expectedType = ControlPacketType.SUBSCRIBE;

                Assert.AreEqual(expectedType, subscribeFixedHeader.Type);
                Assert.AreEqual(
                    0x82,
                    subscribeFixedHeader.Encode().ToArray()[0]);
            }

            [TestMethod]
            public void SUBACK_packet_type_is_encoded_correctly()
            {
                FixedHeader subackFixedHeader =
                    FixedHeader.CreateStandardHeader(
                        ControlPacketType.SUBACK,
                        0);
                ControlPacketType expectedType = ControlPacketType.SUBACK;

                Assert.AreEqual(expectedType, subackFixedHeader.Type);
                Assert.AreEqual(
                    0x90,
                    subackFixedHeader.Encode().ToArray()[0]);
            }

            [TestMethod]
            public void UNSUBSCRIBE_packet_type_is_encoded_correctly()
            {
                FixedHeader unsubFixedHeader =
                    FixedHeader.CreateStandardHeader(
                        ControlPacketType.UNSUBSCRIBE,
                        0);
                ControlPacketType expectedType = ControlPacketType.UNSUBSCRIBE;

                Assert.AreEqual(expectedType, unsubFixedHeader.Type);
                Assert.AreEqual(
                    0xa2,
                    unsubFixedHeader.Encode().ToArray()[0]);
            }

            [TestMethod]
            public void UNSUBACK_packet_type_is_encoded_correctly()
            {
                FixedHeader unsubackFixedHeader =
                    FixedHeader.CreateStandardHeader(
                        ControlPacketType.UNSUBACK,
                        0);
                ControlPacketType expectedType = ControlPacketType.UNSUBACK;

                Assert.AreEqual(expectedType, unsubackFixedHeader.Type);
                Assert.AreEqual(
                    0xb0,
                    unsubackFixedHeader.Encode().ToArray()[0]);
            }

            [TestMethod]
            public void PINGREQ_packet_type_is_encoded_correctly()
            {
                FixedHeader pingreqFixedHeader =
                    FixedHeader.CreateStandardHeader(
                        ControlPacketType.PINGREQ,
                        0);
                ControlPacketType expectedType = ControlPacketType.PINGREQ;

                Assert.AreEqual(expectedType, pingreqFixedHeader.Type);
                Assert.AreEqual(
                    0xc0,
                    pingreqFixedHeader.Encode().ToArray()[0]);
            }

            [TestMethod]
            public void PINGRESP_packet_type_is_encoded_correctly()
            {
                FixedHeader pingrespFixedHeader =
                    FixedHeader.CreateStandardHeader(
                        ControlPacketType.PINGRESP,
                        0);
                ControlPacketType expectedType = ControlPacketType.PINGRESP;

                Assert.AreEqual(expectedType, pingrespFixedHeader.Type);
                Assert.AreEqual(
                    0xd0,
                    pingrespFixedHeader.Encode().ToArray()[0]);
            }

            [TestMethod]
            public void DISCONNECT_packet_type_is_encoded_correctly()
            {
                FixedHeader disconnectFixedHeader =
                    FixedHeader.CreateStandardHeader(
                        ControlPacketType.DISCONNECT,
                        0);
                ControlPacketType expectedType = ControlPacketType.DISCONNECT;

                Assert.AreEqual(expectedType, disconnectFixedHeader.Type);
                Assert.AreEqual(
                    0xe0,
                    disconnectFixedHeader.Encode().ToArray()[0]);
            }

        }
        [TestMethod]
        public void TestConnectPacketByteValues()
        {
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
        public void TestUnsubscribePacketByteValues()
        {
            FixedHeader fh = FixedHeader.CreateStandardHeader(ControlPacketType.UNSUBSCRIBE, 0);

            byte[] expectedBytes = new byte[] { 0xa2, 0 };
            byte[] fhBytes = fh.Encode().ToArray();
            Assert.AreEqual(expectedBytes.Length, fhBytes.Length);
            Assert.AreEqual(expectedBytes[0], fhBytes[0]);
        }

        [TestMethod]
        public void SubscribeWithDupQoS2Retain()
        {
            FixedHeader fh = FixedHeader.CreatePublishHeader(true, QoSLevel.ExactlyOnce, true, 0);
            byte[] expectedBytes = new byte[] { 0x3d, 0 };
            byte[] fhBytes = fh.Encode().ToArray();
            Assert.AreEqual(expectedBytes.Length, fhBytes.Length);
            Assert.AreEqual(expectedBytes[0], fhBytes[0]);
        }


        [TestMethod]
        public void MSB()
        {
            UInt16 packetID = 0xff88;
            Assert.AreEqual(0xff, packetID.MostSignificantByte());

        }

        public void LSB()
        {
            UInt16 packetID = 0xff88;
            Assert.AreEqual(0x88, packetID.LeastSignificantByte());
        }
    }
}
