using System;
using DynamicConfiguration.DuckTyping;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Integration.Casting
{
    public class when_casting_contents_of_a_configuration
    {
        private Because the_parsed_configuration_is_cast = () =>
            {
                var configuration = ConfigurationParser.Parse();
                itemOne = DynamicDuck.AsIf<IAmTheFirstItem>(configuration.ItemOne);
                itemThree = DynamicDuck.AsIf<IAmTheThirdItem>(configuration.ItemThree);
                itemFour = DynamicDuck.AsIf<IAmTryingAnAlternativeDateFormat>(configuration.ItemFour);
            };

        private It should_cast_to_type_matching_interface = () => itemOne.ShouldBeOfType<IAmTheFirstItem>();
        private It should_be_able_to_cast_to_int = () => itemOne.FirstValue.ShouldBeOfType<int>();
        private It should_be_able_to_cast_to_datetime = () => itemThree.FirstValue.ShouldBeOfType<DateTime>();
        private It should_be_able_to_cast_to_double = () => itemThree.SecondValue.ShouldBeOfType<Double>();
        private It should_be_able_to_case_to_alternativeDateTimeFormat = () => itemFour.AlternativeDateFormat.ShouldBeOfType<DateTime>();
        
        private static IAmTheFirstItem itemOne;
        private static IAmTheThirdItem itemThree;
        private static IAmTryingAnAlternativeDateFormat itemFour;
    }

    public interface IAmTryingAnAlternativeDateFormat
    {
        DateTime AlternativeDateFormat { get; set; }
    }

    public interface IAmTheFirstItem
    {
        int FirstValue { get; set; }
        string SecondValue { get; set; }
    }

    public interface IAmTheThirdItem
    {
        DateTime FirstValue { get; set; }
        Double SecondValue { get; set; }
    }
}