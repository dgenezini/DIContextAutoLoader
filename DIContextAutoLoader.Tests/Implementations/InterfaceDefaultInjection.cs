namespace DIContextAutoLoader.Tests.Implementations
{
    public interface InterfaceOne
    { 
    }

    [ConfigureInjection]
    public class InterfaceDefaultInjection: InterfaceOne
    {
    }
}
