using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQTTLib.Protocol
{
    public class PublishVariableHeader :
        VariableHeader
    {
        public EncodedString TopicName { get; }

        public PublishVariableHeader(EncodedString topicName)
        {
            if (topicName.Value.Contains("+") ||
                topicName.Value.Contains("#"))
            {
                throw new ArgumentException("Topic names cannot contain wildcard characters in Publish messages");
            }

            if (topicName.Value.StartsWith("$"))
            {
                throw new ArgumentException("Client topic names cannot start with a $. Only server message topics can start with $");
            }
            TopicName = topicName;
        }

        public override IEnumerable<byte> Encode()
        {
            return TopicName.Encode();
        }
    }
}
