using System.Collections.Generic;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Unit.ConfigurationParserSpecs
{
    public class when_parsing_configuration
    {
        Establish context = () =>
        {
            configuration = ConfigurationParser.Parse("dynamic.config");
        };

        It then_the_configuration_should_not_be_null = () => ((object)configuration).ShouldNotBeNull();
        It then_the_configuration_should_contain_one_element = () => ((object)configuration.ItemOne).ShouldNotBeNull();
        It then_the_first_element_should_have_an_attribute = () => ((object)configuration.ItemOne.FirstValue).ShouldNotBeNull();
        private It then_the_first_attribute_should_have_a_value = () => configuration.ItemOne.FirstValue.Equals("This is not the real implementation");

        static dynamic configuration; 
    }
}
