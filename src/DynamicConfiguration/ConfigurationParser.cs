using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Reflection;

namespace DynamicConfiguration
{
    public class ConfigurationParser
    {
        public static dynamic Parse(string configurationPath = @"~/Dynamic.config")
        {
            dynamic configuration = new ExpandoObject();
            dynamic item = new ExpandoObject();

            if (!File.Exists(configurationPath))
                throw new FileNotFoundException(string.Format("Could not locate dynamic configuration {0}", configurationPath));

            var itemDictionary = (IDictionary<string, object>)item;
            var configurationDictionary = (IDictionary<string, object>) configuration; 
            itemDictionary.Add("FirstValue", "This is not the real implementation");

            configurationDictionary.Add("ItemOne", itemDictionary);

            return configuration; 
        }
    }
}