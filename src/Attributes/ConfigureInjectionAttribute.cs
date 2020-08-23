using System;

namespace DIContextAutoLoader.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigureInjectionAttribute : Attribute
    {
        public InjectionLifetime Lifetime { get; set; } = InjectionLifetime.Scoped;
        public InjectionType InjectionType { get; set; } = InjectionType.Auto;
    }
}
