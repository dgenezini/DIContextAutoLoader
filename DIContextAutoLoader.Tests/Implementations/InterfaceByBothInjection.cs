namespace DIContextAutoLoader.Tests.Implementations
{
    public interface InterfaceTwo
    { 
    }

    [ConfigureInjection(InjectionType = InjectionType.ByBoth)]
    public class InterfaceByBothInjection : InterfaceTwo
    {
    }
}
