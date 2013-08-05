using DynamicConfiguration.Parser;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Integration.ConfigurationParserSpecs
{
    public class when_getting_first_item_in_configuration
    {
        Establish parse_config = () => ParseConfiguration();

        Because and_then_get_first_item = () => { _itemOne = _configuration.ItemOne; };

        It then_it_should_match_expected_value = () => _itemOne.FirstValue.ToString().Equals(1);
        
        private static dynamic _configuration;
        private static dynamic _itemOne; 

        private static void ParseConfiguration()
        {
            _configuration = ConfigurationParser.Parse(); 
        }
    }
}