using System;
using MQTTLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace MQTTLib_Test {
    [TestClass]
    public class EncodedRemainingLengthTest {
        [TestMethod]
        public void Test1ByteMaxValueEncode() {
            EncodedRemainingLength erl = new EncodedRemainingLength(127);
            var bytes = erl.Encode().ToArray();
            Assert.IsTrue(bytes.Length == 1);
            Assert.IsTrue(bytes[0] == 0x7F);
        }
        [TestMethod]
        public void Test2ByteMinValueEncode() {
            EncodedRemainingLength erl = new EncodedRemainingLength(128);
            var bytes = erl.Encode().ToArray();
            Assert.IsTrue(bytes.Length == 2);
            Assert.IsTrue(bytes[0] == 0x80);
            Assert.IsTrue(bytes[1] == 0x01);
        }
        [TestMethod]
        public void Test2ByteMaxValueEncode() {
            EncodedRemainingLength erl = new EncodedRemainingLength(16383);
            var bytes = erl.Encode().ToArray();
            Assert.IsTrue(bytes.Length == 2);
            Assert.IsTrue(bytes[0] == 0xFF);
            Assert.IsTrue(bytes[1] == 0x7F);
        }
        [TestMethod]
        public void Test3ByteMinValueEncode() {
            EncodedRemainingLength erl = new EncodedRemainingLength(16384);
            var bytes = erl.Encode().ToArray();
            Assert.IsTrue(bytes.Length == 3);
            Assert.IsTrue(bytes[0] == 0x80);
            Assert.IsTrue(bytes[1] == 0x80);
            Assert.IsTrue(bytes[2] == 0x01);
        }
        [TestMethod]
        public void Test3ByteMaxValueEncode() {
            EncodedRemainingLength erl = new EncodedRemainingLength(2097151);
            var bytes = erl.Encode().ToArray();
            Assert.IsTrue(bytes.Length == 3);
            Assert.IsTrue(bytes[0] == 0xFF);
            Assert.IsTrue(bytes[1] == 0xFF);
            Assert.IsTrue(bytes[2] == 0x7F);
        }
        [TestMethod]
        public void Test4ByteMinValueEncode() {
            EncodedRemainingLength erl = new EncodedRemainingLength(2097152);
            var bytes = erl.Encode().ToArray();
            Assert.IsTrue(bytes.Length == 4);
            Assert.IsTrue(bytes[0] == 0x80);
            Assert.IsTrue(bytes[1] == 0x80);
            Assert.IsTrue(bytes[2] == 0x80);
            Assert.IsTrue(bytes[3] == 0x01);
        }
        [TestMethod]
        public void Test4ByteMaxValueEncode() {
            EncodedRemainingLength erl = new EncodedRemainingLength(268435455);
            var bytes = erl.Encode().ToArray();
            Assert.IsTrue(bytes.Length == 4);
            Assert.IsTrue(bytes[0] == 0xFF);
            Assert.IsTrue(bytes[1] == 0xFF);
            Assert.IsTrue(bytes[2] == 0xFF);
            Assert.IsTrue(bytes[3] == 0x7F);
        }
        [TestMethod]
        public void Test1ByteMaxValueDecode() {
            byte[] bytes = new byte[] { 0x7F };
            EncodedRemainingLength erl = new EncodedRemainingLength(bytes);
            Assert.AreEqual((UInt32)127, erl.Length);
        }
        [TestMethod]
        public void Test2ByteMinValueDecode() {
            byte[] bytes = new byte[] { 0x80, 0x01 };
            EncodedRemainingLength erl = new EncodedRemainingLength(bytes);
            Assert.AreEqual((UInt32)128, erl.Length);
        }
        [TestMethod]
        public void Test2ByteMaxValueDecode() {
            byte[] bytes = new byte[] { 0xFF, 0x7F };
            EncodedRemainingLength erl = new EncodedRemainingLength(bytes);
            Assert.AreEqual((UInt32)16383, erl.Length);
        }
    }
}
