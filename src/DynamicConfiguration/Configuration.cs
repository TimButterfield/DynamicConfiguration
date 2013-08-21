using System;
using System.Collections.Concurrent;
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
}