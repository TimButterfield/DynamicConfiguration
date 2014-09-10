using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using DynamicConfiguration.DuckTyping;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Integration.ConfigurationSpecs
{
    public class CastingSpecs
    {
        public class when_casting_a_configuration_item_using_dynamic_duck_typing
        {
            Establish context = () =>
            {
                Configuration = new Configuration();
                expando = new ExpandoObject();
            };

            Because of = () =>
            {
                var itemThree = Configuration.ItemThree;
                ItemThree = DynamicDuck.AsIf<IItemThree>(itemThree); 
            };

            It should_cast_the_configuration_item = () =>
            {
                //FirstValue can not be cast to a dateTime because it's an object
                ItemThree.FirstValue.ShouldEqual(DateTime.Parse("05/08/2013 12:01:10"));
                ItemThree.SecondValue.ShouldEqual(100.00000m); 
            };

            static dynamic Configuration;
            static dynamic expando; 
            static IItemThree ItemThree; 
        }

        public interface IItemThree
        {
            DateTime FirstValue { get; set; }
            Decimal SecondValue { get; set; }
        }
    }
}