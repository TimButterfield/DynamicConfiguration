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
}