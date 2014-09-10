using System;
using System.Linq;
using System.Xml.Linq;
using DynamicConfiguration.Exceptions;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Integration.ConfigurationSpecs
{
    [Subject(typeof(Configuration))]
    public class SingleConfigurationItemsSpecs 
    {
        protected Establish Context = () => Configuration = new Configuration();
        protected static dynamic Configuration;

        public class when_configuration_setting_does_not_exist
        {
            Because configuration_items_do_not_exist = () => _exception = Catch.Exception(() => Configuration.AConfigurationItemThatDoesNotExist.FindSomething() );

            It should_throw_a_runtime_exception = () => _exception.ShouldBeOfType<ConfigurationItemNotFoundException>();
            
            static Exception _exception;
        }

        public class when_known_configuration_settings_do_exist
        {
            Because known_configuration_items_do_exist = () =>
                {
                    ItemFour = Configuration.ItemFour;
                    ItemThree = Configuration.ItemThree; 
                };

            It should_find_a_value_for_the_the_alternative_date_format_setting = () =>
                {
                    DateTime result = Convert.ToDateTime(ItemFour.FindAlternativeDateFormat());
                    result.ShouldEqual(ExpectedAlternativeDateFormat);
                };

            It should_find_each_setting_in_the_matching_configuration_element = () =>
                {
                    DateTime firstValue = Convert.ToDateTime(ItemThree.FindFirstValue());
                    firstValue.ShouldEqual(DateTime.Parse("05/08/2013 12:01:10"));

                    decimal secondValue = Convert.ToDecimal(ItemThree.FindSecondValue());
                    secondValue.ShouldEqual(100.00000m);
                };

           static readonly DateTime ExpectedAlternativeDateFormat = Convert.ToDateTime("2013-08-05 12:01:10");
           static dynamic ItemFour;
           static dynamic ItemThree;
        }

        
    }
}