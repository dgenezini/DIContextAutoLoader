using DIContextAutoLoader.Tests.InvalidImplementarions.Implementations;
using NUnit.Framework;
using System;
using System.Linq;
using static DIContextAutoLoader.Tests.InvalidImplementarions.Implementations.ParentClass;

namespace DIContextAutoLoader.Tests
{
    public class InvalidConfigurationsTests
    {
        [Test]
        public void InvalidConfigurarions()
        {
            var ex = Assert
                .Throws<AggregateException>(() =>
                    ServiceInjectionManager.GetServicesInjectionConfigurarions(
                        typeof(AbstractImplementation).Assembly));

            Assert.That(ex.InnerExceptions.All(a => a is DIContextAutoLoaderConfigurationException));
            Assert.That(ex.InnerExceptions.Any(a => a.Message == $"{nameof(AbstractImplementation)} is an abstract a class."));
            Assert.That(ex.InnerExceptions.Any(a => a.Message == $"{nameof(NestedImplementation)} is a nested class."));
            Assert.That(ex.InnerExceptions.Any(a => a.Message == "GenericImplementation`1 is a generic type."));
        }
    }
}