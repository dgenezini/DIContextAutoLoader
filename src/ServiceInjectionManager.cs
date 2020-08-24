using DIContextAutoLoader.Attributes;
using System;
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
            if (assemblies == null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            var InstanceInjectionConfigurarions = 
                new List<ServiceInjectionConfigurarion>();

            foreach (var Assembly in assemblies)
            {
                var Implementations = Assembly
                    .GetExportedTypes()
                    .Where(a => a.GetCustomAttributes<ConfigureInjectionAttribute>().Any() &&
                                a.IsClass &&
                                !a.IsAbstract &&
                                a.IsNested)
                    .ToList();

                foreach (var Implementation in Implementations)
                {
                    var Lifetime = Implementation
                        .GetCustomAttribute<ConfigureInjectionAttribute>()
                        .Lifetime;

                    var InjectionType = Implementation
                        .GetCustomAttribute<ConfigureInjectionAttribute>()
                        .InjectionType;

                    var Interfaces = Implementation.GetTypeInfo()
                        .ImplementedInterfaces;

                    if ((InjectionType == InjectionType.ByBoth) ||
                        (InjectionType == InjectionType.ByImplementationType) ||
                        ((InjectionType == InjectionType.Auto) && (Interfaces.Count() == 0)))
                    {
                        InstanceInjectionConfigurarions
                            .Add(new ServiceInjectionConfigurarion(
                                Implementation, Implementation, Lifetime));
                    }

                    if ((InjectionType == InjectionType.ByBoth) ||
                        (InjectionType == InjectionType.ByServiceType) ||
                        ((InjectionType == InjectionType.Auto) && (Interfaces.Count() > 0)))
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
