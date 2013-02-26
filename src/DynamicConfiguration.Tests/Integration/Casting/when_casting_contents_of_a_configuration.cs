using DynamicConfiguration.DuckTyping;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Integration.Casting
{
    public class when_casting_contents_of_a_configuration
    {
        private Because the_parsed_configuration_is_cast = () =>
            {
                var configuration = ConfigurationParser.Parse();
                result = DynamicDuck.AsIf<IAmTheFirstItem>(configuration.ItemOne);
            };

        private It should_cast_with_no_issues = () => result.ShouldBeOfType<IAmTheFirstItem>();
        private It should_have_the_expected_first_value = () => result.FirstValue.ShouldBeOfType<long>();
        //private It should_have_the_expected_second_value = () => result.SecondValue.ShouldEqual(2); 

        private static IAmTheFirstItem result; 
    }

    public class ItemOne
    {
        public int FirstValue { get; set; }
        public int SecondValue { get; set; }
    }

    public interface IAmTheFirstItem
    {
        long FirstValue { get; set; }
        string SecondValue { get; set; }
    }
}