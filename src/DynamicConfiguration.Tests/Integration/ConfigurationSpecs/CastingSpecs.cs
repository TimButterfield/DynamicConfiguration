using System;
using System.Security.Cryptography.X509Certificates;
using DynamicConfiguration.DuckTyping;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Integration.ConfigurationSpecs
{
    public class CastingSpecs
    {
        public class when_casting_a_configuration_item_using_dynamic_duck_typing
        {
            Establish context = () => { Configuration = new Configuration(); };

            Because of = () =>
            {
                var itemThree = Configuration.ItemThree;
                ItemThree = DynamicDuck.AsIf<IItemThree>(itemThree); 
            };

            It should_cast_the_configuration_item = () =>
            {
                var result = ItemThree.FirstValue;
                var expectation = DateTime.Parse("05/08/2013 12:01:10");
                result.ShouldEqual(expectation);
                ItemThree.SecondValue.ShouldEqual(100.00000m); 
            };

            static dynamic Configuration;
            static IItemThree ItemThree; 
        }

        public interface IItemThree
        {
            DateTime FirstValue { get; set; }
            Decimal SecondValue { get; set; }
        }
    }
}