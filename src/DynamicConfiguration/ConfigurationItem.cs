using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

using DynamicConfiguration.Exceptions;

namespace DynamicConfiguration
{
    public class ConfigurationItem : DynamicObject, IEnumerable<ConfigurationItem>
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

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var matchingElements = _configurationItems.Elements().Where(x => x.Name == binder.Name).ToArray();

            if (!matchingElements.Any())
            {
                object matchedValue;

                if (TryToFindAttribute(binder, out matchedValue))
                {
                    result = matchedValue;
                    return true;
                }

                throw new ConfigurationItemNotFoundException(string.Format("Configuration item {0} could not be found", binder.Name));
            }
            

             result = new ConfigurationItem(matchingElements);
             return true;
            
        }

        bool TryToFindAttribute(GetMemberBinder binder, out object result)
        {
            if (FindValue(binder.Name, out result))
            {
                return true; 
            }
            
            result = null; 
            return false; 
        }

        private bool TryToFindAttribute(InvokeMemberBinder binder, out object result)
        {
            if (FindValue(binder.Name, out result))
            {
                return true;
            }

            result = null;
            return false;   
        }

        bool FindValue(string name, out object result)
        {
            var attributeName = name; 
            
            if (name.Contains("Find"))
            {
                attributeName = name.Substring("Find".Length);
            }
            
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

                //HERE YOU NEED TO LOOK AT THE TYPE-> And Whether you need to cast
                result = match.Value;
                return true;
            }

            result = null;
            return false; 
        }

        public XElement FirstOrDefault()
        {
            return _configurationItems.FirstOrDefault(); 
        }

        public XElement FirstOrDefault(Func<XElement, bool> predicate)
        {
            return _configurationItems.FirstOrDefault(predicate); 
        }

        public XElement ToXmlElement()
        {
            if (_configurationItems.Count() == 1)
                return _configurationItems.First();

            return null;
        }

        public IEnumerator<ConfigurationItem> GetEnumerator()
        {
            if (_configurationItems.Count() == 1 && _configurationItems.Elements().Any())
            {
                foreach (var element in _configurationItems.Elements())
                {
                    yield return new ConfigurationItem(new List<XElement> {element});
                }
            }
            else
            {
                yield return new ConfigurationItem(new[] { _configurationItems.FirstOrDefault() }); 
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}