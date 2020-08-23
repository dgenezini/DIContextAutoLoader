using System;

namespace DIContextAutoLoader
{
    public class ServiceInjectionConfigurarion
    {
        public ServiceInjectionConfigurarion(Type serviceType,
            Type implementationType, InjectionLifetime lifetime)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
            Lifetime = lifetime;
        }

        public Type ServiceType { get; set; }
        public Type ImplementationType { get; set; }
        public InjectionLifetime Lifetime { get; set; }
    }
}
