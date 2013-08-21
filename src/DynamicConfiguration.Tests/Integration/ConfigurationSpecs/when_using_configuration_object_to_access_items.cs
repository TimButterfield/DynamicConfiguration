using System;
using System.Linq;
using System.Xml.Linq;
using DynamicConfiguration.Exceptions;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Integration.ConfigurationSpecs
{
    [Subject(typeof(Configuration))]
    public class when_using_configuration_object_to_access_items 
    {
        protected Establish Context = () => Configuration = new Configuration();
        protected static dynamic Configuration;

        public class when_configuration_setting_does_not_exist
        {
            private Because configuration_items_do_not_exist = () => _exception = Catch.Exception(() => Configuration.AConfigurationItemThatDoesNotExist.FindSomething() );

            private It should_throw_a_runtime_exception = () => _exception.ShouldBeOfType<ConfigurationItemNotFoundException>();
                
            private static Exception _exception;
        }

        public class when_known_configuration_settings_do_exist
        {
            private Because known_configuration_items_do_exist = () =>
                {
                    _itemFour = Configuration.ItemFour;
                    _itemThree = Configuration.ItemThree; 
                };
            private It should_find_a_value_for_the_the_alternative_date_format_setting = () =>
                {
                    DateTime response = Convert.ToDateTime(_itemFour.FindAlternativeDateFormat());
                    response.ShouldEqual(ExpectedResponse);
                };

            private It should_find_each_setting_in_the_matching_configuration_element = () =>
                {
                    DateTime firstValue = Convert.ToDateTime(_itemThree.FindFirstValue());
                    firstValue.ShouldEqual(DateTime.Parse("05/08/2013 12:01:10"));

                    decimal secondValue = Convert.ToDecimal(_itemThree.FindSecondValue());
                    secondValue.ShouldEqual(100.00000m);
                };

            private static readonly DateTime ExpectedResponse = Convert.ToDateTime("2013-08-05 12:01:10");
            private static dynamic _itemFour;
            private static dynamic _itemThree;
        }

        public class when_retrieving_a_collection_of_connection_strings
        {
            private Because the_configuration_contains_a_collection_of_connection_strings = () =>
                {
                    connectionStrings = Configuration.ConnectionStrings;
                };

            private It should_be_possible_to_enumerate_the_collection = () =>
                {
                    var connectionStringCount = 0; 
                    foreach (var connectionString in connectionStrings)
                    {
                        connectionStringCount++; 
                    }

                    var config = XDocument.Load("dynamic.config");
                    var countOfConnectionStringsFromFile = config.Descendants("ConnectionString").Count();

                    connectionStringCount.ShouldEqual(countOfConnectionStringsFromFile);
                };

            private It should_contain_all_connection_strings_from_the_file = () =>
                {
                    var config = XDocument.Load("dynamic.config");
                    var connectionStringsFromFile = config.Descendants("ConnectionString");

                    var counter = 0;
                    foreach (dynamic connectionString in connectionStrings)
                    {
                        string name = connectionString.Findname(); //Note case sensitivity as Xml is case sensitive
                        var expectedValue = connectionStringsFromFile.ElementAt(counter).Attribute("name").Value;
                        name.ShouldEqual(expectedValue);
                        counter++;
                    }
                };

            private static dynamic connectionStrings; 
        }
    }
}