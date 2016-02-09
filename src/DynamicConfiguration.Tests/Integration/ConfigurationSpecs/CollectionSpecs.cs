using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Machine.Specifications;

namespace DynamicConfiguration.Tests.Integration.ConfigurationSpecs
{
    public class CollectionSpecs
    {
        protected Establish Context = () => Configuration = new Configuration();
        protected static dynamic Configuration;

        public class when_retrieving_a_collection_of_connection_strings
        {
            Because the_configuration_contains_a_collection_of_connection_strings = () =>
            {
                ConnectionStrings = Configuration.ConnectionStrings;
            };

            It should_be_possible_to_enumerate_the_collection = () =>
            {
                var connectionStringCount = 0;
                foreach (var connectionString in ConnectionStrings)
                {
                    connectionStringCount++;
                }

                var config = XDocument.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dynamic.config"));
                var countOfConnectionStringsFromFile = config.Descendants("ConnectionString").Count();

                connectionStringCount.ShouldEqual(countOfConnectionStringsFromFile);
            };

            It should_contain_all_connection_strings_from_the_file = () =>
            {
                var config = XDocument.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dynamic.config"));
                var connectionStringsFromFile = config.Descendants("ConnectionString");

                var counter = 0;
                foreach (dynamic connectionString in ConnectionStrings)
                {
                    string name = connectionString.Findname(); //Note case sensitivity as Xml is case sensitive
                    var connectionStrings = connectionStringsFromFile as XElement[] ?? connectionStringsFromFile.ToArray();
                    var expectedValue = connectionStrings.ElementAt(counter).Attribute("name").Value;
                    name.ShouldEqual(expectedValue);
                    counter++;
                }
            };

            static dynamic ConnectionStrings;
        } 
    }
}