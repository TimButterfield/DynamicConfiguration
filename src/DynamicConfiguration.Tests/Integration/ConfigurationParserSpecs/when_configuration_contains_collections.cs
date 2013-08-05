using DynamicConfiguration.Parser;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Integration.ConfigurationParserSpecs
{
    public class when_configuration_contains_collections
    {
        public class then_collections_can_be_parsed_correctly
        {
            private Establish context = () =>
                {
                    var configuration = ConfigurationParser.Parse();
                };
        }
    }
}