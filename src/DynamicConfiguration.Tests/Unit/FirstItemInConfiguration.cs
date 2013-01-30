using System.Collections.Generic;
using System.Dynamic;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Unit
{
    public class FirstItemInConfiguration
    {
        Establish parse_config = () => ParseConfiguration();

        Because and_then_get_first_item = () => { ItemOne = Configuration.ItemOne; };

        It should_match_expected_string = () => ItemOne.FirstValue.ToString().Equals("This is not the real implementation");
        
        private static dynamic Configuration;
        private static dynamic ItemOne; 

        private static void ParseConfiguration()
        {
            Configuration = ConfigurationParser.Parse(); 
        }
    }

    internal class ConfigurationParser
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