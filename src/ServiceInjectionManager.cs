using DIContextAutoLoader.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DIContextAutoLoader
{
    public static class ServiceInjectionManager
    {
        public static IEnumerable<ServiceInjectionConfigurarion> 
            GetServicesInjectionConfigurarions(params Assembly[] assemblies)
        {
            var InstanceInjectionConfigurarions = 
                new List<ServiceInjectionConfigurarion>();

            foreach (var Assembly in assemblies)
            {
                var Implementations = Assembly
                    .GetTypes()
                    .Where(a => a.GetCustomAttributes<ConfigureInjectionAttribute>().Any() &&
                                a.IsClass)
                    .ToList();

                foreach (var Implementation in Implementations)
                {
                    var Lifetime = Implementation
                        .GetCustomAttribute<ConfigureInjectionAttribute>()
                        .Lifetime;

                    var InjectionType = Implementation
                        .GetCustomAttribute<ConfigureInjectionAttribute>()
                        .InjectionType;

                    var Interfaces = Implementation.GetInterfaces()
                        .ToArray();

                    if ((InjectionType == InjectionType.ByBoth) ||
                        (InjectionType == InjectionType.ByImplementationType) ||
                        ((InjectionType == InjectionType.Auto) && (Interfaces.Length == 0)))
                    {
                        InstanceInjectionConfigurarions
                            .Add(new ServiceInjectionConfigurarion(
                                Implementation, Implementation, Lifetime));
                    }

                    if ((InjectionType == InjectionType.ByBoth) ||
                        (InjectionType == InjectionType.ByServiceType) ||
                        ((InjectionType == InjectionType.Auto) && (Interfaces.Length > 0)))
                    {
                        foreach (var Interface in Interfaces)
                        {
                            InstanceInjectionConfigurarions
                                .Add(new ServiceInjectionConfigurarion(
                                    Interface, Implementation, Lifetime));
                        }
                    }
                }
            }

            return InstanceInjectionConfigurarions;
        }
    }
}
