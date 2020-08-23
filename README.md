# DIContextAutoLoader 

[![NuGet version (DIContextAutoLoader)](https://img.shields.io/nuget/v/DIContextAutoLoader.svg?style=flat-square)](https://www.nuget.org/packages/DIContextAutoLoader/)

A simple auto loader that scans for service classes and configure in the dependency injection provider.

# Core Package

```
    Install-Package DIContextAutoLoader
```

# Usage

Annotate your services with the ConfigureInjection attribute:

```csharp
[ConfigureInjection]
public class ServiceOne
{
    ...
}
```

Optionally you can specify the lifetime. (Default is Scoped.)

```csharp
[ConfigureInjection(Lifetime = InjectionLifetime.Singleton)]
public class ServiceOne
{
    ...
}
```

You can also specify the InjectionType. (Default is Auto.)

+ Auto = If the class implement interfaces, DI configuration will be done by them. If not, it will be done by the implementation type;
+ ByImplementationType = DI configuration will be done by implementation type;
+ ByServiceType = DI configuration will be done by service/interface types;
+ ByBoth = DI configuration will be done by service/interface types and by implementation type.

```csharp
[ConfigureInjection(Lifetime = InjectionLifetime.Transient, InjectionType = InjectionType.ByServiceType)]
public class ServiceTwo: ISomeService
{
    ...
}
```

# DI Extensions

+ [Microsoft.Extensions.DependencyInjection](https://github.com/dgenezini/DIContextAutoLoader.Microsoft.Extensions.DependencyInjection)
+ [Ninject](https://github.com/dgenezini/DIContextAutoLoader.Ninject)

# Usage

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AutoLoadServices(typeof(SomeTypeInAssembly).Assembly);
 
        ...
    }
}
```

