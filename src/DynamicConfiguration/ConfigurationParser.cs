using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Xml.Linq;

namespace DynamicConfiguration
{
    public class ConfigurationParser
    {
        private static IDictionary<string, object> _dictionary;


        public static dynamic Parse(string configurationPath = @"dynamic.config")
        {
            if (!File.Exists(configurationPath))
                throw new FileNotFoundException(string.Format("Could not locate dynamic configuration {0}", configurationPath));

            //use linq to xml to get all items under <easy>
            var configuration = XDocument.Load(configurationPath);
            var rootElement = configuration.Root;

            return rootElement != null ? GetElements(rootElement.Elements()) : null;
        }

        private static dynamic GetElements(IEnumerable<XElement> elements, dynamic easyConfig = null)
        {
            foreach (var element in elements)
            {
                if (easyConfig == null)
                {
                    easyConfig = new ExpandoObject();
                    _dictionary = (IDictionary<string, object>)easyConfig;
                }

                if (element.HasAttributes)
                {
                    dynamic item = new ExpandoObject();
                    var localDictionary = (IDictionary<string, object>)item;

                    foreach (var attribute in element.Attributes())
                        localDictionary.Add(attribute.Name.ToString(), attribute.Value);
                    
                    _dictionary.Add(element.Name.ToString(), localDictionary);
                }

                if (element.HasElements)
                    GetElements(element.Elements(), easyConfig);
            }

            return easyConfig;
        }
    }
}