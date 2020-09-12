using DIContextAutoLoader.Tests.Implementations;
using NUnit.Framework;
using System.Linq;

namespace DIContextAutoLoader.Tests
{
    public class DefaultConfigurationTests
    {
        [Test]
        public void InjectByInterfaceByDefault()
        {
            var Configurations = ServiceInjectionManager.GetServicesInjectionConfigurarions(
                typeof(InterfaceDefaultInjection).Assembly);

            var Configuration = Configurations
                .Single(a => a.ImplementationType == typeof(InterfaceDefaultInjection));

            Assert.AreEqual(typeof(InterfaceOne), Configuration.ServiceType);
        }

        [Test]
        public void InjectByImplementarionByDefault()
        {
            var Configurations = ServiceInjectionManager.GetServicesInjectionConfigurarions(
                typeof(NoInterfaceDefaultInjection).Assembly);

            var Configuration = Configurations
                .Single(a => a.ImplementationType == typeof(NoInterfaceDefaultInjection));

            Assert.AreEqual(typeof(NoInterfaceDefaultInjection), Configuration.ServiceType);
        }

        [Test]
        public void InterfaceByBothInjection()
        {
            var Configurations = ServiceInjectionManager.GetServicesInjectionConfigurarions(
                typeof(InterfaceByBothInjection).Assembly);

            var ConfigurationByInterface = Configurations
                .Single(a => a.ServiceType == typeof(InterfaceTwo));

            Assert.AreEqual(typeof(InterfaceTwo), ConfigurationByInterface.ServiceType);
            Assert.That(typeof(InterfaceTwo).IsAssignableFrom(ConfigurationByInterface.ServiceType));

            var ConfigurationByImplementarion = Configurations
                .Single(a => a.ServiceType == typeof(InterfaceByBothInjection));

            Assert.AreEqual(typeof(InterfaceByBothInjection), ConfigurationByImplementarion.ServiceType);
            Assert.That(typeof(InterfaceTwo).IsAssignableFrom(ConfigurationByImplementarion.ServiceType));
        }

        [Test]
        public void InjectByImplementarion()
        {
            var Configurations = ServiceInjectionManager.GetServicesInjectionConfigurarions(
                typeof(InterfaceByImplementarionInjection).Assembly);

            var Configuration = Configurations
                .Single(a => a.ImplementationType == typeof(InterfaceByImplementarionInjection));

            Assert.AreEqual(typeof(InterfaceByImplementarionInjection), Configuration.ServiceType);
            Assert.That(typeof(InterfaceThree).IsAssignableFrom(Configuration.ServiceType));
        }
    }
}