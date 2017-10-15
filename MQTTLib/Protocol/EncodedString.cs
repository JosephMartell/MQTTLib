using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTLib;

namespace MQTTLib.Protocol {

    //TODO: Should this be a subclass of EncodedDataField? (Probably?)
    public class EncodedString : IByteEncodable, IEquatable<EncodedString>, IComparable<EncodedString> {
        public UInt16 Length {
            get {
                return (UInt16)Value.Length;
            }
        }
        public string Value { get; }
        public EncodedString(string value) {
            Value = value;
        }

        public EncodedString(IEnumerable<byte> bytes) {
            UInt16 length = 0;
            length = bytes.First();
            length <<= 8;
            length |= bytes.Skip(1).First();

            UTF8Encoding utf8 = new UTF8Encoding();
            Value = utf8.GetString(bytes.Skip(2).Take(length).ToArray());

        }

        public IEnumerable<byte> Encode() {
            UTF8Encoding utf8 = new UTF8Encoding();
            List<byte> encodedBytes = new List<byte>();
            encodedBytes.Add(Length.MostSignificantByte());
            encodedBytes.Add(Length.LeastSignificantByte());
            encodedBytes.AddRange(utf8.GetBytes(Value));
            return encodedBytes;
        }

        public bool Equals(EncodedString other) {
            return Value.Equals(other);
        }

        public int CompareTo(EncodedString other) {
            return (Value.CompareTo(other));
        }

        public static bool operator ==(EncodedString original, EncodedString other) {
            return (original.Value == other.Value) &&
                   (original.Length == other.Length);
        }

        public static bool operator != (EncodedString original, EncodedString other) {
            return !(original == other);
        }

        public override bool Equals(object obj) {
            return Value.Equals(obj);
        }

        public override int GetHashCode() {
            return Value.GetHashCode();
        }

        public override string ToString() {
            return Value;
        }

        public static implicit operator string(EncodedString s) {
            return s.Value;
        }

        public static implicit operator EncodedString(string s) {
            return new EncodedString(s);
        }
    }
}
