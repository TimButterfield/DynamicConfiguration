using System;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Integration.ConfigurationSpecs
{
    [Subject(typeof(Configuration))]
    public class when_using_configuration_object_to_access_items
    {
        public class when_configuration_item_does_not_exist
        {
            private Establish context = () => configuration = new Configuration();

            private Because configuration_items_do_not_exist = () => _exception = Catch.Exception(() => configuration.AConfigurationItemThatDoesNotExist.FindSomething() );

            private It should_throw_a_runtime_exception = () => _exception.ShouldBeOfType<ConfigurationItemNotFoundException>();
                
            private static dynamic configuration;
            private static Exception _exception;
        }

        public class when_configuration_item_does_exist
        {
            private Establish context = () => configuration = new Configuration();

            private Because configuration_items_do_not_exist = () => itemFour = configuration.ItemFour;

            private It shoudld_find_a_value_for_the_configuration_item = () => itemFour.FindAlternativeDateFormat().ShouldEqual(new DateTime());

            private static dynamic configuration;
            private static dynamic itemFour;
        }
    }
}