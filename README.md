DynamicConfiguration
====================

Dynamic Configuration is a library to enable access to configuration settings without having to create matching configuration types. 

I was inspired to write this library as I felt writing configuration classes was duplicating boilerplate code. I took inspiration for the syntax from Mark Rendles Simple Data.

===========================================================================================================================================

No doubt you've worked with the appsettings, or ConnectionStrings sections of a web.config/app.config before, and maybe you've had to go through the process of creating a custom configuration section. I have and I find that although creating a configuration section gives you static typing, the overhead i.e. the creation of a class mapped with properties decorated with attributes all a bit painful. The pain is ever increasing when you start dealing with collections of configuration items.

Dynamic Configuration is an attempt to remove that pain. So instead of having : 


	public class MyConfigurationSection : ConfigurationSection
	{
		[ConfigurationProperty("AMatchingPropertyNameHere")]
		public bool MyConfigurationProperty
		{ 
	   		get 
           		{ 
				return { Convert.ToBoolean(this["AMatchingPropertyNameHere"]); 
           		} 
			set
			{ 
				this["AMatchingPropertyNameHere"] = value;
			}
		}
	}

you have this : 

	var configuration = new Configuration(); 
	configuration.myConfigurationItem.FindAMatchingPropertyNameHere();

With the above code, you need to know the name of the xml node you're looking for in your configuration file. In this instance an xml node with the name myConfigurationItem. Calling the Find... then invokes a dynamic TryInvokeMember which returns the value of the first matching attribute with the name AMatchingPropertyNameHere. 

You won't have intellisense as everything is dynamic. I've written the library to throw explcit exceptions where things can't be found, but the code is still in development so you might end up with runtime binder exceptions.

More to come on asserting those values at appstart, in testsing etc, during the transformation process. 


