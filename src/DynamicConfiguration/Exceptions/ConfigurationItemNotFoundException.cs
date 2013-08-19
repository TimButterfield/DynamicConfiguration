using System;
using System.Runtime.Serialization;

namespace DynamicConfiguration.Exceptions
{
    public class ConfigurationItemNotFoundException : Exception
    {
        public ConfigurationItemNotFoundException()
        {
        }

        public ConfigurationItemNotFoundException(string message) : base(message)
        {
        }

        public ConfigurationItemNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConfigurationItemNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}