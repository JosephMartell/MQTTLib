using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQTTLib;
using MQTTLib.Protocol;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace MQTTLib_Test {
    [TestClass]
    public class AuthenticationTest {
        [TestMethod]
        public void TestUsernameOnlyEncode() {
            Authentication auth = new Authentication("JonDoe");
            byte[] authBytes = auth.Encode().ToArray();

            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] expectedBytes = utf8.GetBytes("JonDoe");

            Assert.AreEqual(expectedBytes.Length, authBytes.Length);

            for (int i = 0; i < expectedBytes.Length; i++) {
                Assert.AreEqual(expectedBytes[i], authBytes[i]);
            }
        }

        [TestMethod]
        public void TestUsernameAndPwordEncode() {
            string username = "JonDoe";
            string password = "passwordtest";
            UTF8Encoding utf8 = new UTF8Encoding();
            Authentication auth = new Authentication(username, password);
            EncodedRemainingLength erl = new EncodedRemainingLength((uint)password.Length);

            List<byte> expectedBytes = new List<byte>();

            expectedBytes.AddRange(utf8.GetBytes(username));
            expectedBytes.AddRange(erl.Encode());
            expectedBytes.AddRange(utf8.GetBytes(password));

            Assert.AreEqual(expectedBytes.Count, auth.Encode().Count());

            for (int i = 0; i < expectedBytes.Count; i++) {
                Assert.AreEqual(expectedBytes[i], auth.Encode().ToArray()[i]);
            }

            byte[] passwordBytes = auth.Encode().Skip(username.Length).Skip(erl.Encode().Count()).ToArray();
            string decodedPassword = utf8.GetString(passwordBytes);

            Assert.AreEqual(password, decodedPassword);
        }
    }
}
