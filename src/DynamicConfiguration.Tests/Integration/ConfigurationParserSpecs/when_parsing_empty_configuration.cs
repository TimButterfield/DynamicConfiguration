using System;
using DynamicConfiguration.Parser;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Integration.ConfigurationParserSpecs
{
    public class when_parsing_empty_configuration
    {
        Because of_empty_configuration = () => exception = Catch.Exception(() => ConfigurationParser.Parse("Empty.config"));

        It exception_should_be_thrown = () => exception.Message.ShouldEqual("Root element is missing.");

        private static dynamic configuration;
        private static Exception exception;
    }
}