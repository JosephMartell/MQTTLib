using Microsoft.VisualStudio.TestTools.UnitTesting;
using MQTTLib;
using MQTTLib.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MQTTLib_Test.Protocol
{
    [TestClass]
    public class EncodedDataFieldTest
    {
        [TestClass]
        public class Encode
        {

            [TestMethod]
            public void Data_length_is_counted_and_encoded_correctly()
            {
                UInt16 expectedDataLength = 52;
                byte[] exampleDataArray = new byte[expectedDataLength];

                EncodedDataField encodedDataField = new EncodedDataField(exampleDataArray);

                Assert.AreEqual(expectedDataLength, encodedDataField.Length);
                Assert.AreEqual(expectedDataLength.MostSignificantByte(), encodedDataField.Encode().First());
                Assert.AreEqual(expectedDataLength.LeastSignificantByte(), encodedDataField.Encode().Skip(1).First());
            }

            [TestMethod]
            public void Encoded_data_bytes_match_source_data()
            {
                UInt16 testDataLength = 30;
                byte[] data = new byte[testDataLength];
                new Random().NextBytes(data);

                IByteEncodable edf = new EncodedDataField(data);
                IEnumerable<byte> encodedData = edf.Encode().Skip(2);

                for (int i = 0; i < data.Count(); i++) {
                    Assert.AreEqual(data[i], encodedData.ToArray()[i]);
                }
            }

            [TestMethod]
            public void Zero_length_data_array_is_accepted_and_encoded()
            {
                byte[] data = new byte[0];
                EncodedDataField encodedEmptyData = new EncodedDataField(data);
                Assert.AreEqual(data.Length, encodedEmptyData.Length);
            }

            [TestMethod]
            public void Max_length_data_array_is_accepted_and_encoded()
            {
                byte[] testData = new byte[65535];
                new Random().NextBytes(testData);
                EncodedDataField encodedMaxLengthData = new EncodedDataField(testData);
                byte[] encodedDataBytes = encodedMaxLengthData.Encode().Skip(2).ToArray();

                Assert.AreEqual(testData.Length, encodedMaxLengthData.Length);
                for (int i = 0; i < testData.Length; i++)
                {
                    Assert.AreEqual(testData[i], encodedDataBytes[i]);
                }
            }
        }
    }
}
