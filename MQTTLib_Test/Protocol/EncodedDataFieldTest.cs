using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQTTLib.Protocol;
using MQTTLib;
using System.Linq;
using System.Collections.Generic;

namespace MQTTLib_Test.Protocol {
    [TestClass]
    public class EncodedDataFieldTest {
        [TestClass]
        public class Encode {

            [TestMethod]
            public void LengthOfData() {
                UInt16 dataLength = 52;
                byte[] data = new byte[dataLength];

                IByteEncodable edf = new EncodedDataField(data);

                Assert.AreEqual(dataLength.MostSignificantByte(), edf.Encode().First());
                Assert.AreEqual(dataLength.LeastSignificantByte(), edf.Encode().Skip(1).First());
            }

            [TestMethod]
            public void Data() {
                UInt16 dataLength = 52;
                byte[] data = new byte[dataLength];

                IByteEncodable edf = new EncodedDataField(data);

                IEnumerable<byte> encodedData = edf.Encode().Skip(2);

                for (int i = 0; i < data.Count(); i++) {
                    Assert.AreEqual(data[i], encodedData.ToArray()[i]);
                }
            }
        }
    }
}
