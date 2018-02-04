using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTLib;
using MQTTLib.Protocol;

namespace MQTTLib_Test.Protocol {

    //TODO: Publish Variable Header Testing
    [TestClass]
    public class PublishVariableHeaderTest
    {
        [TestClass]
        public class TopicName
        {
            [TestMethod]
            public void Topic_is_encoded_as_first_field()
            {
                EncodedString topicName = "Test/topic";
                PublishVariableHeader variableHeader = new PublishVariableHeader(topicName);
                byte[] encodedTopicNameBytes = variableHeader
                    .Encode()
                    .Take(
                    topicName.Encode().Count()).ToArray();
                for (int i = 0; i < topicName.Encode().Count(); i++)
                {
                    Assert.AreEqual(topicName.Encode().ToArray()[i], encodedTopicNameBytes[i]);
                }
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Throw_exception_if_wildcards_are_used_in_topic_name()
            {
                EncodedString badTopicName = "Wildcards/+/arent/allowed/#";
                PublishVariableHeader throwExceptionHere = new PublishVariableHeader(badTopicName);
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentException))]
            public void Throw_exception_if_topic_name_starts_with_dollar_sign()
            {
                EncodedString topicNameForSystemMsgs = "$This/Is/Not/Allowed";
                PublishVariableHeader throwExceptionHere = new PublishVariableHeader(topicNameForSystemMsgs);
            }
        }

    }
}
