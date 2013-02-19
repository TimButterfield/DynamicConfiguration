using System;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Integration.ConfigurationParserSpecs
{
    public class when_config_root_element_is_not_dynamic : ConfigDepenantFixture
    {
        private Because no_dynamic_root_exists = () => _exception = Catch.Exception(() => ConfigurationParser.Parse("ConfigWithMissingDynamicRoot.config"));

        private It should_throw_an_exception = () =>
            {
                _exception.ShouldBeOfType<Exception>();
                _exception.Message.ShouldEqual("root element must be dynamic");
            };

        static Exception _exception; 
    }
}