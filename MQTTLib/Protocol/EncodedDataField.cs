using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib.Protocol {
    public class EncodedDataField : IByteEncodable {

        public UInt16 Length
        {
            get { return (UInt16)Data.Count(); }
        }

        public IEnumerable<byte> Data { get; }

        public EncodedDataField(IEnumerable<byte> data) {
            Data = data;
        }

        public EncodedDataField(Stream byteStream)
        {
            byte[] lengthBytes = new byte[2];
            byteStream.Read(lengthBytes, 0, 2);

            UInt16 length = (UInt16)lengthBytes[0];
            length <<= 8;
            length |= lengthBytes[1];

            byte[] dataBuffer = new byte[length];
            byteStream.Read(dataBuffer, 0, length);
            Data = dataBuffer;
        }

        public IEnumerable<byte> Encode() {

            UInt16 length = (UInt16)Data.Count();
            return new byte[] { length.MostSignificantByte(), length.LeastSignificantByte() }.Concat(Data);
        }

        public static implicit operator byte[](EncodedDataField edf) {
            return edf.Data.ToArray();
        }
    }
}
