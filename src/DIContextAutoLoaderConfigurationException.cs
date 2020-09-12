using System;

namespace DIContextAutoLoader
{
    public class DIContextAutoLoaderConfigurationException: Exception
    {
        public DIContextAutoLoaderConfigurationException(string message, Exception innerException) :
            base(message, innerException)
        {
        }

        public DIContextAutoLoaderConfigurationException(string message) :
            base(message)
        {
        }
    }
}
