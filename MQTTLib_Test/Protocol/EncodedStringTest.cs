using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using MQTTLib.Protocol;
using MQTTLib;
using System.Collections.Generic;

namespace MQTTLib_Test.Protocol {
    [TestClass]
    public class EncodedStringTest {
        private UTF8Encoding utf8 = new UTF8Encoding();

        [TestMethod]
        public void ZeroLengthEncoded() {
            EncodedString es = new EncodedString(string.Empty);
            Assert.AreEqual(0x00, es.Encode().First());
            Assert.AreEqual(0x00, es.Encode().Skip(1).First());
            Assert.AreEqual(2, es.Encode().Count());
        }

        [TestMethod]
        public void MaxSpecLengthEncoded() {
            //              12345678901234567890123
            string length23Test = "Test 1 length to be: 23";
            UInt16 MaxSpecLength = (UInt16) length23Test.Length;
            IByteEncodable es = new EncodedString(length23Test);
            byte[] esBytes = es.Encode().ToArray();
            Assert.AreEqual(23, MaxSpecLength);
            Assert.AreEqual(MaxSpecLength.MostSignificantByte(), esBytes[0]);
            Assert.AreEqual(MaxSpecLength.LeastSignificantByte(), esBytes[1]);
        }

        [TestMethod]
        public void ExtendedLengthSupported() {
            string extendedLengthString = new string('x', 1000);
            UInt16 expectedLength = (UInt16) extendedLengthString.Length;
            byte[] esBytes = new EncodedString(extendedLengthString).Encode().ToArray();
            Assert.AreEqual(expectedLength.MostSignificantByte(), esBytes[0]);
            Assert.AreEqual(expectedLength.LeastSignificantByte(), esBytes[1]);
        }

        [TestMethod]
        public void SpecCharsEncoded() {
            //                    12345678901234567890123
            string encodeTest1 = "abcdefghijklmnopqrstuvw";
            string encodeTest2 = "xyzABCDEFGHIJKLMNOPQRST";
            string encodeTest3 = "UVWXYZ0123456789";

            IByteEncodable set1 = new EncodedString(encodeTest1);
            IByteEncodable set2 = new EncodedString(encodeTest2);
            IByteEncodable set3 = new EncodedString(encodeTest3);

            string decodedTest1 = utf8.GetString(set1.Encode().ToArray().Skip(2).ToArray());
            string decodedTest2 = utf8.GetString(set2.Encode().ToArray().Skip(2).ToArray());
            string decodedTest3 = utf8.GetString(set3.Encode().ToArray().Skip(2).ToArray());

            Assert.AreEqual(encodeTest1, decodedTest1);
            Assert.AreEqual(encodeTest2, decodedTest2);
            Assert.AreEqual(encodeTest3, decodedTest3);
        }

        [TestMethod]
        public void DecodeByteArray() {
            string testString1 = "This is a test string to be encoded";
            List<byte> expectedBytes = new List<byte>();
            expectedBytes.Add(((UInt16)testString1.Length).MostSignificantByte());
            expectedBytes.Add(((UInt16)testString1.Length).LeastSignificantByte());
            expectedBytes.AddRange(utf8.GetBytes(testString1));


            EncodedString es = new EncodedString(expectedBytes);
            Assert.AreEqual(testString1, es.Value);
            Assert.IsTrue(new EncodedString(testString1) == es);
        }

    }
}
