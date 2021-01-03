using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DIContextAutoLoader
{
    public static class ServiceInjectionManager
    {
        /// <summary>
        /// Sets the default value for ConfigureInjectionAttribute Lifetime. Default = Scoped
        /// </summary>
        public static InjectionLifetime DefaultLifetime { get; set; } = InjectionLifetime.Scoped;

        /// <summary>
        /// Sets the default value for ConfigureInjectionAttribute InjectionType. Default = Auto
        /// </summary>
        public static InjectionType DefaultInjectionType { get; set; } = InjectionType.Auto;

        /// <summary>
        /// Scan assemblies for classes with <strong>ConfigureInjectionAttribute</strong>
        /// </summary>
        /// <param name="assemblies">Assemblies to scan for classes</param>
        /// <returns>A collection of <strong>ServiceInjectionConfigurarion</strong> with <em>Lifetime</em>, <em>ServiceType</em> and <em>ImplementationType</em></returns>
        public static IEnumerable<ServiceInjectionConfigurarion>
            GetServicesInjectionConfigurarions(params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            var InstanceInjectionConfigurarions =
                new List<ServiceInjectionConfigurarion>();

            foreach (var Assembly in assemblies)
            {
                ValidateConfigurations(Assembly);

                var Implementations = Assembly
                    .GetExportedTypes()
                    .Where(a => a.GetCustomAttributes<ConfigureInjectionAttribute>().Any() &&
                                a.IsClass &&
                                !a.IsAbstract &&
                                !a.IsNested &&
                                !a.IsGenericType)
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

        private static void ValidateConfigurations(Assembly assembly)
        {
            var InvalidImplementations = assembly
                                .GetExportedTypes()
                                .Where(a => a.GetCustomAttributes<ConfigureInjectionAttribute>().Any() &&
                                            !a.IsInterface &&
                                            (!a.IsClass ||
                                            a.IsAbstract ||
                                            a.IsNested ||
                                            a.IsGenericType))
                                .ToList();

            if (InvalidImplementations.Any())
            {
                var Exceptions = new List<DIContextAutoLoaderConfigurationException>();

                foreach (var InvalidImplementation in InvalidImplementations)
                {
                    if (!InvalidImplementation.IsClass)
                    {
                        Exceptions.Add(new DIContextAutoLoaderConfigurationException(
                            $"{InvalidImplementation.Name} is not a class."));
                    }

                    if (InvalidImplementation.IsAbstract)
                    {
                        Exceptions.Add(new DIContextAutoLoaderConfigurationException(
                            $"{InvalidImplementation.Name} is an abstract a class."));
                    }

                    if (InvalidImplementation.IsNested)
                    {
                        Exceptions.Add(new DIContextAutoLoaderConfigurationException(
                            $"{InvalidImplementation.Name} is a nested class."));
                    }

                    if (InvalidImplementation.IsGenericType)
                    {
                        Exceptions.Add(new DIContextAutoLoaderConfigurationException(
                            $"{InvalidImplementation.Name} is a generic type."));
                    }
                }

                throw new AggregateException($"Invalid configurarions detected in assembly {assembly.FullName}.", Exceptions);
            }
        }
    }
}
