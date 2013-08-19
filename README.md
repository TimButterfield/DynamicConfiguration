DynamicConfiguration
====================

Dynamic Configuration is a library to enable access to configuration settings without having to create matching configuration types

===========================================================================================================================================

No doubt you've worked with the appsettings, or ConnectionStrings sections of a web.config/app.config before, and maybe you've had to go through the process of creating a custom configuration section. I have and I find that although creating a configuration section gives you static typing, the overhead i.e. the creation of a class mapped with properties decorated with attributes all a bit painful. The pain is ever increasing when you start dealing with collections of configuration items.

Dynamic Configuration is an attempt to remove that pain. So instead of having : 

	[ConfigurationSection()]
	public class MyConfigurationSection
	{
		[ConfigurationProperty("MyProperty")]
		public bool MyConfigurationProperty
		{ 
	   		get 
           		{ 
				return { Convert.ToBoolean(this["myProperty"]); 
           		} 
			set
			{ 
				this["myProperty"] = value;
			}
		}
	}

you have this : 

var configuration = new Configuration(); 
configuration.myConfigurationSection.FindMyProperty()


Configuration is a dynamic type enables the .notation as in configuration.MyConfigurationElementNameHere


