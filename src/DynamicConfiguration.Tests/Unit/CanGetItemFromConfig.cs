using Machine.Specifications;

namespace DynamicConfiguration.Tests.Unit
{
    public class CanGetItemFromConfig
    {
        private readonly Establish parse_config = () => ParseConfiguration();

        private readonly Because and_then_get_first_item = () => { ItemOne = Configuration.ItemOne}; 

        private It should_get_the_expected_value = () => ItemOne.FirstValue.ShouldNotBeNull();
        
        private static dynamic Configuration;
        private static dynamic ItemOne; 

        private static void ParseConfiguration()
        {
            throw new System.NotImplementedException();
        }
    }
}