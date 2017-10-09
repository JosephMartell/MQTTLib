using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQTTLib;
using System.Net.Sockets;

namespace MQTTLib_Test {
    /// <summary>
    /// Summary description for MQTTClientTest
    /// </summary>
    [TestClass]
    public class MQTTClientTest {
        public MQTTClientTest() {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ClientConnectStandardExample() {
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            Client c = new Client(memStream, "Figure 3.6");
            Will w = new Will(MQTTLib.Protocol.QoSLevel.AtLeastOnce,false, "I died!", "But did I ever really live?");
            c.Connect(true, w, new Authentication("username", "password"), 10);
            byte[] receivedPacket = memStream.GetBuffer();

            //Verify fixed header
            Assert.AreEqual(0x10, receivedPacket[0]);
            //length is unknown right now
            Assert.AreEqual(0x00, receivedPacket[2]);
            Assert.AreEqual(0x04, receivedPacket[3]);
            Assert.AreEqual(0x4d, receivedPacket[4]);
            Assert.AreEqual(0x51, receivedPacket[5]);
            Assert.AreEqual(0x54, receivedPacket[6]);
            Assert.AreEqual(0x54, receivedPacket[7]);
            Assert.AreEqual(0x04, receivedPacket[8]);
            Assert.AreEqual(0xce, receivedPacket[9]);
            Assert.AreEqual(0x00, receivedPacket[10]);
            Assert.AreEqual(0x0a, receivedPacket[11]);

        }
    }
}
