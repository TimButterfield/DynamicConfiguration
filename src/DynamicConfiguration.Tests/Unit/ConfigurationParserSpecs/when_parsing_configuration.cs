using System.Collections.Generic;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Unit.ConfigurationParserSpecs
{
    public class when_parsing_configuration
    {
        Establish context = () =>
        {
            configurationElements = ConfigurationParser.Parse("dynamic.config");
        };

        It then_it_should_not_be_null = () => ((IDictionary<string, object>)configurationElements).ShouldNotBeNull();
        It then_it_contain_an_expected_element = () => configurationElements.ItemOne.FirstValue.Equals("This is not the real implementation"); 

        static dynamic configurationElements; 
    }
}