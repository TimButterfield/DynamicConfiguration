using System;
using System.IO;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Unit.ConfigurationParserSpecs
{
    public class when_trying_to_parse_a_none_existent_configuration_file
    {
        private Because of = () => Exception = Catch.Exception(() => ConfigurationParser.Parse("blah.config"));
        private It then_the_parser_should_throw_an_exception = () => Exception.ShouldBeOfType<FileNotFoundException>(); 
        static Exception Exception; 
    }
}