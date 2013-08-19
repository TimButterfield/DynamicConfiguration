using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using DynamicConfiguration.Exceptions;

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

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var rootElement = _configuration.Root;
            ValidateRoot(rootElement);
            
            if (rootElement != null)
            {
                var matchingElements = rootElement.Elements().Where(x => x.Name == binder.Name).ToArray();

                if (!matchingElements.Any())
                    throw new ConfigurationItemNotFoundException(string.Format("Configuration item {0} could not be found", binder.Name));

                result = new ConfigurationItem(matchingElements);
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

    public class ConfigurationItem : DynamicObject
    {
        private readonly IEnumerable<XElement> _configurationItems;

        internal ConfigurationItem(IEnumerable<XElement> configurationItems)
        {
            _configurationItems = configurationItems;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            object matchedValue; 

            if (binder.Name.StartsWith("Find"))
                if (TryToFindAttribute(binder, out matchedValue))
                {
                    result = matchedValue;
                    return true;
                }


            result = null;
            return false; 
        }

        private bool TryToFindAttribute(InvokeMemberBinder binder, out object result)
        {
            var attributeName = binder.Name.Substring("Find".Length);

            foreach (var element in _configurationItems)
            {
                var matchingAttributes = element.Attributes(attributeName);

                var xAttributes = matchingAttributes as XAttribute[] ?? matchingAttributes.ToArray();

                if (xAttributes.Count() > 1)
                    throw new InvalidXmlException(
                        string.Format("More than one attribute found with the name {0} in XmlElement with name {1}",
                                      attributeName, element.Name));

                var match = xAttributes.FirstOrDefault();
                if (match == null)
                {
                    result = null;
                    return false;
                }
                
                result = match.Value;
                return true;
            }

            result = null; 
            return false; 
        }
    }
}