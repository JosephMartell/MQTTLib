using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MQTTLib;

namespace MQTTLib.Protocol
{

    public class EncodedString : 
        EncodedDataField, 
        IByteEncodable, 
        IEquatable<EncodedString>, 
        IComparable<EncodedString>
    {
        public string Value { get; }

        public EncodedString(string value)
            : base(new UTF8Encoding().GetBytes(value))
        {
            Value = value;
        }

        public EncodedString(Stream byteStream)
            : base(byteStream)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            Value = utf8.GetString(Data.ToArray());
        }

        public bool Equals(EncodedString other)
        {
            return Value.Equals(other);
        }

        public int CompareTo(EncodedString other)
        {
            return (Value.CompareTo(other));
        }

        public static bool operator ==(EncodedString original, EncodedString other)
        {
            return (original.Value == other.Value) &&
                   (original.Length == other.Length);
        }

        public static bool operator != (EncodedString original, EncodedString other)
        {
            return !(original == other);
        }

        public override bool Equals(object obj)
        {
            return Value.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(EncodedString s)
        {
            return s.Value;
        }

        public static implicit operator EncodedString(string s)
        {
            return new EncodedString(s);
        }
    }
}
