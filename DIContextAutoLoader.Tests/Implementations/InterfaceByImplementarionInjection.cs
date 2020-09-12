namespace DIContextAutoLoader.Tests.Implementations
{
    public interface InterfaceThree
    { 
    }

    [ConfigureInjection(InjectionType = InjectionType.ByImplementationType)]
    public class InterfaceByImplementarionInjection : InterfaceThree
    {
    }
}
