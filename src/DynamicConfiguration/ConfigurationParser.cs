using System.Collections.Generic;
using System.Dynamic;

namespace DynamicConfiguration
{
    public class ConfigurationParser
    {
        public static dynamic Parse(string configurationPath = "")
        {
            dynamic configuration = new ExpandoObject();
            dynamic item = new ExpandoObject();

            
            var itemDictionary = (IDictionary<string, object>)item;
            var configurationDictionary = (IDictionary<string, object>) configuration; 
            itemDictionary.Add("FirstValue", "This is not the real implementation");


            configurationDictionary.Add("ItemOne", itemDictionary);

            return configuration; 
        }
    }
}