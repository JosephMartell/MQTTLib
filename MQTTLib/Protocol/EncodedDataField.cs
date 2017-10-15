using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib.Protocol {
    public class EncodedDataField : IByteEncodable {
        public IEnumerable<byte> Data { get; }

        public EncodedDataField(IEnumerable<byte> data) {
            Data = data;
        }

        public EncodedDataField(UInt16 length, IEnumerable<byte> data) {
            Data = data.Take(length);
        }

        public IEnumerable<byte> Encode() {

            UInt16 length = (UInt16)Data.Count();
            return new byte[] { length.MostSignificantByte(), length.LeastSignificantByte() }.Concat(Data);
        }

        public static implicit operator EncodedDataField(byte[] data) {
            return new EncodedDataField(data);
        }

        public static implicit operator byte[](EncodedDataField edf) {
            return edf.Data.ToArray();
        }
    }
}
