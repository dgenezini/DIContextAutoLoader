using System;

namespace DIContextAutoLoader
{
    /// <summary>
    /// Mark the class for dependency injection
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class, 
        AllowMultiple = false, 
        Inherited = false)]
    public class ConfigureInjectionAttribute : Attribute
    {
        /// <summary>
        /// Lifetime of injected service. Default = Scoped
        /// </summary>
        public InjectionLifetime Lifetime { get; set; } = InjectionLifetime.Scoped;

        /// <summary>
        /// Sets if the service will be injected by interface or implementation. Default = Auto
        /// </summary>
        public InjectionType InjectionType { get; set; } = InjectionType.Auto;
    }
}
