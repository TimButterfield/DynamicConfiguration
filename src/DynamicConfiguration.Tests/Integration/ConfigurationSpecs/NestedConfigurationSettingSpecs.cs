using System.Linq;
using System.Xml.Linq;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Integration.ConfigurationSpecs
{
    public class NestedConfigurationSettingSpecs : WithSubject
    {
        public class when_getting_a_configuration_setting_that_is_nested_in_the_xml
        {
            Establish context = () =>
            {
                Configuration = new Configuration();

                var configuration = XDocument.Load("dynamic.config");
                ExpectedFileAppenderFileName = GetAppenderFileName(configuration);
                ExpectedConversionPattern = GetAppenderConversionPattern(configuration);
            };
            
            Because of = () =>
            {
                
                Name = Configuration.log4net.appender.file.Findvalue();
                ConverstionPattern = Configuration.log4net.appender.layout.conversionPattern.Findvalue(); 
                
                
            };

            It should_resolve_the_values = () =>
            {
                Name.ShouldEqual(ExpectedFileAppenderFileName);
                ConverstionPattern.ShouldEqual(ExpectedConversionPattern);
            };

            static string Name;
            static string ConverstionPattern ;
            static dynamic Configuration;
            static string ExpectedConversionPattern;
            static string ExpectedFileAppenderFileName;

            static string GetAppenderConversionPattern(XDocument config)
            {
                return config.Descendants("log4net").Elements("appender").Elements("layout").Elements("conversionPattern").Attributes("value").First().Value;
            }

            static string GetAppenderFileName(XDocument config)
            {
                return config.Descendants("log4net").Elements("appender").Elements("file").Attributes("value").First().Value;
            }
        }
    }
}