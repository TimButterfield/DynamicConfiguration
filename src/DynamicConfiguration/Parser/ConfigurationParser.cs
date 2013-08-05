﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DynamicConfiguration.Parser
{
    public class ConfigurationParser
    {
        private static IDictionary<string, object> _dictionary;
        
        public static dynamic Parse(string configurationPath = @"dynamic.config")
        {
            configurationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configurationPath); 
            
            if (!File.Exists(configurationPath))
                throw new FileNotFoundException(string.Format("Could not locate dynamic configuration {0}", configurationPath));

            var configuration = XDocument.Load(configurationPath);
            
            if (!configuration.Nodes().Any())
                throw new Exception("The configuration is empty");

            var rootElement = configuration.Root;
            ValidateRoot(rootElement); 
            return rootElement != null ? GetElements(rootElement.Elements()) : null;
        }

        private static void ValidateRoot(XElement rootElement)
        {
            if (rootElement == null) 
                throw new ArgumentNullException("rootElement");

            if (rootElement.Name != "dynamic")
                throw new Exception("root element must be dynamic");
        }

        private static dynamic GetElements(IEnumerable<XElement> elements, dynamic easyConfig = null)
        {
            foreach (var element in elements)
            {
                if (easyConfig == null)
                {
                    easyConfig = new ExpandoObject();
                    _dictionary = (IDictionary<string, object>)easyConfig;
                }

                if (element.HasAttributes)
                {
                    dynamic item = new ExpandoObject();
                    var localDictionary = (IDictionary<string, object>)item;

                    foreach (var attribute in element.Attributes())
                        localDictionary.Add(attribute.Name.ToString(), GetValue(attribute));
                    
                    _dictionary.Add(element.Name.ToString(), localDictionary);
                }

                if (element.HasElements)
                    GetElements(element.Elements(), easyConfig);
            }

            return easyConfig;
        }

        private static object GetValue(XAttribute attribute)
        {
            DateTime valueAsDateTime; 
            int valueAsInt;
            decimal valueAsDecimal;
            double valueAsDouble; 

            if (DateTime.TryParse(attribute.Value, out valueAsDateTime))
                return valueAsDateTime; 

            if (int.TryParse(attribute.Value, out valueAsInt))
                return valueAsInt;

            if (double.TryParse(attribute.Value, out valueAsDouble))
                return valueAsDouble;

            if (decimal.TryParse(attribute.Value, out valueAsDecimal))
                return valueAsDecimal; 

            return attribute.Value;
        }
    }
}