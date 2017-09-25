using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQTTLib;
using System.Linq;

namespace MQTTLib_Test {
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class FixedHeaderTest {
        public FixedHeaderTest() {
            //
            // TODO: Add constructor logic here
            //
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
        [ExpectedException(typeof(ArgumentException))]
        public void CannotCreateStandardPublishHeader() {
            FixedHeader fh = FixedHeader.CreateStandardHeader(FixedHeader.ControlPacketType.PUBLISH);
        }

        [TestMethod]
        public void TestConnectPacketByteValues() {
            FixedHeader fh = FixedHeader.CreateStandardHeader(FixedHeader.ControlPacketType.CONNECT);

            byte[] expectedBytes = new byte[] { 0x10 };
            byte[] fhBytes = fh.ToBytes().ToArray();

            Assert.AreEqual(expectedBytes.Length, fhBytes.Length);
            Assert.AreEqual(expectedBytes[0], fhBytes[0]);
        }

        [TestMethod]
        public void TestUnsubscribePacketByteValues() {
            FixedHeader fh = FixedHeader.CreateStandardHeader(FixedHeader.ControlPacketType.UNSUBSCRIBE);

            byte[] expectedBytes = new byte[] { 0xa2};
            byte[] fhBytes = fh.ToBytes().ToArray();
            Assert.AreEqual(expectedBytes.Length, fhBytes.Length);
            Assert.AreEqual(expectedBytes[0], fhBytes[0]);
        }

        [TestMethod]
        public void SubscribeWithDupQoS2Retain() {
            FixedHeader fh = FixedHeader.CreatePublishHeader(true, QoSLevel.ExactlyOnce, true);
            byte[] expectedBytes = new byte[] { 0x3d };
            byte[] fhBytes = fh.ToBytes().ToArray();
            Assert.AreEqual(expectedBytes.Length, fhBytes.Length);
            Assert.AreEqual(expectedBytes[0], fhBytes[0]);
        }


    }
}
