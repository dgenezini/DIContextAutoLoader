using System;

namespace DIContextAutoLoader
{
    [AttributeUsage(
        AttributeTargets.Class, 
        AllowMultiple = false, 
        Inherited = false)]
    public class ConfigureInjectionAttribute : Attribute
    {
        public InjectionLifetime Lifetime { get; set; } = InjectionLifetime.Scoped;
        public InjectionType InjectionType { get; set; } = InjectionType.Auto;
    }
}
