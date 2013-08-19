using System;
using System.Runtime.Serialization;

namespace DynamicConfiguration.Exceptions
{
    internal class InvalidXmlException : Exception
    {
        public InvalidXmlException()
        {
        }

        public InvalidXmlException(string message) : base(message)
        {
        }

        public InvalidXmlException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidXmlException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}