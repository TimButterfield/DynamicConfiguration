using Machine.Specifications;

namespace DynamicConfiguration.Tests.Integration.ConfigurationParserSpecs
{
    public class when_getting_all_items_in_configruation : ConfigDepenantFixture
    {
        Establish parse_config = () => ParseConfiguration();
        Because and_then_get_first_item = () =>
            {
                _itemOne = _configuration.ItemOne;
                _itemTwo = _configuration.ItemTwo;
            };

        It the_first_item_first_value_should_match_expectation = () => _itemOne.FirstValue.ToString().Equals(1);
        It the_first_item_second_value_should_match_expectation = () => _itemOne.SecondValue.ToString().Equals(2);


        It the_second_item_first_value_should_match_expectation = () => _itemTwo.FirstValue.ToString().Equals(1);
        It the_second_item_second_value_should_match_expectation = () => _itemTwo.SecondValue.ToString().Equals(2);

        private static dynamic _configuration;
        private static dynamic _itemOne;
        private static dynamic _itemTwo;

        private static void ParseConfiguration()
        {
            _configuration = ConfigurationParser.Parse();
        }
    }
}