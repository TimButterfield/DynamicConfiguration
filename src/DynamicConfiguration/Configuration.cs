using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace DynamicConfiguration
{
    public class Configuration : DynamicObject
    {
        private readonly ConcurrentDictionary<string, dynamic> _members = new ConcurrentDictionary<string, dynamic>();
        private XDocument _configuration;

        public Configuration(string configurationFilePath = @"dynamic.config")
        {
            var configurationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configurationFilePath);

            if (!File.Exists(configurationPath))
                throw new FileNotFoundException(string.Format("Could not locate dynamic configuration {0}", configurationPath));

            _configuration = XDocument.Load(configurationPath);
        }


//        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
//        {
//            return base.TryInvokeMember(binder, args, out result);
//        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var rootElement = _configuration.Root;
            ValidateRoot(rootElement);
            dynamic configurationItem;

            if (rootElement != null)
            {
                var element = rootElement.Elements().Where(x => x.Name == binder.Name); 
                if (!element.Any())
                    throw new ConfigurationItemNotFoundException(string.Format("Configuration item {0} could not be found", binder.Name));

//            foreach ()
//            {
////                if (element.)
////                if (element.HasAttributes)
////                {
////                    foreach (var attribute in element.Attributes())
////                        if (attribute.Name.ToString(), GetValue(attribute));
////
////                    _dictionary.Add(element.Name.ToString(), localDictionary);
////                }
////
////                if (element.HasElements)
////                    GetElements(element.Elements(), easyConfig);
//            }

                result = element;
                return true;
            }
            result = null; 
            return false;
        }

        private static void ValidateRoot(XElement rootElement)
        {
            if (rootElement == null)
                throw new ArgumentNullException("rootElement");

            if (rootElement.Name != "dynamic")
                throw new Exception("root element must be dynamic");
        }
        
        
    }

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